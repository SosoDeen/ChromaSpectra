using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastLevelProgress : MonoBehaviour
{
    public int progressNum;
    public int songNum;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateValues", 1f);
    }

    public void UpdateValues()
    {
        GameManager.Progression = progressNum;
        GameManager.songsUnlocked = songNum;
    }
  
}
