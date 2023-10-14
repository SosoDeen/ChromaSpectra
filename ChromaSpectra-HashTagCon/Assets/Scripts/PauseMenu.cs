using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image controls;
    public Button backButton;

    static bool showCon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }

        controls.gameObject.SetActive(showCon);  
        backButton.gameObject.SetActive(showCon);
    }

    public static void Resume()
    {
        Time.timeScale = 1.0f;
        SceneManager.UnloadSceneAsync("PauseMenu");
        
        Debug.Log("Should go away");
    }
    public static void QuitGame()
    {
        Application.Quit();
        Debug.Log("Should Quit");
    }
    public static void ShowControls()
    {
        Debug.Log("ShowControls");
        showCon = true; 
    }
    public static void CloseControls()
    {
        Debug.Log("Hide");
        showCon = false;
    }
    public void ReturnToMain()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        if (gm != null)
        {
            GameManager.songsUnlocked = 0;
            GameManager.Progression = 0;
            GameManager.Color = "GREY";
            GameManager.inCinematic = false;
            Destroy(gm.gameObject);
        }
        SoundManager sm = FindObjectOfType<SoundManager>();
        if (sm != null) { Destroy(sm.gameObject); }
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        Debug.Log("MainMenu");
    }
}
