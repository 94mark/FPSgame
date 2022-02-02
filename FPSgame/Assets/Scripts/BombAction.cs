using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    //충돌했을 때의 처리
    private void OnCollisionEnter(Collision collision)
    {
        //자기 자신 제거
        Destroy(gameObject);
    }
}
