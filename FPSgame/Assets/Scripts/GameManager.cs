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
}
