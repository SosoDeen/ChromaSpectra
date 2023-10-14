using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allSongs : MonoBehaviour, inventoryManager.Interact
{
    public void interaction()
    {
        GameManager.songsUnlocked = 4;
    }
}
