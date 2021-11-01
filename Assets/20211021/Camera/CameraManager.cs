using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject main;
    public GameObject sub;
    public GameObject result;

    public void OnReplay()
    {
        main.GetComponent<Camera>().enabled = false;
        main.GetComponent<AudioListener>().enabled = false;
        sub.GetComponent<AudioListener>().enabled = true;
        sub.GetComponent<FollowTarget>().enabled = true;
        //sub.GetComponent<FollowTarget>().init();
        sub.GetComponent<Camera>().depth = 0;

        //gameObject.AddComponent<FollowTarget>().target = player;
    }
}
