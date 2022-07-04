using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Pawn>() != null)
        {
            other.gameObject.GetComponent<Pawn>().Dead();
        }
    }
}
