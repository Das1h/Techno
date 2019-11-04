using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingL : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab_L;
    [SerializeField] private GameObject muzzleFlash_L;

    public float shotSpeed_L;

    public AudioClip sound_L;
    AudioSource audioSource_L;

    private void Start()
    {
        audioSource_L = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            audioSource_L.PlayOneShot(sound_L);
            GameObject bullet = (GameObject)Instantiate(bulletPrefab_L, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            GameObject flash = (GameObject)Instantiate(muzzleFlash_L, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed_L);

            //射撃されてから2秒後に銃弾のオブジェクトを破壊する.

            Destroy(bullet, 2.0f);
            Destroy(flash, 0.3f);
        }
    }
}