using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PawnsController : MonoBehaviour
{
    [Zenject.Inject]
    private DrawController drawController;

    private List<Pawn> pawns = new List<Pawn>();

    [SerializeField]
    private int pawnsCount = 1;

    [SerializeField]
    private Pawn pawnRef;
    [SerializeField]
    private GameObject squad;

    private void OnEnable()
    {
        drawController.onPictureDrawn += OnLineDrawn;

        for(int i = 0; i < pawnsCount; i++)
        {
            pawns.Add(GameObject.Instantiate(pawnRef.gameObject, squad.transform).GetComponent<Pawn>());
            pawns[pawns.Count - 1].transform.localPosition = new Vector3(i * 0.5f - 2.5f, 0, 0);
            pawns[pawns.Count - 1].Move(pawns[pawns.Count - 1].transform.localPosition);
            pawns[pawns.Count - 1].onDead += DeattachPawn;
        }
    }

    private void DeattachPawn(Pawn obj)
    {
        pawns.Remove(obj);
        obj.transform.SetParent(null);
        obj.onDead -= DeattachPawn;

        if(pawns.Count == 0)
        {
            GetComponent<SplineFollower>().follow = false;
        }
    }

    private void AttachPawn(Pawn pawn)
    {
        pawn.transform.SetParent(transform);
        pawns.Add(pawn);
        pawns[pawns.Count - 1].onDead += DeattachPawn;
    }

    public void Spawn(Vector3 location)
    {
        Pawn pawn = GameObject.Instantiate(pawnRef);
        AttachPawn(pawn);
        pawn.transform.localPosition = new Vector3(transform.InverseTransformPoint(location).x, 0, transform.InverseTransformPoint(location).z);
        pawn.Move(pawn.transform.localPosition);
    }

    private void OnDisable()
    {
        drawController.onPictureDrawn -= OnLineDrawn;
    }

    private void OnLineDrawn(Vector2[] points)
    {
        float offset = VMath.CalculateOffset(VMath.CalculateLength(points), pawns.Count);

        SetOrder(CalculateOrder(points, offset));

    }

    private Vector2[] CalculateOrder(Vector2[] points, float offset)
    {
        List<Vector2> positions = new List<Vector2>();

        float leftover = 0;
        float distance;
        int counter = 0;

        for(int i = 0; i < points.Length - 1; i ++)
        {
            distance = Vector2.Distance(points[i], points[i + 1]);

            if (leftover == 0)
            {
                positions.Add(points[i]);
            }
            else 
            {
                if (distance <= leftover)
                {
                    leftover = Mathf.Abs(leftover - distance);
                    continue;
                }
                else
                {
                    positions.Add(VMath.GetSlidePoint(points[i], points[i + 1], leftover));
                    distance -= leftover;
                    leftover = 0;
                }
            }

            distance -= offset;

            counter = 0;
            while (distance > 0)
            {
                counter++;
                positions.Add(VMath.GetSlidePoint(points[i], points[i + 1], offset*counter));
                distance -= offset;             
            }
            leftover = Mathf.Abs(distance);
        } 

        return positions.ToArray();
    }

    private void SetOrder(Vector2[] positions)
    {
        for(int i = 0; i < positions.Length && i < pawns.Count; i++)
        {
            pawns[i].Move(new Vector3(positions[i].x / 150, 0, positions[i].y / 150));
        }
    }
}
