using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLoading : MonoBehaviour
{
    // Update is called once per frame
    void Update ()
    {
        transform.position += (transform.right / 10);

        if (transform.position.x >= 20)
        {
            transform.position = new Vector3(-20, 1, transform.position.z);
        }
	}
}
