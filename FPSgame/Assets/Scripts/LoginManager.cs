using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //������� ���̵�� Ű(key)�� �н����带 ��(value)���� ������ ����
        PlayerPrefs.SetString(id.text, password.text);
    }
}
