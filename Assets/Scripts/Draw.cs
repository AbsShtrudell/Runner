using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(UILineRenderer))]
public class Draw : MonoBehaviour
{
    private UILineRenderer lineRenderer;
    private bool cleared = true;

    private Vector2[] Points
    {
        get { return lineRenderer.Points; } set { lineRenderer.Points = value; } 
    }

    private void Start()
    {
        lineRenderer = GetComponent<UILineRenderer>();   
    }

    public void SetPoint(Vector2 point)
    {
        if (cleared) { Points = new Vector2[0]; cleared = false; }

        List<Vector2> points = new List<Vector2>(Points);
        points.Add(point);
        Points = points.ToArray();
    }
    
    public Vector2[] Clear()
    {
        Vector2[] points = Points;
        Points = new Vector2[2];
        cleared = true;
        return points;
    }
}
