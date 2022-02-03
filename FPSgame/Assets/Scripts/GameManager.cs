using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글턴 변수
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    //게임 상태 상수
    public enum GameState
    {
        Ready,
        Run,
        GameOver
    }

    //현재의 게임 상태 변수
    public GameState gState;
    //게임 상태 UI 텍스트 컴포넌트 변수
    Text gameText;
    //게임 상태 UI 오브젝트 변수
    public GameObject gameLabel;

    void Start()
    {
        //초기 게임 상태는 준비 상태로 설정
        gState = GameState.Ready;

        //게임 상태 UI 오브젝트에서 Text 컴포넌트를 가져온다
        gameText = gameLabel.GetComponent<Text>();

        //상태 텍스트의 내용을 Ready로 한다
        gameText.text = "Ready...";

        //상태 텍스트의 색상을 주황색으로 한다
        gameText.color = new Color32(255, 185, 0, 255);

        //게임 준비 -> 게임 중 상태로 전환
        StartCoroutine(ReadyToStart());
    }

    IEnumerator ReadyToStart()
    {
        //2초간 대기
        yield return new WaitForSeconds(2f);
        //상태 텍스트의 내용을 Go로 한다
        gameText.text = "Go!";
        //0.5초간 대기
        yield return new WaitForSeconds(0.5f);
        //상태 텍스를 비활성화
        gameLabel.SetActive(false);
        //상태를 '게임 중' 상태로 변경
        gState = GameState.Run;
    }    
}
