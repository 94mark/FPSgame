using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; //��ǥ�� �� Ʈ������ ������Ʈ

    void Update()
    {
        //ī�޶��� ��ġ�� ��ǥ Ʈ�������� ��ġ�� ��ġ��Ų��
        transform.position = target.position;
    }
}
