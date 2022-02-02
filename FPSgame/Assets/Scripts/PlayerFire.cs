using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //발사 위치
    public GameObject bombFactory; //투척 무기 오브젝트
    public float throwPower = 15f; //투척 파워

    void Update()
    {
        // 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던진다

        //1. 마우스 오른쪽 버튼 입력
        if(Input.GetMouseButtonDown(1))
        {
            //수류탄 오브젝트를 생성한 후 수류탄의 생성 위치를 발사 위치로 한다
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //수류탄 오브젝트의 Rigidbody 컴포넌트 
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            //카메라의 정면 방향으로 수류탄에 물리적 힘을 가함
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
    }
}
