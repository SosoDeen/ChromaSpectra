using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMusicInteract : MonoBehaviour, inventoryManager.Interact
{
    public AudioClip newMusicChime;
    AudioSource source;
    Renderer renderer;
    float destroyTime;
    public void Start()
    {
        source = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
        destroyTime = newMusicChime.length;
    }
    public void interaction()
    {
        GameManager.songsUnlocked ++;
        source.PlayOneShot(newMusicChime);
        renderer.enabled = false;
        Destroy(gameObject, destroyTime);
    }
}
