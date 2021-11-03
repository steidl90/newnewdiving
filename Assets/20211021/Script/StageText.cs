using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageText : MonoBehaviour
{
    private void Start()
    {
        var text = GetComponent<Text>();
        var height = GameManager.gameManager.GetComponent<Stage>().height;
        if ((int)Vars.mode < 2) // 0 1
        {
            text.text = $"{Vars.sector}(Normal)" + "\n" +
                $"{Vars.stage * height}m";
        }
        else if((int)Vars.mode > 1) // 2 3
        {
            text.text = $"{Vars.sector}(Hard)" + "\n" +
                $"{Vars.stage * height}m";
        }
    }
}
