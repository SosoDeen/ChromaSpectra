using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIManager : MonoBehaviour
{
    public GameManager manager;
    public UIManager uIManager;
    public Sprite[] letterNotes;
    private int noteCount = 0;
    public bool playUIActive = false;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.FindWithTag("UI").GetComponent<UIManager>();
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePlayUI()
    {
        noteCount = 0;
        uIManager.musicToggle(GameManager.isInPlayMode);
        playUIActive = GameManager.isInPlayMode;
    }
}
