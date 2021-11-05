using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroySound : MonoBehaviour
{
    public GameObject[] VibrateOn;
    public GameObject[] VibrateOff;

    private void Start()
    {
        if(Vars.isVibration)
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
        if (Vars.isVibration)
            Handheld.Vibrate();
#endif
    }

    public void OnVibrate()
    {
        Vars.isVibration = true;
        VibrateOn[0].gameObject.SetActive(true);
        VibrateOn[1].gameObject.SetActive(false);
        VibrateOff[0].gameObject.SetActive(false);
        VibrateOff[1].gameObject.SetActive(true);
    }
    public void OffVibrate()
    {
        Vars.isVibration = false;
        VibrateOn[0].gameObject.SetActive(false);
        VibrateOn[1].gameObject.SetActive(true);
        VibrateOff[0].gameObject.SetActive(true);
        VibrateOff[1].gameObject.SetActive(false);

    }
}
