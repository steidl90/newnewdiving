using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterWaterEffect : MonoBehaviour
{
    public GameObject BigSplash;
    public bool isSplash;
    private float splashFlag = 0f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && isSplash)
        {
            isSplash = false;
            var particles = gameObject.transform.GetChild(0);
            particles.position = other.transform.position;

            if (splashFlag == 0f)
            {
                StartCoroutine(TriggerSplash());
            }
        }
    }

    IEnumerator TriggerSplash()
    {
        splashFlag = 1f;
        BigSplash.SetActive(true);
        yield return new WaitForSeconds(3f);
        BigSplash.SetActive(false);
        splashFlag = 0f;
    }
}
