using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBEvidence : MonoBehaviour
{
    public Evidence myEvidence;

    // Use this for initialization
    public void AddEvidence (Evidence newEvidence)
    {
        myEvidence = newEvidence;

        Text myText = transform.GetChild(0).GetComponent<Text>();
        myText.text = myEvidence.evidenceName;

        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(PrepareEvidence);
    }

    public void PrepareEvidence()
    {
        Debug.Log("JARED!");
        Transform myMasterT = transform.parent.parent.parent;
        BleepBoardMaster myMaster = myMasterT.GetComponent<BleepBoardMaster>();
        myMaster.PrepareNewEvidence(myEvidence);
    }
}
