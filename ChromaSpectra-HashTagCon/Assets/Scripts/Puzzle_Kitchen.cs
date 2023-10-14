using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Puzzle_Kitchen : MonoBehaviour
{
    public GameObject[] ingredients;
    public GameObject pie;

    public TextAsset text;
    public GameObject speaker1;
    public GameObject speaker2;

    public GameObject OvenPie;
    private dialogueManager dialogue;

    bool allIngredients = false;
    bool readyforPie = false;
    bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        pie.SetActive(false);
        dialogue = GameObject.FindWithTag("UI").GetComponent<dialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredients != null)
        {
            if (ingredients[0].gameObject.active && ingredients[1].gameObject.active && ingredients[2].gameObject.active && !allIngredients)
            {
                allIngredients = true;
                GameManager.Progression++;
            }

            if (allIngredients && GameManager.Color == "RED" && !readyforPie)
            {
                GameManager.Progression++;
                readyforPie = true;
            }
        }
        
        if(OvenPie.active && (GameManager.Color == "GREY" || GameManager.Color == "RED") && !finished)
        {
            finished = true;
            dialogue.startDialogue(text, speaker1, speaker2);
            GameManager.Progression++;
        }
        
    }
    public void MakeThePie()
    {
        for (int i = ingredients.Length -1; i >= 0; i--)
        {
            ingredients[i].SetActive(false);
        }
        pie.SetActive(true);
    }
}
