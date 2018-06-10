using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDishes : MonoBehaviour {

    MeshRenderer renderer;

    public Material materialClean;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.name == "Plate dirtyNEW")
        {
            renderer = col.transform.GetComponent<MeshRenderer>();
            renderer.material = materialClean;
            Debug.Log(col);
        }
        if (col.name == "Dirty Mug NEW")
        {
            renderer = col.transform.GetComponent<MeshRenderer>();
            renderer.material = materialClean;
            Debug.Log(col);
        }
    }
}
