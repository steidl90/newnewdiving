using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;
    public float power;
    public Vector3 force;
    public float y;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        

    }

    public void Fly(Vector3 direction)
    {
        var dir = new Vector3(direction.x, y, direction.y);
        force = dir * power;
        GetComponent<CreatRagdoll>().CreateRagdoll(force);
        var model = GameObject.FindGameObjectWithTag("PlayerModel");
        Destroy(model);

        GameManager.gameManager.Finish();
    }
    
}
