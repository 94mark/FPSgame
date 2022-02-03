using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    //에너미 상태 상수
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State; //에너미 상태 변수
    public float findDistance = 8f; //플레이어 발견 범위
    Transform player; //플레이어 트랜스폼
    public float attackDistance = 2f; //공격 가능 범위
    public float moveSpeed = 5f; //이동 속도
    CharacterController cc; //캐릭터 콘트롤러 
    float currentTime = 0; //누적 시간
    float attackDelay = 2f; //공격 딜레이 시간
    public int attackPower = 3; //에너미의 공격력
    Vector3 originPos; //초기 위치 저장용 변수
    public float moveDistance = 20f; //이동 가능 범위
    public int hp = 15; //에너미의 체력
    int maxHp = 15; //에너미의 최대 체력
    public Slider hpSlider; //에너미 hp Slider 변수
    Animator anim; //애니메이터 변수


    void Start()
    {
        //최초의 에너미 상태는 대기(Idle)로 한다
        m_State = EnemyState.Idle;

        //플레이어의 트랜스폼 컴포넌트 받아오기
        player = GameObject.Find("Player").transform;

        //캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();

        //자신의 초기 위치 저장
        originPos = transform.position;

        //자식 오브젝트로부터 애니메이터 변수 받아오기
        anim = transform.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //현재 상태를 체크해 해당 상태별로 정해진 기능을 수행하게 하고 싶다
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

        //현재 hp(%)를 hp 슬라이더의 value에 반영
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        //플레이어와의 거리가 액션 시작 범위 이내라면 Move 상태로 전환
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환 : Idle -> Move");

            //이동 애니메이션으로 전환하기
            anim.SetTrigger("IdleToMove");
        }
    }
    void Move()
    {
        //만일 현재 위치가 초기 위치에서 이동 가능 범위를 넘긴다면
        if(Vector3.Distance(transform.position, player.position) > moveDistance)
        {
            //현재 상태를 복귀(Return)로 전환
            m_State = EnemyState.Return;
            print("상태 전환 : Move -> Return");
        }
        //만일 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 향해 이동
        else if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            //이동 방향 설정
            Vector3 dir = (player.position - transform.position).normalized;

            //캐릭터 콘트롤러를 이용해 이동
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            m_State = EnemyState.Attack;
            print("상태 전환 : Move -> Attack");

            //누적 시간을 공격 딜레이 시간만큼 미리 진행시켜 놓음
            currentTime = attackDelay;
        }
    }
    void Attack()
    {
        //만일 플레이어가 공격 범위 이내에 있다면 플레이어 공격
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            //일정한 시간마다 플레이어를 공격
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;
            }
        }
        //그렇지 않다면 현재 상태를 이동으로 전환(재추격)
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환 : Attack -> Move");
            currentTime = 0;
        }
    }
    void Return()
    {
        //만일 초기 위치에서의 거리가 0.1f 이상이라면 초기 위치 쪽으로 이동
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        //그렇지 않다면 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기로 전환
        else
        {
            transform.position = originPos;
            //hp 다시 회복
            hp = maxHp;
            m_State = EnemyState.Idle;
            print("상태 전환 : Return -> Idle");
        }
    }
    //데미지 실행 함수
    public void HitEnemy(int hitPower)
    {
        //만일, 이미 피격 상태이거나 사망 상태 또는 복귀 상태라면 아무런 처리도 하지 않고 함수 종료
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        //플레이어의 공격려간큼 에너미의 체력을 감소시킨다
        hp -= hitPower;
        //에너미의 체력이 0보다 크면 피격 상태로 전환
        if(hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환 : Any state -> Damaged");
            Damaged();
        }
        //그렇지 않다면 죽음 상태로 전환
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환 : Any state -> Die");
            Die();
        }
    }

    void Damaged()
    {
        //피격 상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DamageProcess());
    }
    //데미지 처리용 코루틴 함수
    IEnumerator DamageProcess()
    {
        //피격 모션 시간만큼 기다린다
        yield return new WaitForSeconds(0.5f);

        //현재 상태를 이동 상태로 전환
        m_State = EnemyState.Move;
        print("상태 전환 : Damaged -> Move");
    }
    //죽음 상태 함수
    void Die()
    {
        //진행 중인 피격 코루틴 중지
        StopAllCoroutines();

        //죽음 상태를 처리하기 위한 코루틴 실행
        StartCoroutine(DieProcess());
    }
    IEnumerator DieProcess()
    {
        //캐릭터 콘트롤러 컴포넌트를 비활성화
        cc.enabled = false;

        //2초 동안 기다린 후에 자기 자신 제거
        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }
}
