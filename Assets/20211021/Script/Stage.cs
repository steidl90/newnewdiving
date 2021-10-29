using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int stage;
    public GameObject[] obj;
    

    void Start()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = new Vector3(obj[i].transform.position.x, 10f * stage, obj[i].transform.position.z);
        }
    }
}
