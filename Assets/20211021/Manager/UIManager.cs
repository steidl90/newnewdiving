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
    public GameObject stageText;
    private static bool firstStart = true;
    

    private void Start()
    {
        if (firstStart)
        {
            start.transform.GetChild(1).gameObject.SetActive(true);
            firstStart = false;
        }
    }


    public void OnSettingUI()
    {
        settings.SetActive(true);
        GameManager.gameManager.inputManager.SetActive(false);

    }
    public void OffSettingUI()
    {
        settings.SetActive(false);
        if (!start.activeSelf && !tutorial.activeSelf)
            GameManager.gameManager.inputManager.SetActive(true);
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
        arrow.SetActive(false);
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

    public void OnStageTextUI()
    {
        stageText.SetActive(true);
    }


    public void ReStart()
    {
        GoogleMobileAd.Instance.ReStartAd();
    }

    public void NextStage()
    {
        GoogleMobileAd.Instance.NextStageAd();
    }
}
