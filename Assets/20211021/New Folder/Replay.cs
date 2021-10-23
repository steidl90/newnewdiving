using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
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
            OnReplay();
        }
        else
            Record();

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
        if (test)
        {
            var ragodoll = GameManager.gameManager.player.GetComponent<CreatRagdoll>();
            ragodoll.DestroyRagdoll();
            ragodoll.CreateReplayRagdoll();
            var cameraManager = GameManager.gameManager.cameraManager.GetComponent<CameraManager>();
            var replayUI = GameManager.gameManager.UI.transform.GetChild(0);
            replayUI.gameObject.SetActive(true);
            cameraManager.OnReplay();
            test = false;
        }
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime)) // 추후 던져질때 시간 저장 바닥에 충돌했을 때 시간 저장 해서 그 만큼만 하면 되지 않을까..?
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
