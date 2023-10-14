using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnUse : MonoBehaviour, inventoryManager.ItemUser
{
    public GameObject item;
    public string text = "Something goes here.";

    GameObject canvas;
    void Start()
    {
        item.SetActive(false);
        canvas = GameObject.FindGameObjectWithTag("UI");
    }
    public void useItem()
    {
        item.SetActive(true);
        Destroy(this.gameObject);
    }

    public void interaction()
    {
        canvas.SendMessage("easyDialogue", text);
    }
}
