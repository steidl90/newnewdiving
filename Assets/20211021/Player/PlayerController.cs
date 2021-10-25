using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;
    public float power;
    public float y;
    private bool isReplay;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        isReplay = GetComponent<Replay>().isReplay;

        if (isReplay)
        {
            OnReplay(); 
        }
    }
    public void Fly(Vector3 direction)
    {
        var dir = new Vector3(direction.x, y, direction.y);
        var force = dir * power;
        //animator.SetTrigger("Diving");
        //rigid.AddForce(force, ForceMode.Impulse);
        GetComponent<CreatRagdoll>().CreateRagdoll(force);
        var model = GameObject.FindGameObjectWithTag("PlayerModel");
        Destroy(model);
        GetComponent<Replay>().IsDiving = true;
        GameManager.gameManager.Diving();
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player") &&
            other.gameObject.layer != LayerMask.NameToLayer("DivingBoard"))
        {
            GetComponent<Replay>().Endcheak = true;
            GetComponent<Replay>().isStopTime = Time.time;
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{isReplay}, {other.gameObject.layer}");

        if (isReplay && other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log("¼º°ø");
        }
    }

    public void OnReplay()
    {
        var rag = GetComponent<Replay>().ragdollInit;
        var count = GetComponent<Replay>().pointsInTime.Count;
        if (rag)
        {
            Time.timeScale = 1.5f;
            var ragodoll = GetComponent<CreatRagdoll>();
            ragodoll.DestroyRagdoll();
            ragodoll.CreateReplayRagdoll();
            GameManager.gameManager.UI.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.gameManager.cameraManager.GetComponent<CameraManager>().OnReplay();
            GetComponent<Replay>().ragdollInit = false;
        }

        if (count == 0)
        {
            GameManager.gameManager.OnReStartUI();
            GetComponent<Replay>().StopReplay();
            GetComponent<Replay>().Endcheak = false;
            Time.timeScale = 1f;
            Destroy(rigid);
        }
    }
}
