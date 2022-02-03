using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //이동 속도 변수
    CharacterController cc; //캐릭터 콘트롤러 변수
    float gravity = -20f; //중력 변수
    float yVelocity = 0; //수직 속력 변수
    public float jumpPower = 10f; //점프력 변수
    public bool isJumping = false; //점프 상태 변수
    public int hp = 20; //플레이어 체력 변수\
    int maxHp = 20; //최대 체력 변수
    public Slider hpSlider; //hp 슬라이더 변수

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

        //2-2. 점프 중이고, 다시 바닥에 착지했다면
        if(isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            //점프 전 상태로 초기화
            isJumping = false;
            //캐릭터 수직 속도를 0
            yVelocity = 0;
        }

        //2-3. 키보드 space 버튼 입력, 점프를 안 한 상태라면
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            //캐릭터 수직 속도에 점프력을 적용하고 점프 상태로 변경
            yVelocity = jumpPower;
            isJumping = true;
        }

        //2-4. 캐릭터 수직 속도에 중력 값을 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //3. 속도에 맞춰 이동
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //4. 현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영
        hpSlider.value = (float)hp / (float)maxHp;
    }

    //플레이어 피격 함수
    public void DamageAction(int damage)
    {
        //에너미의 공격력만큼 플레이어의 체력을 깎는다
        hp -= damage;
    }
}
