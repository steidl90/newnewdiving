using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject main;
    public GameObject sub;

    public void OnReplay()
    {
        main.GetComponent<Camera>().enabled = false;
        main.GetComponent<AudioListener>().enabled = false;
        sub.SetActive(true);
    }
}
