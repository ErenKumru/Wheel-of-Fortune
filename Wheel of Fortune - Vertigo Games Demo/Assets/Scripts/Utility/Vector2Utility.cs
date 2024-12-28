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

    public static Vector2 SetYValue(this Vector2 vector, float yValue)
    {
        vector.y = yValue;
        return vector;
    }

    public static Vector2 ResizeHeightByTextureSize(this Vector2 vector, Texture2D texture)
    {
        float height = texture.height * vector.x / texture.width;
        vector = SetYValue(vector, height);
        return vector;
    }

    public static Vector2 ResizeWidthByTextureSize(this Vector2 vector, Texture2D texture)
    {
        float width = texture.width * vector.y / texture.height;
        vector = SetXValue(vector, width);
        return vector;
    }
}
