using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class playerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        Standard,
        Playing,
        Dialogue
    }

    // Public variables
    public PlayerState currentState = PlayerState.Standard;
    public float moveSpeed = 5f; // Adjust this to change the player's movement speed.

    [Header ("Camera")]
    public Camera mainCamera; // Reference to the main camera in the scene
    public Transform[] roomCameras; // Array of all the room cameras
    public float cameraMoveSpeed = 5f; // Adjust this to change the camera's movement speed.
    public float cameraRotationSpeed = 3f; // Adjust this to change the camera's rotation speed.
    public float cameraHeight = 3f; // Height of the camera from the ground.
    public float cameraDistance = 7.5f; // Distance of the camera from the player.
    public float zoomSpeed = 5f; // Speed of the camera zoom.
    public float zoomDistance = 2f; // Distance of the camera from the player when zoomed in.

    [Header ("Ocarina Playing")]
    public KeyCode playModeKey = KeyCode.Space; // Key to enter/exit play mode.
    public KeyCode[] noteKeys = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U }; // Array of note keys.
    public AudioClip[] noteSounds; // Array of note sounds.

    [Header ("Dialogue")]
    public UIManager uIManager;
    public TextAsset[] dialogueList; // Array of dialogue.
    

    [Header ("Other")]
    AudioSource audioSource; // Reference to the audio source component.
    public AudioSource bgm;
    public GameObject postProcessing; // Reference to the post-processing game object.
    public inventoryManager inventory;
    public dialogueManager dialogue;
    private GameManager manager;
   

    public Animator playerAnim;
    SpriteRenderer playerSprite;

    // Private variables
    private string songPlayed = ""; // String to store the notes played.
    private int noteIndex = -1; // Index of the note being played.
    private int currentRoom = 0; // Index of the current room.
    //private bool facingForward = true;
    private Vector3 zoomedPosition; // Position of the camera when zoomed in.
    private Vector3 cameraLocation; // Position of the camera when not zoomed in.
    private Quaternion defaultRotation; // Default rotation of the camera.
    private Quaternion zoomRotation; // Rotation of the camera when zoomed in.
    private PostProcessVolume postProcessVolume; // Reference to the post-processing volume component.
    private ColorGrading musicFilter; // Reference to the color grading component.


    // Start is called before the first frame update
    void Start()
    {

        //RoughBGM.heh();

        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        dialogue = GameObject.FindWithTag("UI").GetComponent<dialogueManager>();
        manager = GameManager.Instance;

        defaultRotation.Set(.177f, 0f, 0f, 1f);
        zoomRotation.Set(0, 0, 0, 1);
        audioSource.loop = true;
        postProcessVolume = postProcessing.GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<ColorGrading>(out musicFilter);
        Color color = new Color(0.37f, 0.37f, 0.37f);
        setFilterColor(color, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // enables player movement
        if (!GameManager.isZoomed && !GameManager.cameraMoving)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            float speed = Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput);

            playerAnim.SetFloat("Speed", speed);
            playerAnim.SetFloat("Direction", verticalInput);

            if (horizontalInput > 0)
            {
                playerSprite.flipX = true;
            }
            else if (horizontalInput < 0)
            {
                playerSprite.flipX = false;
            }

            transform.Translate(new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime);
        }
        else if (GameManager.isInPlayMode)
        {
            // goes through noteKeys to check if playing
            for (int i = 0; i < noteKeys.Length; i++)
            {
                // starts playing sound if note is pressed
                if (Input.GetKeyDown(noteKeys[i]))
                {
                    noteIndex = i;
                    audioSource.clip = noteSounds[noteIndex]; // load corresponding note clip
                    Debug.Log("Loaded Note" + noteIndex);
                    songPlayed += i.ToString();
                    audioSource.Play();
                }
                // stops playing sound when note is released
                else if (Input.GetKeyUp(noteKeys[i]) && noteIndex == i)
                {
                    audioSource.Stop();
                    noteIndex = -1;
                }
            }

            // Check if the song has reached the maximum length.
            if (songPlayed.Length >= 3)
            {
                // changes color if matching a song
                // red song 1 (QWE)
                // red song: DEA -> WEY -> 125
                if (songPlayed == "012" && GameManager.songsUnlocked >= 2)
                {
                    Debug.Log("Song 1.");
                    GameManager.Color = "RED"; //sets global var that can be used to hide/unhide/change objects;
                    GameManager.CanChangeTrack = true;
                }
                // green song 2 (RTY)
                // green song: CGF -> QTR -> 043
                else if (songPlayed == "345" && GameManager.songsUnlocked >= 3)
                {
                    Debug.Log("Song 2.");
                    GameManager.Color = "GREEN";
                    GameManager.CanChangeTrack = true;
                }
                // blue song 3 (WTR)
                else if (songPlayed == "143" && GameManager.songsUnlocked >= 4)
                {
                    Debug.Log("Song 3.");
                    GameManager.Color = "BLUE";
                    GameManager.CanChangeTrack = true;
                }
                else
                {
                    Debug.Log("Not a song");
                    GameManager.Color = "GREY";
                    GameManager.CanChangeTrack = true;
                }

                // displays song
                Debug.Log("Played: "+songPlayed);

                // resets song
                songPlayed = "";
                Debug.Log("Song reset.");

            }
        }
        //The changing of the post process colors (seperate for scene change reasons)
        if (GameManager.Color == "RED")
        {
            Color color = new Color(1.0f, 0.71f, 0.72f);
            setFilterColor(color, 30);
        }
        else if (GameManager.Color == "GREEN")
        {
            Color color = new Color(0.71f, 1.0f, 0.79f);
            setFilterColor(color, 30);
        }
        else if (GameManager.Color == "BLUE")
        {
            Color color = new Color(0.5f, 0.84f, 1.0f);
            setFilterColor(color, 30);
        }
        else
        {
            Color color = new Color(0.37f, 0.37f, 0.37f);
            setFilterColor(color, 0.0f);
        }
        // enables/disables play mode if camera isn't moving
        if (Input.GetKeyDown(playModeKey) && !GameManager.cameraMoving && !GameManager.inCinematic && !GameManager.inventoryOpen)
        {
            GameManager.isInPlayMode = !GameManager.isInPlayMode;
            GameManager.cameraMoving = true;
            //audioSource.Stop();
            songPlayed = "";

            if (GameManager.isInPlayMode)
            {
                Debug.Log("Play mode enabled.");
                playerAnim.SetBool("Playing", true);
                ZoomIn(this.gameObject);

                //StartCoroutine(changeVolume(bgm, 0.3f)); //JEN: lower to hear ocarina... 
                GameManager.PublicPlayMode = true;

            }
            else
            {
                Debug.Log("Play mode disabled.");
                playerAnim.SetBool("Playing", false);
                ZoomOut();

                //StartCoroutine(changeVolume(bgm, 1f)); //JEN: Full volume when out of play mode
                GameManager.PublicPlayMode = false;
            }
        }
    }

    // zooms camera in based on player position
    public void ZoomIn(GameObject target)
    {
        GameManager.isZoomed = true;
        GameManager.cameraMoving = true;
        zoomedPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - zoomDistance);
        StartCoroutine(MoveCamera(zoomedPosition, zoomRotation)); 
    }

    //if now value defaults to player
    public void ZoomIn()
    {
        GameManager.isZoomed = true;
        GameManager.cameraMoving = true;
        zoomedPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - zoomDistance);
        StartCoroutine(MoveCamera(zoomedPosition, zoomRotation));
    }


    // zooms camera out based on player position
    public void ZoomOut()
    {
        GameManager.isZoomed = false;
        GameManager.cameraMoving = true;
        cameraLocation = new Vector3(roomCameras[currentRoom].transform.position.x, roomCameras[currentRoom].transform.position.y + cameraHeight, roomCameras[currentRoom].transform.position.z - cameraDistance);
        StartCoroutine(MoveCamera(cameraLocation, defaultRotation));
    }

    // handles camera movement
    private IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        while (mainCamera.transform.position != targetPosition || mainCamera.transform.rotation != targetRotation)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, cameraRotationSpeed * Time.deltaTime);
            yield return null;
        }
        GameManager.cameraMoving = false;
    }

    // checks if player enters a collider
    private void OnTriggerEnter(Collider other)
    {
        // if tagged with Room check if different area and move camera
        if (other.CompareTag("Room"))
        {
            int newRoom = System.Array.IndexOf(roomCameras, other.transform);
            GameManager.cameraMoving = true;

            if (newRoom >= 0 && newRoom < roomCameras.Length)
            {
                currentRoom = newRoom;
                cameraLocation = new Vector3(roomCameras[currentRoom].transform.position.x, roomCameras[currentRoom].transform.position.y + cameraHeight, roomCameras[currentRoom].transform.position.z - cameraDistance);
                StartCoroutine(MoveCamera(cameraLocation, defaultRotation));        
            }
        }
        // if tagged with dialogue, sent the cooresponding dialogue to UIManager
        else if (other.CompareTag("Dialogue"))
        {
            startDialogue(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    public void startDialogue(GameObject dialogueObj)
    {
        if (GameManager.inventoryOpen)
        {
            inventory.toggleInventory();
        }

        dialogue.getDialogue(dialogueObj);
    }

    // ends dialogue
    public void endDialogue()
    {
        ZoomOut();
        GameManager.isInPlayMode = false;
        GameManager.inCinematic = false;
    }

    private void setFilterColor(Color color, float saturation)
    {
        musicFilter.colorFilter.value = color;
        musicFilter.saturation.value = saturation;
    }

    //JEN: Quick solution for Milestone 3 bgm
  
    private IEnumerator changeVolume(AudioSource source, float newVolume)
    {
        float startingVolume = source.volume;
        float time = 0f;

        while (time < 0f)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startingVolume, newVolume, Time.deltaTime);
            yield return null;
        }

        source.volume = newVolume;
    }
}
