using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWallHide : MonoBehaviour
{
    public GameObject wall;
    public GameObject doorFrame;

    Renderer render;
    SpriteRenderer spRender;
    public void Start()
    {
        render = wall.GetComponent<Renderer>();
        spRender = doorFrame.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        { 
            render.enabled = false;
            spRender.color = new Color(1f, 1f, 1f, 0.5f);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        { 
            render.enabled = true;
            spRender.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
