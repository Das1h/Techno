using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private void OnEnable()
    {
        Text resultText = GetComponent<Text>();

        resultText.text = "倒した数 " + TargetManager.killCount.ToString() + "回\n"+
                          "外した数 " + TextManager.missCount.ToString() +"回\n"+
                          "\n合計スコア " + (TextManager.killPoint - TextManager.missCount).ToString();
    }
}
