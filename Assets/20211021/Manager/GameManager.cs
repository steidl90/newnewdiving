using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Vars
{
    public static int stage = 1;
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
        EasyOne,
        EasyTwo,
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

    private float timer;

    private void Awake()
    {
        gameManager = this;
        player = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 0f;
    }

    public void GameStart()
    {
        Time.timeScale = 1f;
        uiManager.GetComponent<UIManager>().OffStartUI();
        if (Vars.stage < 3 && Vars.sector.Equals(Vars.Sector.TheChurch) && Vars.mode.Equals(Vars.Mode.EasyOne))
        {
            uiManager.GetComponent<UIManager>().OnTutorialUI();
            StartCoroutine(CoTuto()); 
        }
        else
        {
            inputManager.GetComponent<InputManager>().OnInputManager();
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
    public void NextStage()
    {
        Vars.stage++;
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
