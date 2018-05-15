using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTutorial : MonoBehaviour {

    public GameObject Dialog;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(DialogSystem.Dialog == false)
        {
            Dialog.SetActive(false);
        }
	}
}
