using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    // Use this for initialization
    void Start () {
        audioPlayer = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.clip = Bleep;
            audioPlayer.Play();
        }
    }
}
