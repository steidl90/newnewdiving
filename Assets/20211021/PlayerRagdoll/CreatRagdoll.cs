using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatRagdoll : MonoBehaviour
{
    public GameObject prefab;

    public void CreateRagdoll(Vector3 force)
    {
        var newOne = Instantiate(prefab, transform.position, transform.rotation);
        newOne.transform.parent = transform;
        var ragdoll = newOne.GetComponent<Ragdoll>();
        ragdoll.ApplyForce(force);
    }
}
