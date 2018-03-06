using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BleepBloop : VRTK_InteractableObject
{
    public AudioClip Bleep;
    private AudioSource audioPlayer;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        Debug.Log("Triggered");
        audioPlayer.clip = Bleep;
        audioPlayer.Play();
    }

    protected void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
}
