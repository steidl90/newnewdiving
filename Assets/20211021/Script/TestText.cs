using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestText : MonoBehaviour
{
    private void Update()
    {
        var text = GetComponent<Text>();
        text.text = $"{Vars.sector}, {Vars.stage}";
    }
}
