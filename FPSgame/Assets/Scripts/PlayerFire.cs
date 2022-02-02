using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ô ���� ������Ʈ
    public float throwPower = 15f; //��ô �Ŀ�

    void Update()
    {
        // ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������

        //1. ���콺 ������ ��ư �Է�
        if(Input.GetMouseButtonDown(1))
        {
            //����ź ������Ʈ�� ������ �� ����ź�� ���� ��ġ�� �߻� ��ġ�� �Ѵ�
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //����ź ������Ʈ�� Rigidbody ������Ʈ 
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            //ī�޶��� ���� �������� ����ź�� ������ ���� ����
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
    }
}
