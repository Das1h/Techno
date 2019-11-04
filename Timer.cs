using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //トータル制限時間
    private float totalTime;
    //制限時間（分
    [SerializeField] private int minute;
    //制限時間(秒
    [SerializeField] private float seconds;
    //前回Updateの秒数
    private float oldSeconds;
    private Text timerText;

    [SerializeField] private GameObject resultPrefab;
    [SerializeField] private AudioClip battle;
    [SerializeField] private AudioClip resultSound;
    private AudioSource audioSource;   

    private void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        timerText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = battle;
        audioSource.Play();
    }

    private static bool there_result;

    public bool getThereResult()
    {
        return there_result;
    }
    
    private void Update()
    {
        if(totalTime <= 0)
        {
            return;
        }
        totalTime = minute * 60 + seconds;
        totalTime -= Time.deltaTime;

        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        if((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSeconds = seconds;

        if(totalTime <= 0f && there_result == false)
        {
            audioSource.Stop();
            audioSource.clip = resultSound;
            audioSource.Play();
            resultPrefab.SetActive(true);
            there_result = true;
        }
    }

    private void OnDisable()
    {
        there_result = false;
    }
}
