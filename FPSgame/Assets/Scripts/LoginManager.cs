using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    //사용자 데이터를 새로 저장하거나 저장된 데이터를 읽어 사용자의 입력과 일치하는지 검사

    //사용자 아이디 변수
    public InputField id;
    //사용자 패스워드 변수
    public InputField password;
    //검사 테스트 변수
    public Text notify;

    void Start()
    {
        //검사 텍스트 창을 비운다
        notify.text = "";
    }

    //아이디와 패스워드 저장 함수
    public void SaveUserData()
    {
        //사용자의 아이디는 키(key)로 패스워드를 값(value)으로 설정해 저장
        PlayerPrefs.SetString(id.text, password.text);
    }
}
