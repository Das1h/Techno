using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlash;

    public float shotSpeed;
    private int shotCount = 0;

    public int getShotCount()
    {
        return shotCount;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            shotCount++;
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            GameObject flash = (GameObject)Instantiate(muzzleFlash, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);

            //射撃されてから2秒後に銃弾のオブジェクトを破壊する.

            Destroy(bullet, 2.0f);
            Destroy(flash, 0.3f);
        }
    }
}
