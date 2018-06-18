using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRoadMove : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
		if (transform.position.x < 79)
        {
            MoveRoad();
        }
        else
        {
            ResetRoad();
        }
	}

    void MoveRoad()
    {
        float xPos = transform.position.x;
        xPos++;
        transform.position = new Vector3(xPos, 0, 0);
    }

    void ResetRoad()
    {
        transform.position = new Vector3(-(79 * 2), 0, 0);
    }
}
