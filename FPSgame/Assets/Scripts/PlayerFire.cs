using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition; //�߻� ��ġ
    public GameObject bombFactory; //��ô ���� ������Ʈ
    public float throwPower = 15f; //��ô �Ŀ�
    public GameObject bulletEffect; //�ǰ� ����Ʈ ������Ʈ
    ParticleSystem ps; //�ǰ� ����Ʈ ��ƼŬ �ý���
    public int weaponPower = 5; //�߻� ���� ���ݷ�
    Animator anim; //�ִϸ����� ����

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
        }
    }
}
