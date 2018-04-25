using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class MissionTV : MonoBehaviour {

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    private VideoPlayer videoPlayer;

    GameObject tv;

    float Timer = 30f;

    bool startTimer = true;

    void Start()
    {
       
        tv = transform.GetChild(0).gameObject;
        audioPlayer = GetComponent<AudioSource>();
        videoPlayer = tv.GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (startTimer == true)
        {
            Timer = Timer - Time.deltaTime;
        }

        if (Timer <= 0 && startTimer == true)
        {
            videoPlayer.Play();
            startTimer = false;
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = Bleep;
                audioPlayer.Play();
            }
        }
        
    }
}

