using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using UnityEngine.Analytics;

public class BreakableObject : StandardObject
{   
    // Breakable variables
    internal GameObject modelParent;
    internal Transform shardParent;
    internal Transform[] shardPopulation;
    internal Vector3[] shardPositions;
    internal Quaternion[] shardRotations;

    internal override void ExtraAwake()
    {
        base.ExtraAwake();

        // Get Variables
        modelParent = transform.GetChild(0).gameObject;
        shardParent = transform.GetChild(1);

        // Get the local population in line
        shardPopulation = new Transform[shardParent.childCount];
        for (int i = 0; i < shardPopulation.Length; i++)
        {
            shardPopulation[i] = shardParent.GetChild(i);
        }

        // Hide Shards
        ShowShards(false);
    }

    // Apply the key text to the GUI of the button
    internal override void SetReset()
    {
        base.SetReset();

        // Get Variables
        shardParent = transform.GetChild(1);
        shardPositions = new Vector3[shardPopulation.Length];
        shardRotations = new Quaternion[shardPopulation.Length];

        // Populate Respawn list
        for (int i = 0; i < shardPopulation.Length; i++)
        {
            shardPositions[i] = shardPopulation[i].localPosition;
            shardRotations[i] = shardPopulation[i].localRotation;
        }
    }

    internal override void ResetObject()
    {
        base.ResetObject();
        for (int i = 0; i < shardPopulation.Length; i++)
        {
            shardPopulation[i].parent = shardParent;
            shardPopulation[i].localPosition = shardPositions[i];
            shardPopulation[i].localRotation = shardRotations[i];
        }
        ShowShards(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 4)
        {
            GetComponent<AudioSource>().Play();
            ShowShards();
            Analytics.CustomEvent("Broken");

            if (canRespawn)
            {
                resetCoroutine = ResetTimer();
                StartCoroutine(resetCoroutine);
            }
        }

    }

    void ShowShards(bool bShow = true)
    {
        BoxCollider grabCollider = GetComponent<BoxCollider>();
        if (bShow)
        {
            grabCollider.enabled = false;
            modelParent.SetActive(false);
            shardParent.gameObject.SetActive(true);

            // Unleash the horde!
            for (int i = 0; i < shardPopulation.Length; i++)
            {
                shardPopulation[i].parent = null;
            }
        }
        else
        {
            grabCollider.enabled = true;
            modelParent.SetActive(true);
            shardParent.gameObject.SetActive(false);
        }
    }
}
