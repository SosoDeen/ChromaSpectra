using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyColorChange : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Color == "RED")
        {
            spriteRenderer.color = Color.red;
        }
        else if (GameManager.Color == "GREEN")
        {
            spriteRenderer.color = Color.green;
        }
        else if (GameManager.Color == "BLUE")
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
