using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class KeypadButtonClear : VRTK_InteractableObject
{
    private Keypad keypad;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        ClearKey();
    }

    void ClearKey()
    {
        if (!keypad)
        {
            keypad = transform.parent.GetComponent<Keypad>();
        }

        keypad.ClearKey();
    }
}
