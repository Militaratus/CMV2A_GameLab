using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BodyFollower : VRTK_TransformFollow
{
    protected override void SetPositionOnGameObject(Vector3 newPosition)
    {
        base.SetPositionOnGameObject(newPosition);

        transformToChange.position = newPosition + (Vector3.up * 1.08f);
    }
}
