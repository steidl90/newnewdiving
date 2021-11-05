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
    public float soundVolume;
    public float r;
    public float g;
    public float b;
    public float angle;
    public bool isVibration;

    public StageData(int stage, int totalStage, int sector, int mode, bool isVibration, float soundVolume, float r, float g, float b, float angle)
    {
        this.stage = stage;
        this.sector = sector;
        this.mode = mode;
        this.totalStage = totalStage;
        this.isVibration = isVibration;
        this.soundVolume = soundVolume;
        this.r = r;
        this.g = g;
        this.b = b;
        this.angle = angle;

    }
}
