using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicklesMoving : MonoBehaviour, inventoryManager.ItemUser
{
    public GameObject standHere;
    public GameObject glasses;
    public string text = "Pickles isn't interested in moving.";
    
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        glasses.SetActive(false);
        canvas = GameObject.FindGameObjectWithTag("UI");
    }

    public void useItem()
    {
        this.transform.position = standHere.transform.position;
        glasses.SetActive(true);
    }
    
    public void interaction()
    {
        canvas.SendMessage("easyDialogue", text);
    }
}
