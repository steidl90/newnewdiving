using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Touch> onTouch;
    public UnityEvent<Vector3> onTouchToDrag;
    private Vector3 startPos;
    private Vector3 endPos;

    private void Update()
    {
        var direction = Vector3.zero;

        if (Input.touchCount == 1)
        {
            var touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = Camera.main.ScreenToViewportPoint(touch.position);
                    break;
                case TouchPhase.Ended:
                    endPos = Camera.main.ScreenToViewportPoint(touch.position);
                    direction = endPos - startPos;
                    if (endPos.y - startPos.y > 0)
                    {
                        onTouchToDrag.Invoke(direction);
                    }
                    startPos = endPos = Vector3.zero;
                    break;
            }
        }
    }
}
