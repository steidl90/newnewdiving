using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject mainCamera;
    public GameObject player;
    public GameObject inputManager;
    public GameObject UI;

    private void Awake()
    {
        gameManager = this;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        //inputManager = GameObject.FindGameObjectWithTag("InputMangaer");
    }

    public void Finish()
    {
        //UI.GetComponent<UIManager>().ButtonOff();
        inputManager.SetActive(false);
        //player.GetComponent<PlayerController>().Diving();
        //mainCamera.GetComponent<FollowTarget>().DivingView();
    }
}
