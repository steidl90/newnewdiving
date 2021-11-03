using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroySound : MonoBehaviour
{
    private static bool isVibration = true;
    public GameObject[] VibrateOn;
    public GameObject[] VibrateOff;

    private void Start()
    {
        if(isVibration)
        {
            OnVibrate();
        }
        else
        {
            OffVibrate();
        }
    }

    public void SoundPlay(Vector3 pos)
    {
        transform.position = pos;
        GetComponent<AudioSource>().Play();
#if UNITY_ANDROID
        if (isVibration)
            Handheld.Vibrate();
#endif
    }

    public void OnVibrate()
    {
        isVibration = true;
        VibrateOn[0].gameObject.SetActive(true);
        VibrateOn[1].gameObject.SetActive(false);
        VibrateOff[0].gameObject.SetActive(false);
        VibrateOff[1].gameObject.SetActive(true);
    }
    public void OffVibrate()
    {
        isVibration = false;
        VibrateOn[0].gameObject.SetActive(false);
        VibrateOn[1].gameObject.SetActive(true);
        VibrateOff[0].gameObject.SetActive(true);
        VibrateOff[1].gameObject.SetActive(false);

    }
}
