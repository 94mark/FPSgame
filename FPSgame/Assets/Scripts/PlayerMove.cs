using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //�̵� �ӵ� ����

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

        //3. �ӵ��� ���� �̵�
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
