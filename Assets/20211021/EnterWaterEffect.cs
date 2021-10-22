using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWaterEffect : MonoBehaviour
{
    public GameObject BigSplash;
    private float splashFlag;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asdasd");

        if (splashFlag == default)
        {
            StartCoroutine(TriggerSplash());
        }
    }

    IEnumerator TriggerSplash()
    {
        splashFlag = 1;
        BigSplash.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        BigSplash.SetActive(false);
        splashFlag = 0;
    }
}
