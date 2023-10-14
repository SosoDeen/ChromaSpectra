using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateKeyScript : MonoBehaviour, inventoryManager.ItemUser
{
    public GateScript gateScript;
    public TextAsset text;

    GameObject canvas;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");
    }
    public void useItem()
    {
        gateScript.startRotate();
        Destroy(this.gameObject);
    }
    public void interaction()
    {
        canvas.SendMessage("easyDialogue", text);
    }



}
