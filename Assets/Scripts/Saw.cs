using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{

    private float speed = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Pawn>() != null)
        {
            other.gameObject.GetComponent<Pawn>().Dead();
        }
    }


    void Update()
    {
        transform.Rotate(0 ,0 , speed);
    }
}
