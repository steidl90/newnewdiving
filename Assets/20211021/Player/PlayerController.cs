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

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    public void Fly(Vector3 direction)
    {
        var dir = new Vector3(direction.x, y, direction.y);
        var force = dir * power;
        GetComponent<CreatRagdoll>().CreateRagdoll(force);
        var model = GameObject.FindGameObjectWithTag("PlayerModel");
        Destroy(model);
        GameManager.gameManager.Finish();
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player") &&
            other.gameObject.layer != LayerMask.NameToLayer("DivingBoard"))
        {
            Debug.Log("asd");
            GetComponent<Replay>().timer = Time.time + 3f;
        }
    }
}
