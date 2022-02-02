using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; //목표가 될 트랜스폼 컴포넌트

    void Update()
    {
        //카메라의 위치를 목표 트랜스폼의 위치에 일치시킨다
        transform.position = target.position;
    }
}
