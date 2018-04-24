using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class BreakableObject : StandardObject
{   
    // Breakable variables
    internal GameObject modelParent;
    internal Transform shardParent;
    internal int shardCount;
    internal Vector3[] shardPositions;
    internal Quaternion[] shardRotations;

    // Apply the key text to the GUI of the button
    public override void SetReset()
    {
        base.SetReset();
        modelParent = transform.GetChild(1).gameObject;
        shardParent = transform.GetChild(2);
        shardCount = shardParent.childCount;
        shardPositions = new Vector3[shardCount];
        shardRotations = new Quaternion[shardCount];
        for (int i = 0; i < shardCount; i++)
        {
            shardPositions[i] = shardParent.GetChild(i).localPosition;
            shardRotations[i] = shardParent.GetChild(i).localRotation;
        }
        modelParent.SetActive(true);
        shardParent.gameObject.SetActive(false);
    }

    public override void ResetObject()
    {
        base.ResetObject();
        for (int i = 0; i < shardCount; i++)
        {
            shardParent.GetChild(i).localPosition = shardPositions[i];
            shardParent.GetChild(i).localRotation = shardRotations[i];
        }
        modelParent.SetActive(true);
        shardParent.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 4)
        {
            GetComponent<AudioSource>().Play();
            modelParent.SetActive(false);
            shardParent.gameObject.SetActive(true);
            //resetCoroutine = ResetTimer();
           //StartCoroutine(resetCoroutine);
        }

    }
}
