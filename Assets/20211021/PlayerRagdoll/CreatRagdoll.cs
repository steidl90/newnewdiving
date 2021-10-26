using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRagdoll : MonoBehaviour
{
    public GameObject originalRagdoll;
    public GameObject replayRagdoll;
    public void CreateRagdoll(Vector3 force, Vector3 pos)
    {
        var onReplay = GetComponent<PlayerController>().IsReplay;
        if (onReplay)
        { 
            OnReplayRagdoll();
            replayRagdoll.transform.position = pos;
            //var ragdoll = replayRagdoll.GetComponent<Ragdoll>();
            //ragdoll.ApplyForce(force);
        }
        else
        {
            OnRagdoll();
            originalRagdoll.transform.position = pos;
            var ragdoll = originalRagdoll.GetComponent<Ragdoll>();
            ragdoll.ApplyForce(force);
        }
        
    }

    public void DestroyRagdoll()
    {
        Destroy(originalRagdoll);
    }
    public void OnRagdoll()
    {
        Debug.Log("기존렉돌");
        originalRagdoll.SetActive(true);
    }
    public void OffRagdoll()
    {
        originalRagdoll.SetActive(false);
    }
    public void OnReplayRagdoll()
    {
        replayRagdoll.SetActive(true);
    }
    public void OffReplayRagdoll()
    {
        replayRagdoll.SetActive(false);
    }
}
