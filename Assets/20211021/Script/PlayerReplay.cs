using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplay : MonoBehaviour
{
    public List<ReplayData> data;
    private GameObject originalRagdoll;
    private GameObject replayRagdoll;
    private GameObject playerTag;
    private FollowerTarget subCamera;

    private void Start()
    {
        data = new List<ReplayData>();
        originalRagdoll = GetComponent<RagdollManager>().originalRagdoll.ragdoll.gameObject;
        replayRagdoll = GetComponent<RagdollManager>().replayRagdoll.ragdoll.gameObject;
        playerTag = GameObject.FindGameObjectWithTag("PlayerModel");
        subCamera = GameManager.gameManager.cameraManager.GetComponent<CameraManager>().sub.GetComponent<FollowerTarget>();
    }

    public void OnReplay()
    {
        if (data.Count != 0)
        {
            ReplayData player = data[data.Count - 1];
            switch (player.types)
            {
                case ReplayData.Types.active:
                    player.target.SetActive(true);
                    break;
                case ReplayData.Types.posAndRot:
                    if(player.target == originalRagdoll)
                    { 
                        player.target = replayRagdoll;
                        player.target.transform.position = player.position;
                        player.target.transform.rotation = player.rotation;
                    }
                    else if(player.target == playerTag)
                    {
                        var rigid = player.target.GetComponent<Rigidbody>();
                        rigid.MovePosition(player.position);
                        rigid.MoveRotation(player.rotation);
                    }
                    break;
                case ReplayData.Types.deActive:
                    var pos = player.target.transform.position;
                    player.target.transform.position = new Vector3(pos.x, pos.y - 1.5f, pos.z);
                    subCamera.pos = player.target.transform.position;
                    player.target.GetComponent<Rigidbody>().isKinematic = true;
                    player.target.SetActive(false);
                    break;
            }
            data.RemoveAt(data.Count - 1);
        }
    }

    public void Record(ReplayData replayData) => data.Insert(0, replayData);
}
