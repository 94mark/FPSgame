using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //���� �ؽ�Ʈ�� Ȱ��ȭ
            gameLabel.SetActive(true);
            //���� �ؽ�Ʈ�� ������ Game Over�� �Ѵ�
            gameText.text = "Game Over";
            //���� �ؽ�Ʈ�� ������ ���������� �Ѵ�
            gameText.color = new Color32(255, 0, 0, 255);
            //���¸� '���� ����' ���·� ����
            gState = GameState.GameOver;
        }
    }
}
