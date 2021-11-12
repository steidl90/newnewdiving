using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariable : MonoBehaviour
{
    public static int stage = 1;
    public static int totalstage = 1;
    public static float soundVolume = 1f;
    public static float angle = 0f;
    public static bool isVibration = true;
    public static Color sunColor = Color.white;
    public enum Sector
    {
        TheChurch,
        WayToBeach,
        TheAlley,
        HouseTriangle
    }
    public static Sector sector = 0;

    public enum Mode
    {
        NormalOne,
        NormalTwo,
        HardOne,
        HardTwo,
    }
    public static Mode mode = 0;
}
