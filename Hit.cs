using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Hit : MonoBehaviour
{
    //Targetが壊れるエフェクト
    [SerializeField] private GameObject destroyEffect;
    
    Timer targetCycle = new Timer(3000);
    private void Start()
    {
        Debug.Log("started");
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("HIT");
        if (other.gameObject.CompareTag("shell"))
        {
            
            gameObject.SetActive(false);
            Debug.Log("set false");
            Destroy(other.gameObject);
            
            GameObject dest = (GameObject)Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(dest, 1.0f);
        }
    }
}