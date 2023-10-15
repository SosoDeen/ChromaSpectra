using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Animator anim;

    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        Image ig = GetComponent<Image>();
        ig.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("This scene " + sceneName);
    }
    public void NextLevelFade(string scene)
    {
        anim.SetTrigger("FadeOut");
        sceneName = scene;
    }
    public void LoadNextLevel()
    {
        Debug.Log("NextSceneLoading");
        SceneManager.LoadScene(sceneName);
    }

}
