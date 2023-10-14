using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public GameManager manager;
    public AudioClip[] bgmSelection; //JEN: array of the different emotions that can be played

    [Header("Mixers")]
    public AudioMixer bgmMixer;
    public AudioMixer sfxMixer;

    [Header("Audio Sources")]
    public AudioSource bgmColor; //Jen: audio source for bgmColor
    public AudioSource bgmAmbience;
    public AudioSource sfxAmbience;
    public AudioSource sfxInteractible;
    public AudioSource sfxPlayer;

    public static SoundManager Instance;

    //TODO: Audio fading between playmode and non playmode

    private AudioMixerSnapshot[] snapshots;

    // Start is called before the first frame update
    void Start()
    {

        // init game manager
        manager = GameManager.Instance;

        //aquire snapshots from the mixer
        snapshots = new AudioMixerSnapshot[2];

        snapshots[0] = bgmMixer.FindSnapshot("PlayMode");
        snapshots[1] = bgmMixer.FindSnapshot("Main");


        Debug.Log("Halo from DA Sound Manager :)");

        //for now
        bgmColor.loop = true;
        //bgmColor.clip = bgmSelection[0]; //play claire theme on start
        setBGM(GameManager.Color);
        bgmColor.Play();
    }

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(this.gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;

        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(this.gameObject); // Destroy the GameObject, this component is attached to
        }

    }

    // Update is called once per frame
    void Update()
    {

        //if in playmode -> fade out whatever is playing and just have the ambient track running
        //when song is finished can changetrack is set and a new song is started

        if (GameManager.PublicPlayMode == true)
        {
            //AudioMixerSnapshot playSnap = bgmMixer.FindSnapshot("PlayMode");
            //bgmMixer.TransitionToSnapshots(snapshots, null, 3);
            snapshots[0].TransitionTo(1);

            //check if we can change track
            if (GameManager.CanChangeTrack == true)
            {
                Debug.Log("DA Sound Manager: We can change songs");
                setBGM(GameManager.Color);
                GameManager.CanChangeTrack = false; //immediately reset flag so song can play
            }

        }
        else {
            snapshots[1].TransitionTo(1);
        }
    }

    private void setBGM(string clipName)
    {
        if (clipName == "GREY")
        {
            Debug.Log("DA Sound Manager: yo we grey");
            bgmColor.clip = bgmSelection[0];
            bgmColor.Play();
        }
        else if (clipName == "RED")
        {
            Debug.Log("DA Sound Manager: yo we red");
            bgmColor.clip = bgmSelection[3];
            bgmColor.Play();
        }
        else if (clipName == "GREEN")
        {
            Debug.Log("DA Sound Manager: yo we green");
            bgmColor.clip = bgmSelection[1];
            bgmColor.Play();
        }
        else if (clipName == "BLUE")
        {
            Debug.Log("DA Sound Manager: yo we blue");
            bgmColor.clip = bgmSelection[2];
            bgmColor.Play();
        }
    }

    public void playSFX(int datCoolTrack){
        Debug.Log("DA Sound Manager: we playin sfx now too. Index: " +datCoolTrack );
        if (datCoolTrack >= 0 && manager.allPickupSounds[datCoolTrack] != null){
            Debug.Log("sfx name: " + manager.allPickupSounds[datCoolTrack]);
            sfxInteractible.clip = manager.allPickupSounds[datCoolTrack];
            sfxInteractible.Play();
        }
    }
}
