using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    public bool isReplay = false;
    //public float recordTime;
    public float isStopTime;
    public List<PointInTime> pointsInTime;
    private Rigidbody rigid;
    private float beforeY;
    public bool ragdollInit = true;
    public bool Endcheak { get; set; } = false;
    public bool IsDiving { get; set; } = false;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Endcheak)
        {
            CheakReplay(); 
        }

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
        // Mathf.Round(recordTime / Time.fixedDeltaTime) 반올림 함수
        // fixedDeltaTime은 0.02가 나온다
        // 현재 레코드 타임이 10이라서 500개를 저장한다
        //if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        //{
        //    // 마지막에 저장 된 것 부터 삭제 된다
        //    pointsInTime.RemoveAt(pointsInTime.Count - 1);
        //}
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

    private void CheakReplay()
    {
        if (isStopTime + 1f < Time.time)
        {
            if (Mathf.Abs(beforeY + 0.02f) < Mathf.Abs(transform.position.y))
            {
                beforeY = transform.position.y;
            }
            else
            {
                StartReplay();
            }
            isStopTime = Time.time;
        }
    }

}
