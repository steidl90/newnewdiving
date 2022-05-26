using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public Ragdoll originalRagdoll;
    public Ragdoll replayRagdoll;
    public void ActiveRagdoll(Vector3 force, Vector3 pos)
    {
        var onReplay = GetComponent<PlayerController>().IsReplay;
        if (onReplay)
        { 
            OnReplayRagdoll();
            replayRagdoll.transform.position = pos;
        }
        else
        {
            OnRagdoll();
            originalRagdoll.transform.position = pos;
            originalRagdoll.ApplyForce(force);
        }
    }

    public void DestroyRagdoll()
    {
        Destroy(originalRagdoll);
    }
    public void OnRagdoll()
    {
        originalRagdoll.gameObject.SetActive(true);
    }
    public void OffRagdoll()
    {
        originalRagdoll.gameObject.SetActive(false);
    }
    public void OnReplayRagdoll()
    {
        replayRagdoll.gameObject.SetActive(true);
    }
    public void OffReplayRagdoll()
    {
        replayRagdoll.gameObject.SetActive(false);
    }
}
