using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieInteract : MonoBehaviour, inventoryManager.Interact 
{
    // Start is called before the first frame update
    public TextAsset text;
    public GameObject speaker1;
    public GameObject speaker2;

    private dialogueManager dialogue;

    void Start()
    {
        dialogue = GameObject.FindWithTag("UI").GetComponent<dialogueManager>();
    }

    public void interaction()
    {
        dialogue.startDialogue(text, speaker1, speaker2);
    }

}
