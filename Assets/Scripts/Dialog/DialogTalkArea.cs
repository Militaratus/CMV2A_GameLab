using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTalkArea : MonoBehaviour
{
    public Transform mahParent;
    DialogSystem myDialog;

	// Use this for initialization
	void Awake ()
    {
        mahParent = transform.parent.parent;
        myDialog = transform.parent.parent.GetComponent<DialogSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myDialog.PlayerEnter(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            myDialog.PlayerExit();
        }
    }
}
