using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AddItemToBed : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // In case positioned objects collide with this script
        StandardObject so = other.GetComponent<StandardObject>();
        if (so != null && other.transform.parent != transform)
        {
            if (so.IsGrabbed() == true)
            {
                // I am in use, do not go past GO!
                return;
            }

            // Set the neccessary settings
            //so.UseGravity(false);
            other.transform.parent = transform;
        }
    }
}
