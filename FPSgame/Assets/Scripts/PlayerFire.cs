using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ô ���� ������Ʈ
    public float throwPower = 15f; //��ô �Ŀ�
    public GameObject bulletEffect; //�ǰ� ����Ʈ ������Ʈ
    ParticleSystem ps; //�ǰ� ����Ʈ ��ƼŬ �ý���
    public int weaponPower = 5; //�߻� ���� ���ݷ�
    Animator anim; //�ִϸ����� ����
    public Text wModeText; //���� ��� �ؽ�Ʈ
    public GameObject[] eff_Flash; //�� �߻� ȿ�� ������Ʈ �迭
    //���� ������ ��������Ʈ ����
    public GameObject weapon01;
    public GameObject weapon02;
    //ũ�ν���� ��������Ʈ ����
    public GameObject crosshair01;
    public GameObject crosshair02;


    //���� ��� ����
    enum WeaponMode
    {
        Normal,
        Sniper
    }
    WeaponMode wMode;
    bool ZoomMode = false; //ī�޶� Ȯ�� Ȯ�ο� ����


    void Start()
    {
        //�ǰ� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ 
        ps = bulletEffect.GetComponent<ParticleSystem>();
        //�ִϸ����� ������Ʈ ��������
        anim = GetComponentInChildren<Animator>();
        //���� �⺻ ��带 ��� ���� ����
        wMode = WeaponMode.Normal;
    }

    void Update()
    {
        //���� ���°� '���� ��' ������ ���� ������ �� �ְ� �Ѵ�
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        //��� ��� : ���콺 ������ ��ư�� ������ �ü��� �ٶ󺸴� �������� ����ź�� ������
        //�������� ��� : ���콺 ������ ��ư�� ������ ȭ�� Ȯ��

        //���콺 ������ ��ư �Է�
        if(Input.GetMouseButtonDown(1))
        {
            switch(wMode)
            {
                case WeaponMode.Normal:
                    //����ź ������Ʈ�� ������ �� ����ź�� ���� ��ġ�� �߻� ��ġ�� �Ѵ�
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position = firePosition.transform.position;

                    //����ź ������Ʈ�� Rigidbody ������Ʈ 
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    //ī�޶��� ���� �������� ����ź�� ������ ���� ����
                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

                    break;
                case WeaponMode.Sniper:
                    //ī�޶� Ȯ���ϰ� �ܸ�� ���·� ����
                    if(!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    }
                    //�׷��� ������ ī�޶� ���� ���·� �ǵ����� �� ��� ���� ����
                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        ZoomMode = false;
                    }
                    break;
            }
        }    

        //���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� �� �߻�
        //���콺 ���� ��ư �Է�
        if(Input.GetMouseButtonDown(0))
        {
            //���� �̵� ���� Ʈ�� �Ķ������ ���� 0�̶��, ���� �ִϸ��̼� ����
            if(anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Attack");
            }
            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //���̰� �ε��� ����� ������ ������ ������ ����
            RaycastHit hitInfo = new RaycastHit();

            //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� �̺�Ʈ ����
            if(Physics.Raycast(ray, out hitInfo))
            {
                //���� ���̿� �ε��� ����� ���̾ Enemy��� ������ �Լ� ����
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                //�׷��� �ʴٸ� ���̿� �ε��� ������ �ǰ� ����Ʈ �÷���
                else
                {
                    //�ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵�
                    bulletEffect.transform.position = hitInfo.point;

                    //�ǰ� ����Ʈ�� forward ������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��
                    bulletEffect.transform.forward = hitInfo.normal;

                    //�ǰ� ����Ʈ �÷���
                    ps.Play();
                }
            }
            //�� ����Ʈ �ǽ�
            StartCoroutine(ShootEffectOn(0.05f));
        }
        //���� Ű������ ���� 1�� �Է��� ������, ���� ��带 �Ϲ� ���� ����
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;
            //ī�޶� ȭ���� �ٽ� ������� �����ش�
            Camera.main.fieldOfView = 60f;
            //�Ϲ� ��� �ؽ�Ʈ ���
            wModeText.text = "Normal Mode";
            //1�� ��������Ʈ�� Ȱ��ȭ�ǰ�, 2�� ��������Ʈ�� ��Ȱ��ȭ�ȴ�
            weapon01.SetActive(true);
            weapon02.SetActive(false);
            crosshair01.SetActive(true);
            crosshair02.SetActive(false);
        }
        //���� Ű������ ���� 2�� �Է��� ������ ���� ��带 �������� ���� ����
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;
            //�������� ��� �ؽ�Ʈ ���
            wModeText.text = "Sniper Mode";
            //1�� ��������Ʈ�� ��Ȱ��ȭ�ǰ�, 2�� ��������Ʈ�� Ȱ��ȭ�ȴ�
            weapon01.SetActive(false);
            weapon02.SetActive(true);
            crosshair01.SetActive(false);
            crosshair02.SetActive(true);
        }
    }
    //�ѱ� ����Ʈ �ڷ�ƾ �Լ�
    IEnumerator ShootEffectOn(float duration)
    {
        //�����ϰ� ���ڸ� ����
        int num = Random.Range(0, eff_Flash.Length);
        //����Ʈ ������Ʈ �迭���� ���� ���ڿ� �ش��ϴ� ����Ʈ ������Ʈ Ȱ��ȭ
        eff_Flash[num].SetActive(true);
        //������ �ð���ŭ ���
        yield return new WaitForSeconds(duration);
        //����Ʈ ������Ʈ�� �ٽ� ��Ȱ��ȭ
        eff_Flash[num].SetActive(false);
    }
}
