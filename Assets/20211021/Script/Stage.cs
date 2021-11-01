using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] obj;
    
    private void Start()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = new Vector3(obj[i].transform.position.x, 10f * Vars.stage, obj[i].transform.position.z);
        }
        Vars.stage++;
    }
}
