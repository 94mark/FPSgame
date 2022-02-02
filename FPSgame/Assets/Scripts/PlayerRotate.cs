using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f; //회전 속도 변수
    float mx = 0; //회전 값 변수

    void Update()
    {
        //사용자의 마우스 입력을 받아 플레이어를 회전
        //1. 마우스 좌우 입력을 받는다
        float mouse_X = Input.GetAxis("Mouse X");

        //1-1. 회전 값 변수에 마우스 입력 값만큼 미리 누적
        mx += mouse_X * rotSpeed * Time.deltaTime;
        //2. 회전 방향으로 물체 회전
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
