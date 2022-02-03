using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f; //�̵� �ӵ� ����
    CharacterController cc; //ĳ���� ��Ʈ�ѷ� ����
    float gravity = -20f; //�߷� ����
    float yVelocity = 0; //���� �ӷ� ����
    public float jumpPower = 10f; //������ ����
    public bool isJumping = false; //���� ���� ����
    public int hp = 20; //�÷��̾� ü�� ����\
    int maxHp = 20; //�ִ� ü�� ����
    public Slider hpSlider; //hp �����̴� ����

    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //1. ����� �Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //2. �̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1. ���� ī�޶� �������� ���� ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        //2-2. ���� ���̰�, �ٽ� �ٴڿ� �����ߴٸ�
        if(isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            //���� �� ���·� �ʱ�ȭ
            isJumping = false;
            //ĳ���� ���� �ӵ��� 0
            yVelocity = 0;
        }

        //2-3. Ű���� space ��ư �Է�, ������ �� �� ���¶��
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            //ĳ���� ���� �ӵ��� �������� �����ϰ� ���� ���·� ����
            yVelocity = jumpPower;
            isJumping = true;
        }

        //2-4. ĳ���� ���� �ӵ��� �߷� ���� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //3. �ӵ��� ���� �̵�
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //4. ���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ�
        hpSlider.value = (float)hp / (float)maxHp;
    }

    //�÷��̾� �ǰ� �Լ�
    public void DamageAction(int damage)
    {
        //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü���� ��´�
        hp -= damage;
    }
}
