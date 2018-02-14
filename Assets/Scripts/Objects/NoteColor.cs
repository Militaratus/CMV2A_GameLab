using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class NoteColor : VRTK_InteractableObject
{
    [Header("Color Note Options")]
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;

    private Image element1;
    private Image element2;
    private Image element3;
    private Image element4;

    protected override void Awake()
    {
        base.Awake();
        SetGUIColor();
    }

    // Apply the color to the GUI of the button
    void SetGUIColor()
    {
        if (!element1)
        {
            element1 = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        }
        if (!element2)
        {
            element2 = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        }
        if (!element3)
        {
            element3 = transform.GetChild(0).GetChild(2).GetComponent<Image>();
        }
        if (!element4)
        {
            element4 = transform.GetChild(0).GetChild(3).GetComponent<Image>();
        }

        element1.color = color1;
        element2.color = color2;
        element3.color = color3;
        element4.color = color4;
    }
}
