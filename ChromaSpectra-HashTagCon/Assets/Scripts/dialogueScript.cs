using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueScript : MonoBehaviour, dialogueManager.Dialogue
{
    // Start is called before the first frame update
    public TextAsset text;
    public GameObject spkr1;
    public GameObject spkr2;

    public TextAsset getDialogue()
    {
        return text;
    }

    public GameObject getSpeaker1()
    {
        return spkr1;
    }

    public GameObject getSpeaker2()
    {
        return spkr2;
    }
}
