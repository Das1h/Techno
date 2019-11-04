using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetData : MonoBehaviour
{
    //Targetが壊れるエフェクト
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private int initHP;
    private int HP;

    private GameObject tm;

    //public AudioClip sound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HP = initHP;
        tm = GameObject.Find("TargetManager");
    }

    private void OnEnable()
    {
        HP = initHP;
    }
        
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("shell") || other.gameObject.CompareTag("charged"))
        {
            HP--;
            if (other.gameObject.CompareTag("charged"))
            {
                HP -= 2;
            }
            Destroy(other.gameObject);
            if (HP <= 0)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, gameObject.transform.position);
                gameObject.SetActive(false);
                TargetManager.current_tarNum--;
                if (gameObject.CompareTag("custom"))
                {
                    TextManager.killPoint += 10;
                }
                else if(gameObject.CompareTag("mob"))
                {
                    TextManager.killPoint += 3;
                }
                TargetManager.killCount++;

                GameObject dest = Instantiate(destroyEffect, transform.position, transform.rotation);
                Destroy(dest, 1.0f);

                if (Random.Range(0, 100) < 35)
                {
                    tm.GetComponent<TargetManager>().setTargets();
                }
            }
        }

        if (other.gameObject.CompareTag("charged") && gameObject.CompareTag("boss"))
        {
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(audioSource.clip, gameObject.transform.position);
            gameObject.SetActive(false);

            GameObject dest = Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(dest, 1.0f);

            TextManager.killPoint += 100;
            tm.GetComponent<TargetManager>().setBoss(false);
            tm.GetComponent<TargetManager>().increaseDifficulty();

            TargetManager.killCount++;
        }
    }
}