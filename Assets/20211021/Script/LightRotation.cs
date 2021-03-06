using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    private float timer;

    private void Awake()
    {
        if (Vars.angle > 345f)
        {
            Vars.angle = 0f;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + Vars.angle, transform.eulerAngles.y, transform.eulerAngles.z);

        var light = transform.parent.GetComponent<LightManager>();
        var mainLight = light.mainLight.GetComponent<Light>();

        light.lampOn = (Vars.angle >= 135f && Vars.angle <= 315f) ? true : false;
        if (!light.lampOn)
        {
            mainLight.color = Vars.sunColor;
        }
        else
        {
            mainLight.color = Vars.sunColor = Color.white;
        }
    }
}
