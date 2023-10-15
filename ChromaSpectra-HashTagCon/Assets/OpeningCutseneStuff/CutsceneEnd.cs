using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneEnd : MonoBehaviour
{
   // public GameObject UI;
    PlayableDirector director;
    public double visibleDelay;
    //public GameObject[] hidden;

    // Start is called before the first frame update
    void Start()
    {
        //UI.gameObject.SetActive(false);
        director = GetComponent<PlayableDirector>();
        /*
        for(int i = 0; i < hidden.Length; i++)
        {
            hidden[i].SetActive(false);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.anyKey) 
        {
            director.time = director.duration;
            ShowUI();
        }

        double showTime = visibleDelay;
        if (visibleDelay > director.duration) { showTime = director.duration; }
        if (Time.time > visibleDelay)
        {
            ShowUI();
        }
        else if(director.time == director.duration)
        {
            /*
            for (int i = 0; i < hidden.Length; i++)
            {
                hidden[i].SetActive(true);
            }
            ShowUI();
            */
        }
    }

    void ShowUI()
    {
        /*
        UI.gameObject.SetActive(true);
        Debug.Log("SceneEnd");
        */
    }
}
