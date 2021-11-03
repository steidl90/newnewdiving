using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public GameObject targetModel;
    public GameObject targetRagdoll;
    public float distance = 10f;
    private float timer = 0f;
    public Vector3 pos;

    public bool isFinish = false;

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
        if (isFinish)
            FinishRotation();
        else
            TargetFollow();
    }
    public void TargetFollow()
    {
        var sec = 10f * Time.deltaTime;
        var targetPos = target.transform.position + -transform.forward * distance;
        var newPos = Vector3.Lerp(transform.position, targetPos, sec);
        transform.position = newPos;
    }

    public void FinishRotation()
    {
        //var sec = 10f * Time.deltaTime;
        //var targetPos = target.transform.position + -transform.forward * distance;
        //var newPos = Vector3.Lerp(transform.position, targetPos, sec);
        //transform.position = newPos;
        timer += Time.deltaTime;
        if (timer < 0.5f)
        {
            transform.LookAt(pos);
            transform.RotateAround(pos, -Vector3.up, 360f * Time.deltaTime);
        }
        else if(timer > 0.5f)
        {
            target.SetActive(true);
            target.GetComponent<Animator>().SetTrigger("Finish");
            var CoVictory = target.transform.parent.GetComponent<PlayerController>().CoVictory();
            StartCoroutine(CoVictory);
            isFinish = false;
        }
    }


}