using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody ragdoll;

    public void ApplyForce(Vector3 force)
    {
        ragdoll.AddForce(force, ForceMode.Impulse);
    }
}

