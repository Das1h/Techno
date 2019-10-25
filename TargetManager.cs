using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    
    //オブジェクト入れるやつ
    GameObject[] tarList = new GameObject[12];

    static float upside_y = 3.835f;
    static float downside_y = 0.96f;
        
    public Vector3[] vectorsList = new Vector3[]
    {
    new Vector3(-5.62f, upside_y, 5.532f),
    new Vector3(-3.935f, upside_y, 7.005f),
    new Vector3(-2.66f, upside_y, 8.64f),
    new Vector3(0f, upside_y, 9.93f),
    new Vector3(2.52f, upside_y, 8.84f),
    new Vector3(4.03f, upside_y, 7.06f),
    new Vector3(-3935f, downside_y, 7.005f),
    new Vector3(-2.66f, downside_y, 8.64f),
    new Vector3(0, downside_y, 8.93f),
    new Vector3(2.65f, downside_y, 8.62f),
    new Vector3(4.03f, downside_y,7.06f),
    new Vector3(5.61f, downside_y,5.66f)
    };    

    //乱数のインスタンス
    System.Random rnd = new System.Random();

    //乱数の入れ物
    int rndNum;

    //湧き回数
    int spawnCount = 0;

    
    void spawnTarget()
    {
        Debug.Log("calling spawnTarget");   //呼ばれる

        rndNum = rnd.Next(12);
        Debug.Log(rndNum);  //呼ばれる
        if(tarList[rndNum].activeSelf == false || tarList[rndNum] == null)
        {
            tarList[rndNum].SetActive(true);
            spawnCount++;

            Debug.Log("spawned");   //呼ばれない
        }else if(tarList[rndNum].activeSelf == true)
        {
            Debug.Log("call again");    //呼ばれない

            spawnTarget();
        }
        Debug.Log("exit if");   //呼ばれない
    }

    //湧き周期
    private Timer timer;

    void Start()
    {
        int listNum;
        GameObject tmpTarget;
        for(listNum = 0; listNum < 12; listNum++)
        {
            tmpTarget = (GameObject)Instantiate(Target, vectorsList[listNum], Quaternion.Euler(0f, 0f, 0f));
            tmpTarget.name = "target_" + listNum.ToString();
            tarList[listNum] = GameObject.Find("target_" + listNum.ToString());
            tarList[listNum].SetActive(false);
        }

        TimerCallback callback = state =>
        {
            Debug.Log("callback");
            spawnTarget();
        };

        timer = new Timer(callback, null, 1000, 2000);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnTarget();
        }
    }
}
