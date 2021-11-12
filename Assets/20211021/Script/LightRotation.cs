using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    private void Awake()
    {
        if (StaticVariable.angle > 345f)
        {
            StaticVariable.angle = 0f;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + StaticVariable.angle, transform.eulerAngles.y, transform.eulerAngles.z);

        var light = transform.parent.GetComponent<LightManager>();
        var mainLight = light.mainLight.GetComponent<Light>();

        light.lampOn = (StaticVariable.angle >= 135f && StaticVariable.angle <= 315f) ? true : false;
        if (!light.lampOn)
        {
            mainLight.color = StaticVariable.sunColor;
        }
        else
        {
            mainLight.color = StaticVariable.sunColor = Color.white;
        }
    }
}
