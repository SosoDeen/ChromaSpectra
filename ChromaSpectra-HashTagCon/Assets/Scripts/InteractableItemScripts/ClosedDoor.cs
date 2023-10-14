using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
    public int progressAfter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Progression >= progressAfter)
        {
            //open the door but I'm just deleting it for now
            Destroy(gameObject);
        }
    }
}
