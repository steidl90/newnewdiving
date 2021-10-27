using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyHouse : MonoBehaviour
{
    public SerializeDictionary<int, GameObject> houses;
    public GameObject[] prefabHouse;
    private Transform[] trans;
    private void Start()
    {
        houses = new SerializeDictionary<int, GameObject>();
        trans = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            houses.Add(i, transform.GetChild(i).gameObject);
            trans[i] = transform.GetChild(i).gameObject.transform;
        }
    }

    public void HouseDestroy(int key)
    {
        Destroy(houses[key].gameObject);
    }
    public void HouseInit(int key)
    {
        houses[key] = Instantiate(prefabHouse[key]);
        houses[key].transform.SetParent(transform);
        houses[key].transform.SetPositionAndRotation(trans[key].position, trans[key].rotation);
    }
}
