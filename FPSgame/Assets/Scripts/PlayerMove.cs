using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //이동 속도 변수
    CharacterController cc; //캐릭터 콘트롤러 변수
    float gravity = -20f; //중력 변수
    float yVelocity = 0; //수직 속력 변수
    public float jumpPower = 10f; //점프력 변수

    void Start()
    {
        //캐릭터 컨트롤러 컴포넌트
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //1. 사용자 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2. 이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1. 메인 카메라를 기준으로 방향 변환
        dir = Camera.main.transform.TransformDirection(dir);

        //2-2. 점프 구현
        if(Input.GetButtonDown("Jump"))
        {
            //캐릭터 수직 속도에 점프력 적용
            yVelocity = jumpPower;
        }

        //2-3. 캐릭터 수직 속도에 중력 값을 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //3. 속도에 맞춰 이동
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
