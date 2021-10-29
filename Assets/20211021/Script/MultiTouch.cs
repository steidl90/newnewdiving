using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouch : MonoBehaviour
{
    // ���� �� �뵵
    private static float lastTouchTime;
    private static float doubleTouchDelay = 0.2f;
    // �� ������ �뵵
    private static float firstTapTime;
    private static float longTapTime = 1f;
    // �������� �뵵
    private static Vector2 touchStartPos;
    private static int fingerId = int.MinValue;
    public static float minSwipeDistancePixels;
    private static Vector2 result;
    // Ȯ�� ��� �뵵
    private static float minZoomInce = 0.25f;
    private static float maxZoomInce = 0.5f;
    // ����
    private static float currentDegree;


    private void Start()
    {
        minSwipeDistancePixels = Screen.dpi * 0.25f;
    }

    
    public static bool Tap()
    {
        if(Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Tap�� �߽��ϴ�");
                return true;
            }
        }
        return false;
    }

    public static bool DoubleTap()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && Time.time - lastTouchTime < doubleTouchDelay)
            {
                Debug.Log("DoubleTap�� �߽��ϴ�");
                return true;
            }
            if(touch.phase == TouchPhase.Ended)
                lastTouchTime = Time.time;
        }
        return false;
    }

    public static bool LongPress()
    {
        if(Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstTapTime = Time.time;
            }
            if (firstTapTime + longTapTime < Time.time) // ��ġ �� 1�ʰ� ������ �� LongPress
            {
                //Debug.Log("LongPress�� �߽��ϴ�");
                return true;
            }
        }
        return false;
    }

    public static Vector2 Swipe()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began && fingerId == int.MinValue)
            {
                touchStartPos = touch.position;
                fingerId = touch.fingerId;
                firstTapTime = Time.time;
            }
            if (touch.phase == TouchPhase.Ended && fingerId == touch.fingerId)
            {
                var endPos = touch.position;
                var delta = endPos - touchStartPos;
                if (Mathf.Abs(delta.x) > minSwipeDistancePixels ||
                    Mathf.Abs(delta.y) > minSwipeDistancePixels)
                {
                    // ����� ��
                    if (delta.y > 0 && Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
                        Debug.Log($"up = {Vector2.up}");
                    else if (delta.y < 0 && Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
                        Debug.Log($"down = {Vector2.down}");
                    else if (delta.x > 0 && Mathf.Abs(delta.y) < Mathf.Abs(delta.x))
                        Debug.Log($"right = {Vector2.right}");
                    else if (delta.x < 0 && Mathf.Abs(delta.y) < Mathf.Abs(delta.x))
                        Debug.Log($"left = {Vector2.left}");

                    // ���� �� ��ȯ
                    result = (Mathf.Abs(delta.y) > Mathf.Abs(delta.x)) ? 
                        (delta.y > 0 ? Vector2.up : Vector2.down) : (delta.x > 0 ? Vector2.right : Vector2.left);
                }
                fingerId = int.MinValue;
            }
        }
        return result;
    }

    public static float PinchToZoom()
    {
        var result = 0f;
        if(Input.touchCount == 2)
        {
            var touch0 = Input.touches[0];
            var touch1 = Input.touches[1];
            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;
            var diffPrev = Vector2.Distance(touch0PrevPos, touch1PrevPos);
            var diffCurr = Vector2.Distance(touch0.position, touch1.position);
            var diffPixels = diffCurr - diffPrev; // �� ���� +�� Ȯ�� -�� ���
            result = diffPixels / Screen.dpi;
            result = Mathf.Clamp(result, -minZoomInce, maxZoomInce);
            
            // ����׿�
            if (result > 0)
                Debug.Log($"Zoom�� �� {result}");
            else
                Debug.Log($"Pinch�� �� {result}");
        }
        return result;
    }
    
    public static float Rotate()
    {
        if (Input.touchCount == 2)
        {
            var touch0 = Input.touches[0];
            var touch1 = Input.touches[1];
            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;

            var rot0 = touch1PrevPos - touch0PrevPos;
            var rot1 = touch1.position - touch0.position;

            var dot0 = Vector2.Dot(Vector2.up, rot0.normalized);
            var prevDegree0 = Mathf.Acos(dot0) * Mathf.Rad2Deg;
            var dot1 = Vector2.Dot(Vector2.up, rot1.normalized);
            var prevDegree1 = Mathf.Acos(dot1) * Mathf.Rad2Deg;

            currentDegree += prevDegree1 - prevDegree0;

            // �����
            Debug.Log($"Rotate �� {currentDegree}");
        }
        return currentDegree;
    }
}
