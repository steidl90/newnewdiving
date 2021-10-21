using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void ButtonOff()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
