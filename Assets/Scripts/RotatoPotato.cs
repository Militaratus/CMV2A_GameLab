using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatoPotato : MonoBehaviour {


    float speed = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.down * Time.deltaTime * speed);
	}

}
