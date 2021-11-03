using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> onTouchToDrag;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool startSetting = false;

    private void Update()
    {
        var maincamera = GameManager.gameManager.cameraManager.GetComponent<CameraManager>().main.GetComponent<Camera>().enabled;
        var ui = GameManager.gameManager.uiManager.GetComponent<UIManager>().settings.activeSelf;
        if (Input.touchCount == 1 && maincamera && !ui)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = Camera.main.ScreenToViewportPoint(touch.position);
                    startSetting = true;
                    break;
                case TouchPhase.Ended:
                    if (startSetting)
                    {
                        endPos = Camera.main.ScreenToViewportPoint(touch.position);
                        var direction = endPos - startPos;
                        if (endPos.y - startPos.y > 0)
                        {
                            onTouchToDrag.Invoke(direction);
                            startPos = endPos = Vector3.zero;
                            OffInputManager();
                            GameManager.gameManager.uiManager.GetComponent<UIManager>().arrow.SetActive(false);
                            GameManager.gameManager.uiManager.GetComponent<UIManager>().tutorial.SetActive(false);
                        }
                        startSetting = false;
                    }
                    break;
            }
        }
    }
    public void OnInputManager()
    {
        gameObject.SetActive(true);
    }

    public void OffInputManager()
    {
        gameObject.SetActive(false);
    }
}
