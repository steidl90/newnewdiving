using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public float duration;
    public bool isLife;

    private Vector3 distance;
    private Vector3 startPos;
    private float startTime;
    //private float endTime = 2f;
    
    private void Start()
    {
        startTime = Time.time;
        startPos = transform.position;
        isLife = true;
        distance = target.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        //if (isLife)
            TargetFollow();
    }

    public void TargetFollow()
    {
        //var sec = (Time.time - startTime) / duration;
        var sec = 5f * Time.deltaTime;
        var newPos = Vector3.Lerp(transform.position, target.transform.position - distance, sec);
        transform.position = newPos;
    }
    
    
}