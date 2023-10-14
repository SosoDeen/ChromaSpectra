using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameManager manager;
    public List<GameObject> Interactable = new List<GameObject>();
    public List<AudioClip> AudioClips = new List<AudioClip>();
    public List<AudioClip> AmbientSounds = new List<AudioClip>();

    void Start() {
        Debug.Log("Dictionary Test: "+ Interactable[0]);
        manager = GameManager.Instance;
        // assigns lists to game manager
        manager.allInteractables = Interactable;
        manager.allPickupSounds = AudioClips;
        manager.allAmbientSounds = AmbientSounds;
    }
}