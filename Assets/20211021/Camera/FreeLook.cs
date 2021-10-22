using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLook : MonoBehaviour
{
    private CinemachineFreeLook freeLook;
    void Start()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {
        var ragdoll = GameManager.gameManager.player.GetComponent<CreatRagdoll>();
        //if(ragdoll.ragdoll != null)
        //{ 
        //    freeLook.Follow = ragdoll.ragdoll.transform;
        //    freeLook.LookAt = ragdoll.ragdoll.transform;
        //}
    }
}
