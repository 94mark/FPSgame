using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�̱��� ����
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    //���� ���� ���
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    //������ ���� ���� ����
    public GameState gState;
    //���� ���� UI �ؽ�Ʈ ������Ʈ ����
    Text gameText;
    //���� ���� UI ������Ʈ ����
    public GameObject gameLabel;
    //PlayerMove Ŭ���� ����
    PlayerMove player;
    public GameObject gameOption; //�ɼ� ȭ�� UI ������Ʈ ����

    void Start()
    {
        //�ʱ� ���� ���´� �غ� ���·� ����
        gState = GameState.Ready;

        //���� ���� UI ������Ʈ���� Text ������Ʈ�� �����´�
        gameText = gameLabel.GetComponent<Text>();

        //���� �ؽ�Ʈ�� ������ Ready�� �Ѵ�
        gameText.text = "Ready...";

        //���� �ؽ�Ʈ�� ������ ��Ȳ������ �Ѵ�
        gameText.color = new Color32(255, 185, 0, 255);

        //���� �غ� -> ���� �� ���·� ��ȯ
        StartCoroutine(ReadyToStart());

        //�÷��̾� ������Ʈ�� ã�� �� �÷��̾��� PlayerMove ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator ReadyToStart()
    {
        //2�ʰ� ���
        yield return new WaitForSeconds(2f);
        //���� �ؽ�Ʈ�� ������ Go�� �Ѵ�
        gameText.text = "Go!";
        //0.5�ʰ� ���
        yield return new WaitForSeconds(0.5f);
        //���� �ؽ��� ��Ȱ��ȭ
        gameLabel.SetActive(false);
        //���¸� '���� ��' ���·� ����
        gState = GameState.Run;
    }

    void Update()
    {
        //���� �÷��̾��� hp�� 0�̶��
        if(player.hp <= 0)
        {
            //�÷��̾��� �ִϸ��̼��� �����
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0f);
            //���� �ؽ�Ʈ�� Ȱ��ȭ
            gameLabel.SetActive(true);
            //���� �ؽ�Ʈ�� ������ Game Over�� �Ѵ�
            gameText.text = "Game Over";
            //���� �ؽ�Ʈ�� ������ ���������� �Ѵ�
            gameText.color = new Color32(255, 0, 0, 255);
            //���� �ؽ�Ʈ�� �ڽ� ������Ʈ�� Ʈ������ ������Ʈ
            Transform buttons = gameText.transform.GetChild(0);
            //��ư ������Ʈ Ȱ��ȭ
            buttons.gameObject.SetActive(true);
            //���¸� '���� ����' ���·� ����
            gState = GameState.GameOver;
        }
    }

    //�ɼ� ȭ�� �ѱ�
    public void OpenOptionWindow()
    {
        //�ɼ� â�� Ȱ��ȭ
        gameOption.SetActive(true);
        //���� �ӵ��� 0������� ��ȯ
        Time.timeScale = 0f;
        //���� ���¸� �Ͻ� ���� ���·� ����
        gState = GameState.Pause;
    }

    //����ϱ� �ɼ�
    public void CloseOptionWindow()
    {
        //�ɼ� â�� ��Ȱ��ȭ
        gameOption.SetActive(false);
        //���� �ӵ��� 1������� ��ȯ
        Time.timeScale = 1f;
        //���� ���¸� ���� �� ���·� ����
        gState = GameState.Run;
    }

    //�ٽ��ϱ� �ɼ�
    public void RestartGame()
    {
        //���� �ӵ��� 1������� ��ȯ
        Time.timeScale = 1f;

        //���� �� ��ȣ�� �ٽ� �ε�
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //�ε� ȭ�� ���� �ε�
        SceneManager.LoadScene(1);
    }
    //���� ���� �ɼ�
    public void QuitGame()
    {
        //���ø����̼� ����
        Application.Quit();
    }
}
