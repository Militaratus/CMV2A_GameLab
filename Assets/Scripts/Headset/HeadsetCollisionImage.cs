using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class HeadsetCollisionImage : VRTK_HeadsetCollisionFade
{
    [Header("Salty Puppies - Custom Settings")]
    public Image fadeMaterial;

    protected override void OnHeadsetCollisionEnded(object sender, HeadsetCollisionEventArgs e)
    {
        base.OnHeadsetCollisionEnded(sender, e);
        fadeMaterial.color = Color.clear;
    }

    protected override void StartFade()
    {
        base.StartFade();
        fadeMaterial.color = Color.white;
    }
}
