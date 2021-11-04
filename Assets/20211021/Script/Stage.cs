using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] obj;
    public float height;
    public GameObject[] terrains;
    public GameObject endingCamera;
    private float posX;
    private float posZ;
    
    public void Start()
    {
        switch (Vars.mode)
        {
            case Vars.Mode.NormalOne:
                if (Vars.stage > 5)
                {
                    Vars.stage = 1;
                    Vars.sector++;
                }
                break;
            case Vars.Mode.NormalTwo:
                if (Vars.stage > 10)
                {
                    Vars.stage = 6;
                    Vars.sector++;
                }
                break;
            case Vars.Mode.HardOne:
                if (Vars.stage > 5)
                {
                    Vars.stage = 1;
                    Vars.sector++;
                }
                break;
            case Vars.Mode.HardTwo:
                if (Vars.stage > 10)
                {
                    Vars.stage = 6;
                    Vars.sector++;
                }
                break;
        }

        if ((int)Vars.sector > 3)
            Vars.sector = 0;

        switch (Vars.sector)
        {
            case Vars.Sector.TheChurch:
                posX = 0f;
                posZ = 0f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[0].SetActive(true);
                if ((int)Vars.mode > 1)
                {
                    terrains[0].transform.GetChild(2).gameObject.transform.localPosition = new Vector3(129.84f, 40.05f, 121.21f);
                }
                endingCamera.transform.position = new Vector3(3.97f, 3.74f, 4.17f);

                break;
            case Vars.Sector.WayToBeach:
                posX = 15.2f;
                posZ = 23.32f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[1].SetActive(true);
                if ((int)Vars.mode > 1)
                {
                    terrains[1].transform.GetChild(2).gameObject.transform.localPosition = new Vector3(142.629f, 40.05f, 143.951f);
                }
                endingCamera.transform.position = new Vector3(16.54f, 3.2f, 25.2f);
                break;
            case Vars.Sector.TheAlley:
                posX = -10.8f;
                posZ = 18.56f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[2].SetActive(true);
                if ((int)Vars.mode > 1)
                {
                    terrains[2].transform.GetChild(2).gameObject.transform.localPosition = new Vector3(123.1106f, 40.05f, 136.117f);
                }
                endingCamera.transform.position = new Vector3(-1.8f, 3.24f, 17.8f);
                break;
            case Vars.Sector.HouseTriangle:
                posX = -18f;
                posZ = -4.8f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[3].SetActive(true);
                if ((int)Vars.mode > 1)
                {
                    terrains[3].transform.GetChild(2).gameObject.transform.localPosition = new Vector3(114.48f, 40.05f, 112.68f);
                }
                endingCamera.transform.position = new Vector3(-10.05f, 3.21f, -5.99f);
                break;
        }

        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = new Vector3(obj[i].transform.position.x + posX, height * Vars.stage, obj[i].transform.position.z + posZ);
        }
    }
}
