using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    public bool isReplay = false;
    public List<PointInTime> pointsInTime;
    private Rigidbody rigid;
    public bool ragdollInit = true;
    public bool IsDiving { get; set; } = false;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isReplay)
        { 
            OnReplay();
        }
        else if (IsDiving)
        { 
            Record();
        }
    }

    private void OnReplay()
    {
        if (pointsInTime.Count != 0)
        {
            PointInTime pointInTime = pointsInTime[pointsInTime.Count - 1];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
    }

    private void Record()
    {
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartReplay()
    {
        isReplay = true;
        rigid.isKinematic = true;
        IsDiving = false;
    }
    public void StopReplay()
    {
        isReplay = false;
        rigid.isKinematic = false;
        IsDiving = false;
    }

    //private void CheakReplay()
    //{
    //    if (isStopTime + 1f < Time.time)
    //    {
    //        if (Mathf.Abs(beforeY + 0.02f) < Mathf.Abs(transform.position.y))
    //        {
    //            beforeY = transform.position.y;
    //        }
    //        else
    //        {
    //            StartReplay();
    //        }
    //        isStopTime = Time.time;
    //    }
    //}

}
