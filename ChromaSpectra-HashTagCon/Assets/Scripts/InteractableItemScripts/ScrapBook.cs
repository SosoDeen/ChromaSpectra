using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrapBook : MonoBehaviour, inventoryManager.ItemUser
{
    public string text;
    GameObject canvas;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");
    }
   public void interaction()
    {
        canvas.SendMessage("easyDialogue", text);
    }

    public void useItem()
    {
        Debug.Log("Congrats you have beaten the game");
        Destroy(GameObject.FindGameObjectWithTag("SoundManager"));
        
        SceneTransition transition = FindObjectOfType<SceneTransition>();
        transition.NextLevelFade("EndAndCredits");
    }
}
