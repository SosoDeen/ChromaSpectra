using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfoSO", order = 1)]

public class DialogueAudioInfoSO : ScriptableObject
{
    public string id;
    public AudioClip[] dialogueTypingSoundClips;
    public bool stopAudioSource; //keep one shot sounds from overlapping
    [Range(1,5)]
    public int frequencyLevel = 2; //word per sound frequency
    //randomize pitch
    [Range(-3, 3)]
    public float minPitch = 0.5f;
    [Range(-3, 3)]
    public float maxPitch = 3f;
}
