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
    public GameObject UI;


    private void Awake()
    {
        gameManager = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Finish()
    {
        //UI.GetComponent<UIManager>().ButtonOff();
        inputManager.GetComponent<InputManager>().OffInputManager();
        //player.GetComponent<PlayerController>().Diving();
        //mainCamera.GetComponent<FollowTarget>().DivingView();
    }
    public void GameStart()
    {
        var startButton = UI.transform.GetChild(1);
        startButton.GetComponent<GameStart>().OffButton();
        inputManager.GetComponent<InputManager>().OnInputManager();
    }
    public void OnReStartUI()
    {
        UI.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
