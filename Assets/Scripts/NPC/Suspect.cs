using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Suspect : ScriptableObject
{
    public string suspectName;
    public string suspectDescription;
    public Sprite suspectImage;
    public Evidence[] suspectEvidence;
}
