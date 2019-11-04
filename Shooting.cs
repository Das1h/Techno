using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject chargeEffectPrefab;

    public float shotSpeed;

    public AudioClip sound;
    AudioSource audioSource;

    private GameObject tmpEffect;
    private GameObject chargeEffect;
    private Transform effectTransform;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        tmpEffect = Instantiate(chargeEffectPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
        tmpEffect.name = "chargeEffect";
        chargeEffect = GameObject.Find("chargeEffect");
        effectTransform = chargeEffect.transform;
        chargeEffect.SetActive(false);
    }
    
    private float chargeTime = 0;

    private bool completeCharge = false;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            audioSource.PlayOneShot(sound);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            GameObject flash = Instantiate(muzzleFlash, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            shotBullet(bulletRb);

            //射撃されてから2秒後に銃弾のオブジェクトを破壊する.

            Destroy(bullet, 2.0f);
            Destroy(flash, 0.3f);
        }else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            chargeEffect.SetActive(false);
            if(chargeTime > 1.3f)
            {
                audioSource.PlayOneShot(sound);
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                GameObject flash = Instantiate(muzzleFlash, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * 1.5f, bullet.transform.localScale.x * 1.5f, bullet.transform.localScale.x * 1.5f);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                shotBullet(bulletRb);
                completeCharge = false;

                bullet.gameObject.tag = "charged";

                Destroy(bullet, 2.0f);
                Destroy(flash, 0.3f);
            }

            chargeTime = 0;
        }

        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            chargeTime += Time.deltaTime;
            if(chargeTime > 0.05f)
            {
                chargeEffect.SetActive(true);
                effectTransform.position = gameObject.transform.position;
            }

            if(chargeTime >= 1.3f && completeCharge == false)
            {
                audioSource.PlayOneShot(audioSource.clip);
                completeCharge = true;
            }
        }

    }

    private void shotBullet(Rigidbody bulletRb)
    {
        bulletRb.AddForce(transform.forward * shotSpeed);
    }
}