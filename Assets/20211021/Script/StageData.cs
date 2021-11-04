using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public int stage = 1;
    public int totalStage = 1;
    public int sector = 0;
    public int mode = 0;

    public StageData(int stage, int totalstage, int sector, int mode)
    {
        this.stage = stage;
        this.sector = sector;
        this.mode = mode;
        this.totalStage = totalstage;
    }
}
