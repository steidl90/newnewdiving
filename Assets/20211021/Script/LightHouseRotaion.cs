using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseRotaion : MonoBehaviour
{
    private void Update()
    {

        transform.eulerAngles += new Vector3(0f, 60f * Time.deltaTime, 0f);
    }
}
