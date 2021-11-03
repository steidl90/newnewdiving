using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public int stage;
    public int sector;
    public int mode;

    public StageData(int stage, int sector, int mode)
    {
        this.stage = stage;
        this.sector = sector;
        this.mode = mode;
    }
}
