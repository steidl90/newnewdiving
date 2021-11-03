using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject arrow;
    public GameObject start;
    public GameObject tutorial;
    public GameObject replay;
    public GameObject restart;
    public GameObject settings;
    public GameObject ending;
    public void ButtonOff()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void OnSettingUI()
    {
        settings.SetActive(true);
    }
    public void OffSettingUI()
    {
        settings.SetActive(false);
    }

    public void OnReplayUI()
    {
        replay.SetActive(true);
    }
    public void OffReplayUI()
    {
        replay.SetActive(false);
    }

    public void OffStartUI()
    {
        start.SetActive(false);
    }

    public void OnTutorialUI()
    {
        tutorial.SetActive(true);
        arrow.SetActive(true);
    }

    public void OffTutorialUI()
    {
        tutorial.SetActive(false);
    }
    
    public void OnReStartUI()
    {
        restart.SetActive(true);
    }

    public void OffReStartUI()
    {
        restart.SetActive(false);
    }

    public void OnEndingUI()
    {
        ending.SetActive(true);
    }

    public void OffEndingUI()
    {
        ending.SetActive(false);
    }

    public void TestStage()
    {
        //Vars.mode = Vars.Mode.EasyTwo;
        //Vars.sector++;
        //Vars.stage += 3;

        //if ((int)Vars.sector > 3)
        //    Vars.sector = 0;
        //Vars.stage = 1;

        Vars.mode = Vars.Mode.EasyTwo;
        Vars.sector = Vars.Sector.HouseTriangle;
        Vars.stage = 5;
    }
}
