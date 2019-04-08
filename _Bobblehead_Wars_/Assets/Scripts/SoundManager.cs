using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    //sound manager for bobblehead wars
    public static SoundManager Instance = null;//want an instacne that can be access from other scripts
    //all the audio clips
    public AudioClip gunfire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpSpawn;

    //need to get the audio source for the sound effect
    private AudioSource effectSound;
	// Use this for initialization
	void Start () {
        //make sure there is only one sound manager
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
        }

        //get the audio source that is for sound effects, this is the source that is null in inspector
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach(AudioSource source in sources)
        {
            if(source.clip == null)
            {
                //this is the sound effect source
                effectSound = source;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //play a sound effect
    public void PlayOneShot(AudioClip clip)
    {
        effectSound.PlayOneShot(clip);
    }
}
