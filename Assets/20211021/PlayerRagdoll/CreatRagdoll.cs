using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRagdoll : MonoBehaviour
{
    public GameObject originalRagdoll;
    public GameObject replayRagdoll;
    public void CreateRagdoll(Vector3 force)
    {
        originalRagdoll.SetActive(true);
        var chest = GameObject.FindGameObjectWithTag("Spine");
        chest.AddComponent<FixedJoint>();
        chest.GetComponent<FixedJoint>().connectedBody = GameManager.gameManager.player.GetComponent<Rigidbody>();
        var ragdoll = originalRagdoll.GetComponent<Ragdoll>();
        ragdoll.ApplyForce(force);
        
        //var ragdoll = Instantiate(prefab, transform.position, transform.rotation);
        //ragdoll.transform.parent = transform;
        //var newOne = ragdoll.GetComponent<Ragdoll>();
        //newOne.ApplyForce(force);
    }
    public void CreateReplayRagdoll()
    {
        replayRagdoll.SetActive(true);
        
        var chest = GameObject.FindGameObjectWithTag("Spine2");
        chest.AddComponent<FixedJoint>();
        chest.GetComponent<FixedJoint>().connectedBody = GameManager.gameManager.player.GetComponent<Rigidbody>();
        
    }

    public void DestroyRagdoll()
    {
        Destroy(originalRagdoll);
    }
}
