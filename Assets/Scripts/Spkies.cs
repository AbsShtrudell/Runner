using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spkies : MonoBehaviour
{
    [SerializeField]
    private GameObject Cone;

    private bool closed = false;
    private bool working = false;

    private void OnEnable()
    {
    }

    private void Update()
    {
        if(!working)
        {
            if(closed)
            {
                StartCoroutine(Wait());
            }
            else
            {
                StartCoroutine(Close());
            }
        }
    }

    IEnumerator Open()
    {
        while(Mathf.Abs(Cone.transform.localPosition.y - 0.034f) > 0.005f)
        {
            working = true;
            Cone.transform.Translate(0, 0.003f, 0);
            yield return new WaitForEndOfFrame();
        }
        Cone.transform.localPosition = new Vector3(0, 0.034f, 0);
        closed = false;
        working = false;
    }

    IEnumerator Close()
    {
        while (Mathf.Abs(Cone.transform.localPosition.y - (-0.564f)) > 0.01f)
        {
            working = true;
            Cone.transform.Translate(0, -0.004f, 0);
            yield return new WaitForEndOfFrame();
        }
        Cone.transform.localPosition = new Vector3(0, -0.564f, 0);
        closed = true;
        working = false;
    }

    IEnumerator Wait()
    {
        working = true;
        yield return new WaitForSeconds(3);
        StartCoroutine(Open());
    }
}
