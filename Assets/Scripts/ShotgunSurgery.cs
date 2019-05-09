using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSurgery : MonoBehaviour
{
    public void ForceStartCoroutine(IEnumerator coroutine)
    {
        if (coroutine == null) return;
        StartCoroutine(coroutine);
    }

    public void ForceStopCoroutine(IEnumerator coroutine)
    {
        if (coroutine == null) return;
        StopCoroutine(coroutine);
    }
}
