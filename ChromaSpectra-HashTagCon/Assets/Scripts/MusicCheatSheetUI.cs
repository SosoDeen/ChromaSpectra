using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MusicCheatSheetUI : MonoBehaviour
{
    List<GameObject> musicPieces = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            musicPieces.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameManager.songsUnlocked; i++)
        {
            musicPieces[i].SetActive(true);
        }
    }
}
