using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotspeed;
    
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawTouch.LIndexTrigger))
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotspeed);

            //射撃されてから2秒後に銃弾のオブジェクトを破壊する.

            Destroy(bullet, 1.0f);
        }
    }
}
