using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{

    //public AdaptText textAdapter; // reference to AdaptText
    public playerMovement playerScript; // reference to playerMovement
    public inventoryManager inventory;
    public dialogueManager dialogue;
    public GameManager manager;

    public string[] sentenceWords; // array of string of words in each sentence
    public string[] currentPassage; // array of sentences in string
    public TextAsset currentWriting; // current dialogue option 

    public float wordDelay = 0.1f; // delay between showing each word
    private int currentSentence = 0; // current sentence in currentPassage
    public string currentText = "Starting Text"; // default text
    private bool finishedSentence = false; // checks if finished in words in sentenceWords
    private bool inDialogue = false;
    bool finishedPassage = false; // checks if finished with sentences in currentPassage

    public Text dialogueText; // reference to UI text  
    public RectTransform dialogueBox; // reference to UI backing image
    public float dialogueDistance = 200f; // reference to movement distance for dialogueBox

    public RectTransform playModeBox;
    public float playModeDistance = 300f;
    public Image[] noteImages;

    public Image[] inventoryImages;
    public Button[] inventoryButtons;
    public RectTransform inventoryBox;
    public float inventoryDistance = -200f;
    public float inventoryDelay = 2f;
    private void Start()
    {
        manager = GameManager.Instance;

        GameObject player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<playerMovement>();
        inventory = player.GetComponent<inventoryManager>();


        for (int i = 0; i < inventoryButtons.Length; i++)
        {
            int currentButton = i;

            inventoryButtons[i].onClick.AddListener(() => inventoryButtonPress(currentButton));
        }

        updateInventory();
    }

    void Update()
    {
        updateText();
        // checks if sentence is complete, allows player to click to continue
        if (finishedSentence && finishedPassage == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentText = " ";
                currentSentence++;
                nextSentence();
                finishedSentence = false;
            }
        }
        // ends dialogue once passage is finished
        else if (finishedPassage && inDialogue)
        {
            Debug.Log("finished passage");
            finishText();
            dialogue.nextSection();
        }
    }

    // updates UI text
    public void updateText()
    {
        dialogueText.text = currentText;
    }

    // converts text and enables dialogue UI
    public void displayPassage(string[] textName)
    {
        currentText  = "";
        finishedPassage = false;
        inDialogue = true;
        currentPassage = textName;

        nextSentence();

    }

    // checks if passage is complete, displays next sentence if not
    public void nextSentence()
    {
        if (currentSentence == currentPassage.Length)
        {
            finishedSentence = true;
            finishedPassage = true;

        }
        else
        {
            sentenceWords = adaptSentence(currentPassage[currentSentence]);
            StartCoroutine(displaySentence());
        }
    }

    // displays each word one at a time
    public IEnumerator displaySentence()
    {
        for (int i = 0; i < sentenceWords.Length; i++)
        {
            currentText += sentenceWords[i] + " ";
            Debug.Log("adding: " + sentenceWords[i]);
            yield return new WaitForSeconds(wordDelay);
        }
        finishedSentence = true;
    }

    // converts sentence to array of words
    public string[] adaptSentence(string sentence)
    {
        string[] nextSentence = sentence.Split(' ');
        return nextSentence;
    }

    // resets text for next dialogue
    void finishText()
    {
        currentSentence = 0;
        finishedSentence = false;
        finishedPassage = false;
    }

    public void endDialogue()
    {
        currentText = "";
        calculateUI(dialogueBox, dialogueDistance * -1, 0.2f);
        inDialogue = false;
    }

    public void addItem(Sprite itemImage)
    {
        updateInventory();

        StartCoroutine(displayNewItem());
    }
    public void removeItem(int item)
    {
        updateInventory();
    }

    bool inventoryShown = false;
    public void inventoryToggle(bool currently)
    {
        updateInventory();
        if (currently && !inventoryShown)
        {
            calculateUI(inventoryBox, inventoryDistance, 0.2f);
            inventoryShown = true;
        }
        else if (!currently && inventoryShown)
        {
            calculateUI(inventoryBox, -inventoryDistance, 0.2f);
            inventoryShown = false;
        }
    }

    public void inventoryButtonPress(int buttonPressed)
    {
        if (!GameManager.inCinematic && buttonPressed < inventoryImages.Length)
        {
            inventory.useItem(buttonPressed);
        }
    }

    public void updateInventory()
    {
        // Reforms array to remove all null entries
        manager.pickupItems.RemoveAll(item => item == null);
        manager.pickupSprite.RemoveAll(item => item == null);
        Image[] tempArray = new Image[inventoryImages.Length];

        int newIndex = 0;
        for (int i = 0; i < inventoryImages.Length; i++)
        {
            if (inventoryImages[i] != null)
            {
                tempArray[newIndex] = inventoryImages[i];
                newIndex++;
            }
        }

        System.Array.Resize(ref tempArray, newIndex);
        inventoryImages = tempArray;

        // Sets all UI images to null
        for (int i = 0; i < 6; i++)
        {
            inventoryImages[i].sprite = null;
        }

        // Readds all UI imagfes as sprites
        for (int i = 0; i < manager.pickupSprite.Count; i++)
        {
            inventoryImages[i].sprite = manager.pickupSprite[i];
        }
    }

    public IEnumerator displayNewItem()
    {
        // only displays item if inventory closed
        if (!GameManager.inventoryOpen){
            inventoryToggle(true);
            yield return new WaitForSeconds(inventoryDelay);
            inventoryToggle(false);
            yield return new WaitForSeconds(0.5f);
        }
        
        playerScript.endDialogue();
    }

    // calculates which UI element and distance to move
    public void calculateUI(RectTransform UI, float uiDestination, float time)
    {
        Vector3 currentUIPosition = UI.localPosition;
        Vector3 newUIPosition = new Vector3(currentUIPosition.x, currentUIPosition.y + uiDestination, currentUIPosition.z);
        StartCoroutine(moveUI(currentUIPosition, newUIPosition, UI, time));
    }

    bool musicShown = false;
    public void musicToggle(bool currently)
    {
        if (currently && !musicShown)
        {
            calculateUI(playModeBox, playModeDistance, 0.6f);
            musicShown = true;
        }
        else if(! currently && musicShown)
        {
            calculateUI(playModeBox, -playModeDistance, 0.5f);
            musicShown=false;
        }
    }
    public void updateNote(int notePos, Sprite sprite, Color color)
    {
        noteImages[notePos].color = color;
        noteImages[notePos].sprite = sprite;
    }


    // moves UI based on calculateUI distances
    public IEnumerator moveUI(Vector3 start, Vector3 end, RectTransform button, float moveTime)
    {
        float currentMovementTime = 0f;
        while (Vector3.Distance(button.transform.localPosition, end) > 0)
        {
            currentMovementTime += Time.deltaTime;
            button.transform.localPosition = Vector3.Lerp(start, end, currentMovementTime / moveTime);
            yield return null;
        }
    }
}
