using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTarget : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Color startColor;
    private float timer;

    private void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(0f, -7f, 0f);
        startColor = GetComponent<MeshRenderer>().material.color;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f)
        {
            timer = 0f;
            transform.position = startPos;
            GetComponent<MeshRenderer>().material.color = startColor;
        }
        transform.position = Vector3.Lerp(transform.position, startPos + endPos, Time.deltaTime * 5f);
        var color = GetComponent<MeshRenderer>().material.color;
        color.a -= Time.deltaTime;
        GetComponent<MeshRenderer>().material.color = color; 
    }
}
