using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float power;
    public float y;

    private Animator animator;
    private Rigidbody rigid;
    private Vector3 force;
    private bool isReplay;
    private bool isCollision = true;
    private GameObject model;
    public float beforeY;
    public float isStopTime;

    private bool endcheak = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        model = GameObject.FindGameObjectWithTag("PlayerModel");
    }
    private void Update()
    {
        isReplay = GetComponent<Replay>().isReplay;
        if (endcheak)
        {
            CheakReplay();

            if (isReplay)
            {
                OnReplay();
            }
        }
    }
    public void Fly(Vector3 direction)
    {
        var dir = new Vector3(direction.x, y, direction.y);
        force = dir * power;
        animator.SetTrigger("Diving");
        rigid.AddForce(force, ForceMode.Impulse);

        GetComponent<Replay>().IsDiving = true;
        GameManager.gameManager.Diving();
    }
    
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log($"{isReplay}, {other.gameObject.layer}");

        if (other.gameObject.layer != LayerMask.NameToLayer("Player") &&
            other.gameObject.layer != LayerMask.NameToLayer("DivingBoard"))
        {
            endcheak = true;
            isStopTime = Time.time;

            if (other.gameObject.layer == LayerMask.NameToLayer("Floor") &&
            isCollision)
            {
                GetComponent<CreatRagdoll>().CreateRagdoll(force * 0f);
                model.SetActive(false);
                isCollision = false;
            }
            else if (other.gameObject.layer != LayerMask.NameToLayer("Water") &&
                     isCollision)
            {
                GetComponent<CreatRagdoll>().CreateRagdoll(force * 50f);
                model.SetActive(false);
                isCollision = false;
            }
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
            ragodoll.originalRagdoll.SetActive(false);
            model.SetActive(true);
            animator.SetTrigger("IsReplay");
            isCollision = true;
            GameManager.gameManager.UI.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.gameManager.cameraManager.GetComponent<CameraManager>().OnReplay();
            GetComponent<Replay>().ragdollInit = false;
        }

        if (count == 0)
        {
            GameManager.gameManager.OnReStartUI();
            GetComponent<Replay>().StopReplay();
            endcheak = false;
            Time.timeScale = 1f;
            //Destroy(rigid);
        }
    }

    private void CheakReplay()
    {
        if (beforeY + 0.02f < transform.position.y)
        {
            isStopTime = Time.time;
        }
        else if(isStopTime + 1f < Time.time)
        {
            GetComponent<Replay>().StartReplay();
        }
        beforeY = transform.position.y;
    }
}
