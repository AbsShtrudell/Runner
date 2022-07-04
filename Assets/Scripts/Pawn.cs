using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Pawn : MonoBehaviour
{
    private Vector3 position = Vector3.zero;
    private int speed = 5;

    private Vector3 pos = Vector3.zero;

    public event Action<Pawn> onDead;
    void Start()
    {
        
    }

    void Update()
    {
        if (Vector3.Distance(position, transform.localPosition) > 0.1)
        {
            Vector3 direction = Vector3.MoveTowards(transform.localPosition, position, speed * Time.deltaTime);
            transform.localPosition = direction;
        }

        if (pos != transform.position)
        {
            GetComponent<Animator>().SetBool("Run", true);
        }
        else GetComponent<Animator>().SetBool("Run", false);
        pos = transform.position;
    }

    public void Move(Vector3 point)
    {
        position = point;
    }

    public void Dead()
    {
        onDead?.Invoke(this);
        GetComponent<Animator>().SetTrigger("Death");
        Move(transform.localPosition);
    }
}
