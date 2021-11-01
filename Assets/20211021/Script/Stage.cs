using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] obj;
    public float height;
    public GameObject[] terrains;
    private float posX;
    private float posZ;
    
    private void Start()
    {
        Vars.stage++;
        if ((int)Vars.sector > 3)
            Vars.sector = 0;
        switch (Vars.mode)
        {
            case Vars.Mode.EasyOne:
                if (Vars.stage > 5)
                {
                    Vars.stage = 1;
                    Vars.sector++;
                }
                break;
            case Vars.Mode.EasyTwo:
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

        Debug.Log($"{Vars.sector}, stage: {Vars.stage}, {Vars.mode}");

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
                break;
            case Vars.Sector.WayToBeach:
                posX = 15.2f;
                posZ = 23.32f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[1].SetActive(true);
                break;
            case Vars.Sector.TheAlley:
                posX = -10.8f;
                posZ = 18.56f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[2].SetActive(true);
                break;
            case Vars.Sector.HouseTriangle:
                posX = -18f;
                posZ = -4.8f;
                for (int i = 0; i < terrains.Length; i++)
                {
                    terrains[i].SetActive(false);
                }
                terrains[3].SetActive(true);
                if(Vars.stage == 5) // 재 시작시 모드를 낮춰야 함
                { 
                    Vars.mode++;
                    Vars.sector++;
                }
                break;
        }

        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].transform.position = new Vector3(obj[i].transform.position.x + posX, height * Vars.stage, obj[i].transform.position.z + posZ);
        }
    }
}
