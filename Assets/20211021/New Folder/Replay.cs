using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    public bool isReplay = false;
    public float recordTime;
    private List<PointInTime> pointsInTime;
    private Rigidbody rigid;
    private float beforeY;
    private float isStopTime;
    private bool ragdollInit = true;
    public bool Endcheak { get; set; } = false;
    public bool IsDiving { get; set; } = false;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rigid = GetComponent<Rigidbody>();
        isStopTime = -0.5f;
    }

    private void Update()
    {

        if (Endcheak)
        {
            CheakReplay(); 
        }

        //if (rigid.velocity.magnitude < 0.5f && timer < Time.time)
        //{
        //    StartReplay();
        //}
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
        else if (IsDiving)
        { 
            Record();
        }
    }

    private void OnReplay()
    {
        Time.timeScale = 1.5f;
        if (pointsInTime.Count != 0)
        {
            PointInTime pointInTime = pointsInTime[pointsInTime.Count - 1];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1); 
        }
        if (ragdollInit)
        {
            var ragodoll = GameManager.gameManager.player.GetComponent<CreatRagdoll>();
            ragodoll.DestroyRagdoll();
            ragodoll.CreateReplayRagdoll();
            var cameraManager = GameManager.gameManager.cameraManager.GetComponent<CameraManager>();
            var replayUI = GameManager.gameManager.UI.transform.GetChild(0);
            replayUI.gameObject.SetActive(true);
            cameraManager.OnReplay();
            ragdollInit = false;
        }
        if (pointsInTime.Count == 0)
        {
            GameManager.gameManager.OnReStartUI();
        }
    }

    private void Record()
    {
        // Mathf.Round(recordTime / Time.fixedDeltaTime) 반올림 함수
        // fixedDeltaTime은 0.02가 나온다
        // 현재 레코드 타임이 7이라서 350개를 저장한다
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            // 마지막에 저장 된 것 부터 삭제 된다
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
        Debug.Log($"{Mathf.Abs(beforeY + 0.05f)}, {Mathf.Abs(transform.position.y)}");
    }

}
