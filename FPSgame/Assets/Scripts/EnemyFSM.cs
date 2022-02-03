using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    //���ʹ� ���� ���
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State; //���ʹ� ���� ����
    public float findDistance = 8f; //�÷��̾� �߰� ����
    Transform player; //�÷��̾� Ʈ������
    public float attackDistance = 2f; //���� ���� ����
    public float moveSpeed = 5f; //�̵� �ӵ�
    CharacterController cc; //ĳ���� ��Ʈ�ѷ� 
    float currentTime = 0; //���� �ð�
    float attackDelay = 2f; //���� ������ �ð�
    public int attackPower = 3; //���ʹ��� ���ݷ�
    Vector3 originPos; //�ʱ� ��ġ ����� ����
    public float moveDistance = 20f; //�̵� ���� ����
    public int hp = 15; //���ʹ��� ü��
    int maxHp = 15; //���ʹ��� �ִ� ü��
    public Slider hpSlider; //���ʹ� hp Slider ����
    Animator anim; //�ִϸ����� ����


    void Start()
    {
        //������ ���ʹ� ���´� ���(Idle)�� �Ѵ�
        m_State = EnemyState.Idle;

        //�÷��̾��� Ʈ������ ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").transform;

        //ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        //�ڽ��� �ʱ� ��ġ ����
        originPos = transform.position;

        //�ڽ� ������Ʈ�κ��� �ִϸ����� ���� �޾ƿ���
        anim = transform.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //���� ���¸� üũ�� �ش� ���º��� ������ ����� �����ϰ� �ϰ� �ʹ�
        switch(m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        //���� hp(%)�� hp �����̴��� value�� �ݿ�
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        //�÷��̾���� �Ÿ��� �׼� ���� ���� �̳���� Move ���·� ��ȯ
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ : Idle -> Move");

            //�̵� �ִϸ��̼����� ��ȯ�ϱ�
            anim.SetTrigger("IdleToMove");
        }
    }
    void Move()
    {
        //���� ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �ѱ�ٸ�
        if(Vector3.Distance(transform.position, player.position) > moveDistance)
        {
            //���� ���¸� ����(Return)�� ��ȯ
            m_State = EnemyState.Return;
            print("���� ��ȯ : Move -> Return");
        }
        //���� �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵�
        else if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            //�̵� ���� ����
            Vector3 dir = (player.position - transform.position).normalized;

            //ĳ���� ��Ʈ�ѷ��� �̿��� �̵�
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            m_State = EnemyState.Attack;
            print("���� ��ȯ : Move -> Attack");

            //���� �ð��� ���� ������ �ð���ŭ �̸� ������� ����
            currentTime = attackDelay;
        }
    }
    void Attack()
    {
        //���� �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾� ����
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            //������ �ð����� �÷��̾ ����
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����");
                currentTime = 0;
            }
        }
        //�׷��� �ʴٸ� ���� ���¸� �̵����� ��ȯ(���߰�)
        else
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ : Attack -> Move");
            currentTime = 0;
        }
    }
    void Return()
    {
        //���� �ʱ� ��ġ������ �Ÿ��� 0.1f �̻��̶�� �ʱ� ��ġ ������ �̵�
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        //�׷��� �ʴٸ� �ڽ��� ��ġ�� �ʱ� ��ġ�� �����ϰ� ���� ���¸� ���� ��ȯ
        else
        {
            transform.position = originPos;
            //hp �ٽ� ȸ��
            hp = maxHp;
            m_State = EnemyState.Idle;
            print("���� ��ȯ : Return -> Idle");
        }
    }
    //������ ���� �Լ�
    public void HitEnemy(int hitPower)
    {
        //����, �̹� �ǰ� �����̰ų� ��� ���� �Ǵ� ���� ���¶�� �ƹ��� ó���� ���� �ʰ� �Լ� ����
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        //�÷��̾��� ���ݷ���ŭ ���ʹ��� ü���� ���ҽ�Ų��
        hp -= hitPower;
        //���ʹ��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
        if(hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("���� ��ȯ : Any state -> Damaged");
            Damaged();
        }
        //�׷��� �ʴٸ� ���� ���·� ��ȯ
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ : Any state -> Die");
            Die();
        }
    }

    void Damaged()
    {
        //�ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ ����
        StartCoroutine(DamageProcess());
    }
    //������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        //�ǰ� ��� �ð���ŭ ��ٸ���
        yield return new WaitForSeconds(0.5f);

        //���� ���¸� �̵� ���·� ��ȯ
        m_State = EnemyState.Move;
        print("���� ��ȯ : Damaged -> Move");
    }
    //���� ���� �Լ�
    void Die()
    {
        //���� ���� �ǰ� �ڷ�ƾ ����
        StopAllCoroutines();

        //���� ���¸� ó���ϱ� ���� �ڷ�ƾ ����
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ�� ��Ȱ��ȭ
        cc.enabled = false;

        //2�� ���� ��ٸ� �Ŀ� �ڱ� �ڽ� ����
        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }
}
