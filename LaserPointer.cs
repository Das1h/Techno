using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private GameObject PointerObject;
    [SerializeField] private GameObject Gun;

    private GameObject tmpPointer;
    private GameObject pointer;

    private Vector3 hitPos;
    private Vector3 pointerPos; //仮のポインターの位置

    private Transform myTransform;

    private float distance = 20f;
    
    private void Start()
    {
        //ポインターの出現位置を入れる
        hitPos = new Vector3 (0f, 0f, 0f);
        //ポインターのオブジェクトを格納
        tmpPointer = Instantiate(PointerObject, hitPos, Quaternion.Euler(0f, 0f, 0f));
        tmpPointer.name = "tmpPointer";
        pointer = GameObject.Find("tmpPointer");
        pointer.SetActive(false);
        //transformを取得
        myTransform = pointer.transform;
    }
    
    private void Update()
    {
        //検知用のRay
        Ray ray = new Ray(Gun.transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (!hit.collider.CompareTag("shell") && !hit.collider.CompareTag("charged"))
            {
                //当たった座標をとる
                hitPos = hit.point;
                //ポインターの座標を入れるヤツを作る(transform.position)
                pointerPos = myTransform.position;
                //上のヤツを当たった座標に変える
                pointerPos = hit.point;
                //変えたものを元のヤツに入れる
                myTransform.position = pointerPos;
                //myTransform.position = hit.point;
                pointer.SetActive(true);
            }
        } else
        {
            pointer.SetActive(false);
        }
    }
}