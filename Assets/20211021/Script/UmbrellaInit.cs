using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaInit : MonoBehaviour
{
    public GameObject[] umbrella;
    public GameObject[] newUmbrella;
    public Vector3[] pos;
    public Quaternion[] rot;
    public int index;
    private void Start()
    {
        pos = new Vector3[umbrella.Length];
        rot = new Quaternion[umbrella.Length];
        for (int i = 0; i < umbrella.Length; i++)
        {
            pos[i] = umbrella[i].transform.position;
            rot[i] = umbrella[i].transform.rotation;
        }
    }

    public void DestroyUmbrella()
    {
        Destroy(umbrella[index]);
    }

    public void InitUmbrella()
    {
        var newOne = Instantiate(newUmbrella[index], pos[index], rot[index]);
        newOne.gameObject.AddComponent<Rigidbody>();
        Debug.Log("»ý¼º");
    }
}
