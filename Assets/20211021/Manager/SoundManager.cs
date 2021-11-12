using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] totalSound;
    private Slider volume;
    private void Awake()
    {
        volume = GameManager.gameManager.uiManager.GetComponent<UIManager>().settings.transform.GetComponentInChildren<Slider>();
        volume.value = StaticVariable.soundVolume;
    }

    public void VolumeChange()
    {
        StaticVariable.soundVolume = volume.value;
        for (int i = 0; i < totalSound.Length; i++)
        {
            totalSound[i].volume = StaticVariable.soundVolume;
        }
    }

}
