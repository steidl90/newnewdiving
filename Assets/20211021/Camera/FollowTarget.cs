using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    private Vector3 distance;

    public void init()
    {
        distance = target.transform.position - transform.position;
    }

    //public void Start()
    //{
    //    distance = new Vector3(0f, 5f, 2f);
    //}

    private void LateUpdate()
    {
        TargetFollow();
    }

    public void TargetFollow()
    {
        //var sec = (Time.time - startTime) / duration;
        Debug.Log(target.transform.position - transform.position);
        var sec = 5f * Time.deltaTime;
        var newPos = Vector3.Lerp(transform.position, target.transform.position - distance, sec);
        transform.position = newPos;
    }
    
    
}