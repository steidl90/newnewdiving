using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    public bool isReplay = false;
    public List<PointInTime> pointsInTime;
    private Rigidbody rigid;
    public GameObject plane;
    public GameObject planeReplay;
    public bool IsDiving { get; set; } = false;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        IsDiving = GameManager.gameManager.player.GetComponent<PlayerController>().IsDiving;
        isReplay = GameManager.gameManager.player.GetComponent<PlayerController>().IsReplay;
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
        planeReplay = GetComponentInChildren<PlaneMove>().gameObject;
        if (pointsInTime.Count != 0)
        {
            PointInTime pointInTime = pointsInTime[pointsInTime.Count - 1];
            planeReplay.transform.position = pointInTime.position;
            planeReplay.transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
    }

    private void Record()
    {
        pointsInTime.Insert(0, new PointInTime(plane.transform.position, plane.transform.rotation));
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
}
