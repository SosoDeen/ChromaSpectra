using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string Color = "GREY";
    public static bool CanChangeTrack;
    public static bool PublicPlayMode;
    public static int songsUnlocked;
    public static int Progression;

    public static bool isZoomed = false; // Flag to check if the camera is zoomed in.
    public static bool cameraMoving = false; // Flag to check if the camera is moving.
    public static bool inCinematic = false;
    public static bool inventoryOpen = false;
    public static bool isInPlayMode = false; // Flag to check if the player is in play mode.

    [Header ("UI Manager")]
    public List<Sprite> pickupSprite; // List of sprites of picked up items
    public List<string> pickupItems; // List of picked up items


    [Header ("Interacibles Dictionary")]
    public List<GameObject> allInteractables; // list of all interactable items in scene
    public List<AudioClip> allPickupSounds; // list of all sounds of interactble items
    public List<AudioClip> allAmbientSounds; // list of ambient sounds for scene

    public static GameManager Instance;

    public static int lastScene;

    // Start is called before the first frame update
    void Start()
    {
        
        songsUnlocked = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            Time.timeScale = 0.0f;
        }
        //Debug.Log("Progress " + Progression);  
        
    }
    void Awake()
    {
        if(Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(this.gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        
        } 
        else if(Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(this.gameObject); // Destroy the GameObject, this component is attached to
        }

    }

    public void updateInventory(string name, Sprite sprite)
    {
        pickupItems.Add(name);
        pickupSprite.Add(sprite);
    }
}