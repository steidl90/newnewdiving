using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject start;
    public GameObject tutorial;
    public GameObject replay;
    public GameObject restart;
    public GameObject settings;
    public void ButtonOff()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void SettingUIOn()
    {
        settings.SetActive(true);
    }
    public void SettingUIOff()
    {
        settings.SetActive(false);
    }

    public void ReplayUIOn()
    {
        replay.SetActive(true);
    }

    public void StartUIOff()
    {
        start.SetActive(false);
    }

    public void TutorialUIOn()
    {
        tutorial.SetActive(true);
    }

    public void TutorialUIOff()
    {
        tutorial.SetActive(false);
    }
}
