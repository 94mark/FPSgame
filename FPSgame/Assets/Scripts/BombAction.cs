using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    //�浹���� ���� ó��
    private void OnCollisionEnter(Collision collision)
    {
        //�ڱ� �ڽ� ����
        Destroy(gameObject);
    }
}
