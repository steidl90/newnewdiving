using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayData
{
    public enum Types
    {
        active,
        posAndRot,
        deActive
    }
    public Types types;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject target;
}

