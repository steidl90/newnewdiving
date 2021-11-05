using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject mainLight;
    public GameObject lampLights;
    public bool lampOn;

    private void Start()
    {
        if (lampOn)
        {
            lampLights.SetActive(true);
        }
    }
    //private void Update()
    //{
    //    if (lampOn)
    //    {
    //        lampLights.SetActive(true);
    //    }
    //    else
    //        lampLights.SetActive(false);
    //}
}
