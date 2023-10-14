using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressUp : MonoBehaviour, inventoryManager.Interact
{
    public void interaction()
    {
        GameManager.Progression++;
        Debug.Log("Progress is at " + GameManager.Progression);
    }
}
