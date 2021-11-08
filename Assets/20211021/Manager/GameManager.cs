using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Vars
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
            
            Vars.stage = data.stage;
            Vars.totalstage = data.totalStage;
            Vars.sector = (Vars.Sector)data.sector;
            Vars.mode = (Vars.Mode)data.mode;
            Vars.soundVolume = data.soundVolume;
            Vars.isVibration = data.isVibration;
            Vars.sunColor = new Color(data.r, data.g, data.b);
            Vars.angle = data.angle;
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
        if (Vars.stage < 3 && Vars.sector.Equals(Vars.Sector.TheChurch) && Vars.mode.Equals(Vars.Mode.NormalOne))
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
        Vars.stage++;
        Vars.totalstage++;
        Vars.angle += 15f;
        if (Vars.stage > 5 && (int)Vars.mode % 2 == 0)
        {
            if ((int)Vars.sector > 2)
            {
                Vars.mode++;
                Vars.sector = 0;
            }
        }
        else if (Vars.stage > 10)
        {
            if ((int)Vars.sector > 2)
            {
                Vars.mode++;
            }
        }
        if (Vars.sunColor.g * 255f >= 15f)
            Vars.sunColor.g -= 15f / 255f;
        if (Vars.sunColor.b * 255f >= 30f)
            Vars.sunColor.b -= 30f / 255f;

        SaveData.SaveStage(Vars.stage, Vars.totalstage, (int)Vars.sector, (int)Vars.mode, Vars.isVibration, Vars.soundVolume, Vars.sunColor.r, Vars.sunColor.g, Vars.sunColor.b, Vars.angle);
        SceneManager.LoadScene(0);
    }

    public void TestButton()
    {
        if (Vars.stage < 2)
        {
            if (Vars.sector == 0 && (int)Vars.mode > 0)
                Vars.mode--;
            if ((int)Vars.sector > 0)
                Vars.sector--;
        }
        if (Vars.stage > 1)
        {
            Vars.stage--;
            Vars.totalstage--;
        }
        
        SaveData.SaveStage(Vars.stage, Vars.totalstage, (int)Vars.sector, (int)Vars.mode, Vars.isVibration, Vars.soundVolume, Vars.sunColor.r, Vars.sunColor.g, Vars.sunColor.b, Vars.angle);
        SceneManager.LoadScene(0);
    }

    public void ReSetHouse()
    {
        destoryHouse.GetComponentInChildren<FraggedController>().ReleaseFrags();
    }

    private IEnumerator CoTuto()
    {
        while (timer < 2.9f)
        {
            timer += Time.deltaTime;
            yield return null; 
        }
        uiManager.GetComponent<UIManager>().OffTutorialUI();
        inputManager.GetComponent<InputManager>().OnInputManager();
    }
}
