using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnUse : MonoBehaviour, inventoryManager.ItemUser
{
    public GameObject[] objects;
    public string text = "I need to get rid of this";
    public int willProgress = 0;

    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");   
    }

    public void useItem()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        GameManager.Progression += willProgress;
        Destroy(this.gameObject);
    }
    public void interaction()
    {
        canvas.SendMessage("easyDialogue", text);
    }
}
