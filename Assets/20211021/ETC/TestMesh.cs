using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMesh : MonoBehaviour
{
    public GameObject mesh;

    private void Update()
    {
        if (!mesh.GetComponent<MeshRenderer>().enabled)
            GetComponent<MeshRenderer>().enabled = false;
    }
}
