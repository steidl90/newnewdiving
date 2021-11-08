using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageText : MonoBehaviour
{
    private void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();
        var height = GameManager.gameManager.GetComponent<Stage>().height;
        text.text = $"{Vars.totalstage} Stage, {Vars.stage * height} m";
    }
}
