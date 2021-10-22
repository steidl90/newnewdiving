using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isReplay = false;
    public float recordTime;
    private List<PointInTime> pointsInTime;
    private Rigidbody rigid;

    private bool test = true;
    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (MultiTouch.DoubleTap())
        {
            StartReplay();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopReplay();
        }

        

    }
    private void FixedUpdate()
    {
        if (isReplay)
        { 
            Replay();
        }
        else
            Record();

    }

    private void Replay()
    {
        if (pointsInTime.Count != 0)
        {
            PointInTime pointInTime = pointsInTime[pointsInTime.Count - 1];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1); 
        }
        if (test)
        {
            var cameraManager = GameManager.gameManager.cameraManager.GetComponent<CameraManager>();
            var replayUI = GameManager.gameManager.UI.transform.GetChild(0);
            replayUI.gameObject.SetActive(true);
            cameraManager.OnReplay();
            test = false;
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    private void StartReplay()
    {
        isReplay = true;
        rigid.isKinematic = true;
        
    }
    private void StopReplay()
    {
        isReplay = false;
        rigid.isKinematic = false;
    }

}
