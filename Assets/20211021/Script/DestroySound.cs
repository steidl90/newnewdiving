using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    public void SoundPlay(Vector3 pos)
    {
        transform.position = pos;
        GetComponent<AudioSource>().Play();
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }
}
