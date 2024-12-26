using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Utility
{
    public static Vector2 SetXValue(this Vector2 vector, float xValue)
    {
        vector.x = xValue;
        return vector;
    }
}
