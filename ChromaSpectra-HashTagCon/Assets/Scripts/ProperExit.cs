using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProperExit : MonoBehaviour
{
    public GameObject[] NotNeeded;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(GameManager.lastScene == 4)
        {
            Player.transform.position = this.transform.position;
            Debug.Log("The illusion of a proper exit");
        }
        if(GameManager.Progression == 6)
        {
            for(int i = 0; i < NotNeeded.Length; i++)
            {
                Destroy(NotNeeded[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
