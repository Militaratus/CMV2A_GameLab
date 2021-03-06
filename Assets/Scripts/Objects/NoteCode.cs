﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class NoteCode : VRTK_InteractableObject
{
    [Header("Code Note Options")]
    public string passcode;

    private Text element;

    protected override void Awake()
    {
        base.Awake();
        SetGUIKey();
    }

    // Apply the key text to the GUI of the button
    void SetGUIKey()
    {
        if (!element)
        {
            element = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        }

        element.text = passcode;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
