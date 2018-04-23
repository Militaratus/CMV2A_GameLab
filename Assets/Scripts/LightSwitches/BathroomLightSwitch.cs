using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Video;

public class BathroomLightSwitch : VRTK_InteractableObject
{

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    GameObject Lights;

    int lightOff = 1;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        Debug.Log("Using");
        if (lightOff == 1)
        {
            Lights.SetActive(true);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = Bleep;
                audioPlayer.Play();
            }
            lightOff = 0;
        }
        else if (lightOff == 0)
        {
            Lights.SetActive(false);
            audioPlayer.Play();
            lightOff = 1;
        }
    }

    protected void Start()
    {
        Lights = transform.GetChild(0).gameObject;
        audioPlayer = GetComponent<AudioSource>();
    }
}
