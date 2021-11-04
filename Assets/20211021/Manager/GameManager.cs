using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Vars
{
    public static int stage = 1;
    public static int totalstage = 1;
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
        uiManager.GetComponent<UIManager>().OffStartUI();
        if (Vars.stage < 3 && Vars.sector.Equals(Vars.Sector.TheChurch) && Vars.mode.Equals(Vars.Mode.NormalOne))
        {
            uiManager.GetComponent<UIManager>().OnTutorialUI();
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
        Debug.Log($"{Vars.stage},{Vars.totalstage}, {(int)Vars.sector}, {(int)Vars.mode}");
        SaveData.SaveStage(Vars.stage, Vars.totalstage, (int)Vars.sector, (int)Vars.mode);
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
        
        Debug.Log($"{Vars.stage},{Vars.totalstage}, {(int)Vars.sector}, {(int)Vars.mode}");
        SaveData.SaveStage(Vars.stage, Vars.totalstage, (int)Vars.sector, (int)Vars.mode);
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
