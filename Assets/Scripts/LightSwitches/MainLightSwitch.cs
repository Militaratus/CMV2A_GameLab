using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Video;

public class MainLightSwitch : VRTK_InteractableObject
{

    public AudioClip Bleep;
    private AudioSource audioPlayer;

    GameObject Lights;

    int lightOff = 0;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        Debug.Log("Using");
        if (lightOff == 0)
        {
            Lights.SetActive(false);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = Bleep;
                audioPlayer.Play();
            }
            lightOff = 1;
        }
        else if (lightOff == 1)
        {
            Lights.SetActive(true);
            audioPlayer.Play();
            lightOff = 0;
        }
    }

    protected void Start()
    {
        Lights = transform.GetChild(0).gameObject;
        audioPlayer = GetComponent<AudioSource>();
    }
}
