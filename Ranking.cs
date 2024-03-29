﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    private int rankSize = 10;
    private string path = "./GunShooting_Data/Score.txt";       //ビルドした時用
    //private string path = "./Assets/Scripts/Score/Score.txt"; //Editorの時用

    private void OnEnable()
    {
        int score = TextManager.killPoint - TextManager.missCount;
        //スコアを出力
        string scoreData = File.ReadAllText(path);
        //Debug.Log(scoreData);
        scoreData = score.ToString() + "\n" + scoreData;
        File.WriteAllText(path, scoreData);

        //またスコアデータを読み込む
        string[] scores = File.ReadAllLines(path);

        //intへ
        int[] intScores = scores.Select(int.Parse).ToArray();

        //降順ソート
        Array.Sort(intScores, (a, b) => b -a);

        
        int[] rankArray = new int[rankSize];

        if(rankSize > intScores.Length)
        {
            for (int i = 0; i < intScores.Length; i++)
            {
                rankArray[i] = intScores[i];
            }
        }
        else
        {
            for (int i = 0; i < rankSize; i++)
            {
                rankArray[i] = intScores[i];
            }
        }

        //配列を1つのテキストに
        string rankingText = string.Join("\n", rankArray);

        //Debug.Log(rankingText);

        //表示
        GetComponent<Text>().text = rankingText;
    }
}
