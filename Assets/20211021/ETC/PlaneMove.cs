using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed;

    public void Update()
    {
        transform.position += (transform.localRotation * Vector3.forward) * speed * Time.deltaTime;
    }
}
