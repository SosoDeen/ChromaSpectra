using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BertaTalking : MonoBehaviour, inventoryManager.Interact
{
    public TextAsset[] lines;
    public int lineNumber;
    public int progressOffset; 
 

    [Header ("TriggerEvent")]
    public GameObject puzzleManager;
    private dialogueManager dialogue;
    public int progressPoint;
    public string NameofFunction;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.FindWithTag("UI").GetComponent<dialogueManager>();
    }

    void Update()
    {
        lineNumber = GameManager.Progression - progressOffset;
        //so lines can move with progress easily subtract curent prgress from progress you got entering the room
    }
    public void interaction()
    {
        if(lineNumber >= lines.Length) { lineNumber = lines.Length - 1; }
        dialogue.startDialogue(lines[lineNumber], this.gameObject, null);
        //Debug.Log(lines[lineNumber]);

        if (progressPoint == GameManager.Progression) //This way we can invoke methods that move the game along so far it makes pie
        {
            Invoke("EventCountdown", delay); //delay it so not istant
            
        }
    }

    public void EventCountdown()
    {
        puzzleManager.SendMessage(NameofFunction); //by handing these duties to puzzlemanager can be different between scens
    }
}
