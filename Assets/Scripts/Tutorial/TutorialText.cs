using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

    public GameObject Teleport;
    public GameObject PickUP;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(VRTK.VRTK_ControllerEvents.TeleportActive == false)
        {
            Teleport.SetActive(false);
            PickUP.SetActive(true);
        }
        if(StandardObject.PickUPActive == false && VRTK.VRTK_ControllerEvents.TeleportActive == false)
        {
            PickUP.SetActive(false);
        }
	}
}
