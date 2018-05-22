using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuToggle : MonoBehaviour {

    public VRTK_ControllerEvents controllerEvents;
    public GameObject menu;
    public GameObject menuFollower;

    bool menuState = true;

    // Use ts for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        controllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;
        menuFollower.GetComponent<VRTK_TransformFollow>().enabled = false;
    }

    void OnDisable()
    {
        controllerEvents.ButtonTwoReleased -= ControllerEvents_ButtonTwoReleased;
        menuFollower.GetComponent<VRTK_TransformFollow>().enabled = true;
    }

    private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        menuState = !menuState;
        menu.SetActive(menuState);
    }
}
