using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glassesScript : MonoBehaviour
{
    private GameObject canvas;
    public string text;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI");
    }

    // Start is called before the first frame update
    private void OnDestroy()
    {
        canvas.SendMessage("easyDialogue", text);
    }
}
