using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Video;

public class TVSwitch : VRTK_InteractableObject
{

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    private VideoPlayer videoPlayer;

    GameObject tv;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        Debug.Log("Using");
        videoPlayer.Play();
        if (!audioPlayer.isPlaying)
        {
            audioPlayer.clip = Bleep;
            audioPlayer.Play();
        }
     }

    protected void Start()
    {
        tv = transform.GetChild(0).gameObject;
        audioPlayer = GetComponent<AudioSource>();
        videoPlayer = tv.GetComponent<VideoPlayer>();
    }
}
