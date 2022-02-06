using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    //����� �����͸� ���� �����ϰų� ����� �����͸� �о� ������� �Է°� ��ġ�ϴ��� �˻�

    //����� ���̵� ����
    public InputField id;
    //����� �н����� ����
    public InputField password;
    //�˻� �׽�Ʈ ����
    public Text notify;

    void Start()
    {
        //�˻� �ؽ�Ʈ â�� ����
        notify.text = "";
    }

    //���̵�� �н����� ���� �Լ�
    public void SaveUserData()
    {
        //���� �ý��ۿ� ����� �ִ� ���̵� �������� �ʴ´ٸ�
        if(!PlayerPrefs.HasKey(id.text))
        {
            //������� ���̵�� Ű(key)�� �н����带 ��(value)���� ������ ����
            PlayerPrefs.SetString(id.text, password.text);
            notify.text = "���̵� ������ �Ϸ�ƽ��ϴ�";
        }
        //�׷��� ������ �̹� �����Ѵٴ� �޽��� ���
        else
        {
            notify.text = "�̹� �����ϴ� ���̵��Դϴ�";
        }        
    }

    //�α��� �Լ�
    public void CheckUserData()
    {
        //����ڰ� �Է��� ���̵� Ű�� ����� �ý��ۿ� ����� ���� �ҷ���
        string pass = PlayerPrefs.GetString(id.text);

        //���� ����ڰ� �Է��� �н������ �ý��ۿ��� �ҷ��� ���� ���� �����ϴٸ�
        if (password.text == pass)
        {
            //���� ��(1�� ��) �ε�
            SceneManager.LoadScene(1);
        }
        //�׷��� �ʰ� �� �������� ���� �ٸ���, ����� ���� ����ġ �޽����� �����
        else
        {
            notify.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�";
        }
    }
}
