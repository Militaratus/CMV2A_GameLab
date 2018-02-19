using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Breakable : VRTK_InteractableObject
{
    Vector3 resetPosition;
    Quaternion resetRotation;

    int shardCount;
    public Vector3[] shardPositions;
    public Quaternion[] shardRotations;

    IEnumerator resetCoroutine;

    protected override void Awake()
    {
        base.Awake();
        SetReset();
        ResetObject();
    }

    // Apply the key text to the GUI of the button
    void SetReset()
    {
        resetPosition = transform.position;
        resetRotation = transform.rotation;

        shardCount = transform.GetChild(0).childCount;
        shardPositions = new Vector3[shardCount];
        shardRotations = new Quaternion[shardCount];
        for (int i = 0; i < shardCount; i++)
        {
            shardPositions[i] = transform.GetChild(0).GetChild(i).localPosition;
            shardRotations[i] = transform.GetChild(0).GetChild(i).localRotation;
        }
    }

    void ResetObject()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        transform.position = resetPosition;
        transform.rotation = resetRotation;
        for (int i = 0; i < shardCount; i++)
        {
            transform.GetChild(0).GetChild(i).localPosition = shardPositions[i];
            transform.GetChild(0).GetChild(i).localRotation = shardRotations[i];
        }
        GetComponent<MeshRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            //audioSource.Play();
            Debug.Log("BOOM!");
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            resetCoroutine = ResetTimer();
            StartCoroutine(resetCoroutine);
        }

    }

    public IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(3.0f);
        ResetObject();
    }
}
