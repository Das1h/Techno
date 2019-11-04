using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{   

    public static int killPoint = 0;    //スコア
    public static int missCount = 0;    //ミス回数
    
    void Update()
    {
        Text scoreText = gameObject.GetComponent<Text>();
        
        scoreText.text = (killPoint - missCount).ToString() + "点";
    }

    private void OnDisable()
    {
        killPoint = 0;
        missCount = 0;
    }
}
