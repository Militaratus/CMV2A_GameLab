using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class KeypadButton : VRTK_InteractableObject
{
    [Header("Key Button Options")]
    public string key;

    private Keypad keypad;
    private Text element;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        AddKey();
    }

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

        element.text = key;
    }

    void AddKey()
    {
        if (!keypad)
        {
            keypad = transform.parent.GetComponent<Keypad>();
        }

        keypad.AddKey(key);
    }
}
