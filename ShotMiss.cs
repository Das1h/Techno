using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMiss : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("laser"))
        {
            TextManager.missCount++;
        }
    }
}