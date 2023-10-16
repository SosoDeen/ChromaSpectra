using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIManager : MonoBehaviour
{
    public GameManager manager;
    public UIManager uIManager;
    public Sprite[] letterNotes;
    public Sprite blankSprite;
    private int noteCount = 0;
    public bool playUIActive = false;

    Color full = new Color(1f,1f,1f,1f);
    Color blank = new Color(1f, 1f, 1f, 0f);
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
        if (GameManager.isInPlayMode)
        {
            resetNotes();
        }
        playUIActive = GameManager.isInPlayMode;
    }
    public void addNoteSprite(int note)
    {
        Debug.Log("Adding " + note);
        if (noteCount >= 3)
        {
            resetNotes();

        }
        Debug.Log("Calling UI Manager");
        uIManager.updateNote(noteCount, letterNotes[note], full);
        noteCount++;
    }
    public void resetNotes()
    {
        for (int i = 0; i < 3; i++)
        {
            uIManager.updateNote(i, letterNotes[1], blank);
            noteCount = 0;
        }
    }
}
