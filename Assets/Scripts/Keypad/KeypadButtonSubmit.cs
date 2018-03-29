using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class KeypadButtonSubmit : VRTK_InteractableObject
{
    private Keypad keypad;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        Debug.Log("Pressed");
        base.StartUsing(usingObject);
        SubmitKey();
    }

    void SubmitKey()
    {
        if (!keypad)
        {
            keypad = transform.parent.GetComponent<Keypad>();
        }

        keypad.SubmitKey();
    }
}
