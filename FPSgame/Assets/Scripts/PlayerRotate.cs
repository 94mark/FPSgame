using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f; //ȸ�� �ӵ� ����
    float mx = 0; //ȸ�� �� ����

    void Update()
    {
        //������� ���콺 �Է��� �޾� �÷��̾ ȸ��
        //1. ���콺 �¿� �Է��� �޴´�
        float mouse_X = Input.GetAxis("Mouse X");

        //1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ����
        mx += mouse_X * rotSpeed * Time.deltaTime;
        //2. ȸ�� �������� ��ü ȸ��
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
