using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BleepBloop : VRTK_InteractableObject
{
    public AudioClip Bleep;
    private AudioSource audioPlayer;

    GameObject canvas;

    public bool canvasActive = false;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        audioPlayer.clip = Bleep;
        audioPlayer.Play();
        if (!canvasActive)
        {
            canvasActive = true;
            canvas.SetActive(true);
        }
        else if (canvasActive)
        {
            Debug.Log("FUCK");
            canvasActive = false;
            canvas.SetActive(false);
        }
    }

    protected void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        canvas = transform.GetChild(0).gameObject;
    }
}
