using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //�̵� �ӵ� ����
    CharacterController cc; //ĳ���� ��Ʈ�ѷ� ����
    float gravity = -20f; //�߷� ����
    float yVelocity = 0; //���� �ӷ� ����
    public float jumpPower = 10f; //������ ����

    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //1. ����� �Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2. �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1. ���� ī�޶� �������� ���� ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        //2-2. ���� ����
        if(Input.GetButtonDown("Jump"))
        {
            //ĳ���� ���� �ӵ��� ������ ����
            yVelocity = jumpPower;
        }

        //2-3. ĳ���� ���� �ӵ��� �߷� ���� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //3. �ӵ��� ���� �̵�
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
