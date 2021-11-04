using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] totalSound;
    private static float soundVolume = 1;
    private Slider volume;
    private void Awake()
    {
        volume = GameManager.gameManager.uiManager.GetComponent<UIManager>().settings.transform.GetComponentInChildren<Slider>();
        volume.value = soundVolume;
    }

    public void VolumeChange()
    {
        soundVolume = volume.value;
        for (int i = 0; i < totalSound.Length; i++)
        {
            totalSound[i].volume = soundVolume;
        }
    }

}
