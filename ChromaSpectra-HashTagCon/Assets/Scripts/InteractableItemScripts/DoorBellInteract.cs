using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBellInteract : MonoBehaviour, inventoryManager.Interact, inventoryManager.ItemUser
{
    
    public AudioClip doorBellRing;
    public GameObject rope;
    public string text;

    AudioSource bell;
    GameObject canvas;
    bool complete = false;

    void Start()
    {
        bell = GetComponent<AudioSource>();
        rope.gameObject.SetActive(false);
        canvas = GameObject.FindGameObjectWithTag("UI");
    }
   public void interaction()
    {
        if(!complete)
        {
            canvas.SendMessage("easyDialogue", text);
            Debug.Log("The DoorBell is missings its ROPE. I can't ring it.");
        }
        else
        {
            bell.PlayOneShot(doorBellRing);
            if(GameManager.Progression < 1) { GameManager.Progression++; }
        }
        
    }
    public void useItem()
    {
        complete = true;
        rope.gameObject.SetActive(true);
    }

}
