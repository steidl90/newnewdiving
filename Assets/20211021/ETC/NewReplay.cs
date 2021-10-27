using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewReplay : MonoBehaviour
{
    public List<ReplayData> data;
    
    private void Start()
    {
        data = new List<ReplayData>();
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
                    if(player.target ==
                        GetComponent<CreatRagdoll>().originalRagdoll.GetComponent<Ragdoll>().ragdoll.gameObject)
                    player.target = GetComponent<CreatRagdoll>().replayRagdoll.GetComponent<Ragdoll>().ragdoll.gameObject;
                    player.target.transform.position = player.position;
                    player.target.transform.rotation = player.rotation;
                    break;
                case ReplayData.Types.deActive:
                    player.target.SetActive(false);
                    break;
            }
            data.RemoveAt(data.Count - 1);
        }
    }

    public void Record(ReplayData replayData)
    {
        data.Insert(0, replayData);
    }
}
