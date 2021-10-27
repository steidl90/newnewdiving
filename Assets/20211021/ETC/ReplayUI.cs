using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayUI : MonoBehaviour
{
    private Text text;

    private void OnReplay()
    {
        gameObject.SetActive(true);
    }
}
