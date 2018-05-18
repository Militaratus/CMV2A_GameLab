using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BleepBoardTutorial : MonoBehaviour {

    public GameObject evidencePlaceText;
    public GameObject evidenceConnectClue;
    public GameObject evidenceDelete;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (BleepBoardMaster.placedClue == true)
        {
            evidencePlaceText.SetActive(false);
            evidenceConnectClue.SetActive(true);
        }
        if(BleepBoardMaster.connectedClue == true)
        {
            evidenceConnectClue.SetActive(false);
            evidenceDelete.SetActive(true);
        }
        if(BleepBoardMaster.deletedClue == true)
        {
            evidenceDelete.SetActive(false);
        }
    }
}
