using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Video;

public class ShowerOnOff : VRTK_InteractableObject
{

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    GameObject showerWater;

    int showerOff = 1;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        Debug.Log("Using");
        if (showerOff == 1)
        {
            showerWater.SetActive(true);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = Bleep;
                audioPlayer.Play();
            }
            showerOff = 0;
        }
        else if(showerOff == 0)
        {
            showerWater.SetActive(false);
            audioPlayer.Stop();
            showerOff = 1;
        }
    }

    protected void Start()
    {
        showerWater = transform.GetChild(0).gameObject;
        audioPlayer = GetComponent<AudioSource>();
    }
}
