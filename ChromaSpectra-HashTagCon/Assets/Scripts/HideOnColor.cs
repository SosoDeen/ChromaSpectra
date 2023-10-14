using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnColor : MonoBehaviour
{
    public string[] viewColors;
    public bool ChangeSpriteColor;
    public bool TurnOffCollisons;

    bool foundColor;
    string currColor;
    Renderer render;
    Collider collider;

    SpriteRenderer spriteRenderer;
   
    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        currColor = GameManager.Color;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<Collider>(); 

        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        if (ChangeSpriteColor && spriteRenderer != null) { SpriteColorChanger(); }
        if (TurnOffCollisons && collider != null) { ChangeCollider(); }
    }

    void ChangeColor() 
    {
        foundColor = false;
        for (int i = 0; i < viewColors.Length; i++)
        {
            if (viewColors[i] == GameManager.Color)
            {
                render.enabled = true;
                foundColor = true;
            }
        }
        if (!foundColor) { render.enabled = false; }
    }

    void SpriteColorChanger()
    {
        if(GameManager.Color == "RED")
        {
            spriteRenderer.color = Color.red;
        }
        else if(GameManager.Color == "GREEN")
        {
            spriteRenderer.color = Color.green;
        }
        else if(GameManager.Color == "BLUE")
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.color = Color.white; 
        }
    }

    void ChangeCollider()
    {
        if(foundColor) { collider.enabled = true; } 
        else { collider.enabled = false; }  
    }
}
