using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBEvidence : MonoBehaviour
{
    public Evidence myEvidence;
    int buttonID;

    // Use this for initialization
    public void AddEvidence (Evidence newEvidence, int newID)
    {
        myEvidence = newEvidence;
        buttonID = newID;

        Text myText = transform.GetChild(0).GetComponent<Text>();
        myText.text = myEvidence.evidenceName;

        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(PrepareEvidence);
    }

    public void PrepareEvidence()
    {
        Transform myMasterT = transform.parent.parent.parent;
        BleepBoardMaster myMaster = myMasterT.GetComponent<BleepBoardMaster>();
        myMaster.PrepareNewEvidence(myEvidence, buttonID);
    }
}
