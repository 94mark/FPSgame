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
}
