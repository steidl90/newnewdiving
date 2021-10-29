using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed = 15f;
    public GameObject town;
    private Vector3 StartPos;
    private Quaternion pot;
    private int timer;
    private void Start()
    {
        StartPos = transform.position;
        timer = (int)Time.time;
        town = transform.parent.parent.gameObject;
        pot = town.transform.localRotation * transform.localRotation;

    }
    private void FixedUpdate()
    {
        var isReplay = transform.parent.GetComponent<Replay>().isReplay;
        if (!isReplay)
        {
            if (timer + 15 <= Time.time)
            {
                transform.position = StartPos;
                timer = (int)Time.time;
            }
            transform.position += pot * Vector3.forward * speed * Time.deltaTime; 
        }
    }
}
