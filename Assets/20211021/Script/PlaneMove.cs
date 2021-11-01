using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public float speed = 15f;
    public GameObject town;
    public bool isCollision;
    private Vector3 StartPos;
    private Quaternion pot;
    private int timer;
    
    private void Start()
    {
        StartPos = transform.position;
        town = transform.parent.parent.gameObject;
        pot = town.transform.localRotation * transform.localRotation;
        isCollision = false;
    }
    private void FixedUpdate()
    {
        var isReplay = transform.parent.GetComponent<Replay>().isReplay;
        //var isCollision = transform.parent.GetComponent<Replay>().;
        if (!isReplay && !isCollision)
        {
            if (timer < Time.time)
            {
                transform.position = StartPos;
                timer = (int)Time.time + 10;
                GetComponent<AudioSource>().Play();
            }
            transform.position += pot * Vector3.forward * speed * Time.deltaTime; 
        }
    }
}
