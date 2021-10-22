using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRagdoll : MonoBehaviour
{
    public GameObject prefab;
    public void CreateRagdoll(Vector3 force)
    {
        prefab.SetActive(true);
        var chest = GameObject.FindGameObjectWithTag("Spine");
        chest.AddComponent<FixedJoint>();
        chest.GetComponent<FixedJoint>().connectedBody = GameManager.gameManager.player.GetComponent<Rigidbody>();
        var ragdoll = prefab.GetComponent<Ragdoll>();
        ragdoll.ApplyForce(force);
        


        //var ragdoll = Instantiate(prefab, transform.position, transform.rotation);
        //ragdoll.transform.parent = transform;
        //var newOne = ragdoll.GetComponent<Ragdoll>();
        //newOne.ApplyForce(force);


    }
}
