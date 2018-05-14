using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class Evidence : ScriptableObject
{
    public string evidenceName;
    public string evidenceDescription;
    public Sprite evidenceImage;
    public string evidenceInformation;
    public Evidence scannedEvidence;
    public List<Evidence> linkedEvidence;
    internal bool amScanned = false;

#if UNITY_EDITOR
    // Simply press Play Buttonn in Editor to activate this.
    private void OnEnable()
    {
        for (int i = 0; i < linkedEvidence.Count; i++)
        {
            linkedEvidence[i].CheckLinks(this);
        }
    }

    void CheckLinks(Evidence checkEvidence)
    {
        bool amLinked = false;
        for (int i = 0; i < linkedEvidence.Count; i++)
        {
            if (linkedEvidence[i] == checkEvidence)
            {
                amLinked = true;
                break;
            }
        }

        if (!amLinked)
        {
            linkedEvidence.Add(checkEvidence);
        }
    }
#endif
}
