using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptText : MonoBehaviour
{
    public string passage; // the text content of the TextAsset
    public string[] sentenceList; // array of sentences from the passage

    public string[] adaptText(TextAsset currentTxt)
    {
        passage = convertTxt(currentTxt);
        sentenceList = splitText(passage);
        return sentenceList;
    }

    // converts text document to string
    public string convertTxt(TextAsset currentTxt)
    {
        return currentTxt.text;
    }

    // converts text to array of sentence strings
    public string[] splitText(string passage)
    {
        string[] sentences = passage.Split('*');
        return sentences;
    }
}