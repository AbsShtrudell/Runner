using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recruit : MonoBehaviour
{
    private float speed = 3;
    private float height = 0.1f;
    private Vector3 startPos;
    private bool destroyed = false;
    public GameObject child;
    [Zenject.Inject]
    private PawnsController pawnsController;

    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.Rotate(0, 0.2f, 0);
        float newY = Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(0, newY, 0) * height + startPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyed == false)
            if (other.GetComponent<Pawn>() != null)
            {
                pawnsController.Spawn(transform.position);
                GetComponent<ParticleSystem>().Play();
                child.SetActive(false);
                destroyed = true;
            }
    }
}
