using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Pause,
        GameOver
    }

    //현재의 게임 상태 변수
    public GameState gState;
    //게임 상태 UI 텍스트 컴포넌트 변수
    Text gameText;
    //게임 상태 UI 오브젝트 변수
    public GameObject gameLabel;
    //PlayerMove 클래스 변수
    PlayerMove player;
    public GameObject gameOption; //옵션 화면 UI 오브젝트 변수

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

        //플레이어 오브젝트를 찾은 후 플레이어의 PlayerMove 컴포넌트 받아오기
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
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

    void Update()
    {
        //만일 플레이어의 hp가 0이라면
        if(player.hp <= 0)
        {
            //플레이어의 애니메이션을 멈춘다
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0f);
            //상태 텍스트를 활성화
            gameLabel.SetActive(true);
            //상태 텍스트의 내용을 Game Over로 한다
            gameText.text = "Game Over";
            //상태 텍스트의 색상을 붉은색으로 한다
            gameText.color = new Color32(255, 0, 0, 255);
            //상태 텍스트의 자식 오브젝트의 트랜스폼 컴포넌트
            Transform buttons = gameText.transform.GetChild(0);
            //버튼 오브젝트 활성화
            buttons.gameObject.SetActive(true);
            //상태를 '게임 오버' 상태로 변경
            gState = GameState.GameOver;
        }
    }

    //옵션 화면 켜기
    public void OpenOptionWindow()
    {
        //옵션 창을 활성화
        gameOption.SetActive(true);
        //게임 속도를 0배속으로 전환
        Time.timeScale = 0f;
        //게임 상태를 일시 정지 상태로 변경
        gState = GameState.Pause;
    }

    //계속하기 옵션
    public void CloseOptionWindow()
    {
        //옵션 창을 비활성화
        gameOption.SetActive(false);
        //게임 속도를 1배속으로 전환
        Time.timeScale = 1f;
        //게임 상태를 게임 중 상태로 변경
        gState = GameState.Run;
    }

    //다시하기 옵션
    public void RestartGame()
    {
        //게임 속도를 1배속으로 전환
        Time.timeScale = 1f;

        //현재 씬 번호를 다시 로드
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //로딩 화면 씬을 로드
        SceneManager.LoadScene(1);
    }
    //게임 종료 옵션
    public void QuitGame()
    {
        //애플리케이션 종료
        Application.Quit();
    }
}
