using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRespondText : MonoBehaviour, inventoryManager.Interact
{
    public TextAsset text;
    public string sentence;

    GameObject canvas;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");
    }
    public void interaction()
    {
        if(text != null)
        {
            canvas.SendMessage("easyDialogue", text);
        }
        else { canvas.SendMessage("easyDialogue", sentence); }
    }
    
}
