using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject cameraManager;
    public GameObject player;
    public GameObject inputManager;
    public GameObject uiManager;
    public GameObject destoryHouse;
    public static bool isStart = true;

    private float timer;

    private void Awake()
    {
        Resources.UnloadUnusedAssets(); // 사용중이지 않은 에셋 해제 하기
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // 스마트폰 화면 꺼짐 방지
        gameManager = this;
        player = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 0f;
        if (SaveData.LoadStage() != null)
        {
            StageData data = SaveData.LoadStage();
            
            StaticVariable.stage = data.stage;
            StaticVariable.totalstage = data.totalStage;
            StaticVariable.sector = (StaticVariable.Sector)data.sector;
            StaticVariable.mode = (StaticVariable.Mode)data.mode;
            StaticVariable.soundVolume = data.soundVolume;
            StaticVariable.isVibration = data.isVibration;
            StaticVariable.sunColor = new Color(data.r, data.g, data.b);
            StaticVariable.angle = data.angle;
        }
        if (isStart)
        {
            GoogleMobileAd.Instance.Init();
            isStart = false;
        }
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        var ui = uiManager.GetComponent<UIManager>();
        ui.OffStartUI();
        if (StaticVariable.stage < 3 && StaticVariable.sector.Equals(StaticVariable.Sector.TheChurch) && StaticVariable.mode.Equals(StaticVariable.Mode.NormalOne))
        {
            ui.OnTutorialUI();
            //StartCoroutine(CoTuto());
        }
        inputManager.GetComponent<InputManager>().OnInputManager();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
    public void NextStage()
    {
        StaticVariable.stage++;
        StaticVariable.totalstage++;
        StaticVariable.angle += 15f;
        if (StaticVariable.stage > 5 && (int)StaticVariable.mode % 2 == 0)
        {
            if ((int)StaticVariable.sector > 2)
            {
                StaticVariable.mode++;
                StaticVariable.sector = 0;
            }
        }
        else if (StaticVariable.stage > 10)
        {
            if ((int)StaticVariable.sector > 2)
            {
                StaticVariable.mode++;
            }
        }
        if (StaticVariable.sunColor.g * 255f >= 15f)
            StaticVariable.sunColor.g -= 15f / 255f;
        if (StaticVariable.sunColor.b * 255f >= 30f)
            StaticVariable.sunColor.b -= 30f / 255f;

        SaveData.SaveStage(StaticVariable.stage, StaticVariable.totalstage, (int)StaticVariable.sector, (int)StaticVariable.mode, StaticVariable.isVibration, StaticVariable.soundVolume, StaticVariable.sunColor.r, StaticVariable.sunColor.g, StaticVariable.sunColor.b, StaticVariable.angle);
        SceneManager.LoadScene(0);
    }

    public void ReSetHouse()
    {
        destoryHouse.GetComponentInChildren<FraggedController>().ReleaseFrags();
    }

    //private IEnumerator CoTuto()
    //{
    //    while (timer < 2.9f)
    //    {
    //        timer += Time.deltaTime;
    //        yield return null; 
    //    }
    //    uiManager.GetComponent<UIManager>().OffTutorialUI();
    //    inputManager.GetComponent<InputManager>().OnInputManager();
    //}
}
