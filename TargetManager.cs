using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject TargetC;
    [SerializeField] private GameObject BossObject;

    const int targetNum = 14;
    
    //オブジェクト入れるやつ
    GameObject[] tarList = new GameObject[targetNum];
    GameObject[] tarListC = new GameObject[targetNum];
    GameObject Boss;

    const float upside_y = 3.835f;
    const float downside_y = 0.96f;
    
    Vector3[] vectorsList = new Vector3[]
    {
    new Vector3(-5.62f, upside_y, 5.532f),
    new Vector3(-3.935f, upside_y, 7.005f),
    new Vector3(-2.66f, upside_y, 8.64f),
    new Vector3(0f, upside_y, 9.93f),
    new Vector3(2.52f, upside_y, 8.84f),
    new Vector3(4.03f, upside_y, 7.06f),
    new Vector3(5.22f, upside_y, 4.85f),
    new Vector3(-4.12f, downside_y + 0.5f, 4.15f),
    new Vector3(-3.935f, downside_y, 7.005f),
    new Vector3(-2.66f, downside_y, 8.64f),
    new Vector3(0, downside_y, 8.93f),
    new Vector3(2.65f, downside_y, 8.62f),
    new Vector3(4.03f, downside_y,7.06f),
    new Vector3(5.61f, downside_y,5.66f)
    };

    Vector3 bossPoint = new Vector3(0f, 4.08f, 5.362f);

    //乱数用リスト
    private List<int> numList = new List<int>();

    //乱数の入れ物
    private int rndNum;

    //湧き周期
    private float span = 1.24f;

    //強い敵の確率
    private int c_rarity;

    //今いる数
    public static int current_tarNum = 0;

    //敵が強くなる数
    private int enhancePoint = 5;

    //倒した数
    public static int killCount = 0;

    //難易度
    private int difficulty = 1;

    public void increaseDifficulty()
    {
        difficulty++;
    }

    Timer timer = new Timer();

    private bool canEngance = false;
    private bool canSetBoss = false;

    private void Update()
    {
        //enhancePointを超えたらcanEnhance = true
        if (killCount % enhancePoint == 0 && killCount != 0) canEngance = true;
        if (killCount % (enhancePoint * 6) == 0 && killCount != 0) canSetBoss = true;
    }

    IEnumerator spawnTarget()
    {
        while (timer.getThereResult() == false)
        {            
            // 敵を一定数(enhancePoint)倒すと強敵の出現確率増加、敵の出現速度↑
            if (canEngance)
            {
                c_rarity++;
                if(span > 1.1f)
                {
                    span -= 0.015f;
                }
                canEngance = false;
            }

            if (canSetBoss)
            {
                setBoss(true);
                canSetBoss = false;
            }

            c_rarity = 2;

            setTargets();
            if(difficulty >= 2 && Random.Range(0, 10) < 3)
            {
                setTargets();
            }else if (difficulty >= 3 )
            {
                setTargets();
            }

            yield return new WaitForSeconds(span);
        }
        StopCoroutine("spawnTarget");
        destroyAllTargets();
    }

    private void destroyAllTargets()
    {
        for(int i = 0; i < targetNum; i++)
        {
            tarListC[i].SetActive(false);
            tarList[i].SetActive(false);
            setBoss(false);
        }
    }

    private bool isBossActive = false;

    public void setBoss(bool active)
    {
        if(isBossActive != active)
        {
            Boss.SetActive(active);
            isBossActive = active;
        }
    }

    public void setTargets()
    {
        if (numList.Count <= 0)
        {
            setNumList();
        }

        int index = Random.Range(0, numList.Count);
        rndNum = numList[index];
        numList.RemoveAt(index);

        if (tarList[rndNum].activeSelf == false && tarListC[rndNum].activeSelf == false)
        {
            if (Random.Range(0, 10) < c_rarity)
            {
                tarListC[rndNum].SetActive(true);
                // 出現してから一定時間後に消える
                //Invoke("setTargetC_false", 6f);
            }
            else
            {
                tarList[rndNum].SetActive(true);
                // 出現してから一定時間後に消える
                //Invoke("setTarget_false", 6f);
            }
            current_tarNum++;
        }
    }

    private void setTargetC_false()
    {
        tarListC[rndNum].SetActive(false);
    }

    private void setTarget_false()
    {
        tarList[rndNum].SetActive(false);
    }


    private void OnEnable()
    {
        int listNum;
        GameObject tmpTarget;
        GameObject tmpTargetC;
        GameObject tmpBoss;
        for(listNum = 0; listNum < vectorsList.Length; listNum++)
        {
            tmpTarget = Instantiate(Target, vectorsList[listNum], Quaternion.Euler(0f, 180f, 0f));
            tmpTarget.name = "target_" + listNum.ToString();
            tarList[listNum] = GameObject.Find("target_" + listNum.ToString());
            tarList[listNum].SetActive(false);

            tmpTargetC = Instantiate(TargetC, vectorsList[listNum], Quaternion.Euler(0f, 180f, 0f));
            tmpTargetC.name = "target_" + listNum.ToString() + "_C";
            tarListC[listNum] = GameObject.Find("target_" + listNum.ToString() + "_C");
            tarListC[listNum].SetActive(false);
        }

        tmpBoss = Instantiate(BossObject, bossPoint, Quaternion.Euler(0f, 180f, 0f));
        tmpBoss.name = "Boss";
        Boss = GameObject.Find("Boss");
        Boss.SetActive(false);

        setNumList();

        StartCoroutine("spawnTarget");
    }
    
    private void setNumList()
    {
        for (int i = 0; i < targetNum; i++)
        {
            numList.Add(i);
        }
    }

    private void OnDisable()
    {
        killCount = 0;
        current_tarNum = 0;
    }
}