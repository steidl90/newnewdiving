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
    public GameObject UIManager;
    public GameObject destoryHouse;

    private void Awake()
    {
        gameManager = this;
        player = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 0f;
    }

    public void Diving()
    {
        //inputManager.GetComponent<InputManager>().OffInputManager();
    }
    public void GameStart()
    {
        Time.timeScale = 1f;
        UIManager.GetComponent<UIManager>().OffStartUI();
        UIManager.GetComponent<UIManager>().OnTutorialUI();
        inputManager.GetComponent<InputManager>().OnInputManager();

    }
    public void OnReStartUI()
    {
        UIManager.GetComponent<UIManager>().restart.SetActive(true);
    }
    public void OnEndingUI()
    {

    }
    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
    public void ReSetHouse()
    {
        destoryHouse.GetComponentInChildren<FraggedController>().ReleaseFrags();
    }
}
