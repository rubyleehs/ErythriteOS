using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;

    void Update()
    {
        this.transform.rotation *= Quaternion.Euler(Vector3.forward * speed * Time.deltaTime);
    }
}
