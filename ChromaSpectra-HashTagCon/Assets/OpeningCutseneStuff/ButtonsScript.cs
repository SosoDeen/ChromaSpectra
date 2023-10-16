using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public Image controls;
    bool showCon;

    private void Start()
    {
        if(controls != null)
        {
            showCon = false;
            controls.enabled = showCon;
        }
        
    }

    public void GameStart()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if (gm != null)
        {
            GameManager.songsUnlocked = 0;
            GameManager.Progression = 0;
            GameManager.Color = "GREY";
            Destroy(gm.gameObject);
        }
        SceneManager.LoadScene(1);
        Debug.Log("START GAME");
    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("Quit the Game");
    }
    
    public void ReturnToMain()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if (gm != null)
        {
            GameManager.songsUnlocked = 0;
            GameManager.Progression = 0;
            GameManager.Color = "GREY";
            Destroy(gm.gameObject);
        }
        SceneManager.LoadScene(0);
        Debug.Log("MainMenu");
    }

    public void ShowControls()
    {
        showCon = !showCon;
        controls.enabled = showCon;
    }
}
