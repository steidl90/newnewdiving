using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public float distance = 10f;

    public void Start()
    {
    }

    private void LateUpdate()
    {
        TargetFollow();
    }
        
    public void TargetFollow()
    {
        //var sec = (Time.time - startTime) / duration;
        var sec = 5f * Time.deltaTime;
        var targetPos = target.transform.position + -transform.forward * distance;
        var newPos = Vector3.Lerp(transform.position, targetPos, sec);
        transform.position = newPos;
    }
    
    
}