using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ô ���� ������Ʈ
    public float throwPower = 15f; //��ô �Ŀ�
    public GameObject bulletEffect; //�ǰ� ����Ʈ ������Ʈ
    ParticleSystem ps; //�ǰ� ����Ʈ ��ƼŬ �ý���

    void Start()
    {
        //�ǰ� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ 
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

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

        //���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� �� �߻�

        //���콺 ���� ��ư �Է�
        if(Input.GetMouseButtonDown(0))
        {
            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //���̰� �ε��� ����� ������ ������ ������ ����
            RaycastHit hitInfo = new RaycastHit();

            //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� �̺�Ʈ ����
            if(Physics.Raycast(ray, out hitInfo))
            {
                //�ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵�
                bulletEffect.transform.position = hitInfo.point;

                //�ǰ� ����Ʈ �÷���
                ps.Play();
            }
        }
    }
}
