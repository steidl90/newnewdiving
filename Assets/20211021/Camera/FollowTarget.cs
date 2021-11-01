using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject targetModel;
    public GameObject targetRagdoll;
    public float distance = 10f;

    public void Start()
    {
        target = targetModel;
    }

    private void LateUpdate()
    {
        var isSuccess = GameManager.gameManager.player.GetComponent<PlayerController>().isSuccess;
        if (!target.activeSelf && !isSuccess)
        {
            target = targetRagdoll;
        }
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