using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPopup;
    //public String tutorialMessage;
    public KeyCode taskKey;
    private Boolean touchingLol;
    private Boolean shown;
    private Boolean taskComplete;
    //public Vector3 textOffset = new Vector3(0, 1.3f, 0);

    // Start is called before the first frame update
    void Start()
    {
        shown = false;
        taskComplete = false;
        tutorialPopup.SetActive(false);
        //tutorialPopup.GetComponent<TextMesh>().text = tutorialMessage;
        //tutorialPopup.transform.localPosition += textOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(taskKey) && touchingLol){
            taskComplete = true;
        }
    }
    
    public void OnCollisionEnter(Collision other){
        touchingLol = true;

        if(!taskComplete && !shown){
            showPopup();
            shown = true;
            Debug.Log("not task not show -> now show");
        }

        if (taskComplete){
            //tutorialPopup.GetComponent<TutorialPopup>().destroy();
            //Destroy(tutorialPopup);
            hidePopup();
            Debug.Log("task -> now hide");
        }
    }

    public void OnCollisionExit(Collision other){
        touchingLol = false;
        hidePopup();
        Debug.Log("yur so far awaaay -> no show");
        //Debug.Log("haha BEGONE!");
    }

    void showPopup(){
        //Instantiate(tutorialPopup, transform.position + textOffset, Quaternion.identity, transform);
        tutorialPopup.SetActive(true);
        shown = true;
        Debug.Log("we shine now");
    }

    void hidePopup(){
        tutorialPopup.SetActive(false);
        shown = false;
        Debug.Log("we hide now");
    }

}
