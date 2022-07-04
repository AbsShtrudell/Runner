using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VMath
{
    public static float CalculateLength(Vector2[] points)
    {
        if (points.Length < 2) return 0;

        float length = 0f;

        for (int i = 0; i < points.Length - 1; i++)
        {
            length += Vector2.Distance(points[i], points[i + 1]);
        }

        return length;
    }

    public static float CalculateOffset(float length, int elementsCount)
    {
        return length / elementsCount;
    }

    public static Vector2 GetSlidePoint(Vector2 point1, Vector2 point2, float offset)
    {
        Vector2 result = new Vector2();

        float alpha = Vector2.Distance(point1, point2) / offset;

        result.x = (point1.x + alpha*point2.x) / (1 + alpha);
        result.y = (point1.y + alpha*point2.y) / (1 + alpha);

        return result;
    }
}
