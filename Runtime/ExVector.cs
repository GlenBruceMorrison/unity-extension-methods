using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExVector
{
    public static void X(this Transform transform, float value)
    {
        transform.position = new Vector3(value, transform.position.y, transform.position.z);
    }
}
