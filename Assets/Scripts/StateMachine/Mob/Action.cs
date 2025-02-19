using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Action : MonoBehaviour
{
    // Start is called before the first frame update
    public enum State
    {
        Chase,
        Attack,
        Patrol,
        Dead
    }

    public StateMachine stateMachine;
    private EnemyWeapon weapon;
    private BossAttack bossAttack;
    public Dictionary<State, IState> dicState = new Dictionary<State, IState>();

    public Animator anim;
    public NavMeshAgent agent; // NavMeshAgent
    public string type;

    public float PatrolRadius = 10f;
    public float ChaseRadius = 4f;
    public float AttackRadius = 8f;
    public int damage = 0;
    public bool isAttack = false;
    private int beforenum = -1;

    //public object NavMesh { get; internal set; }

    void Start()
    {
        IState chase = new Chase(this);
        IState dead = new Dead(this);
        IState attack = new Attack(this);
        IState patrol = new Patrol(this);

        dicState.Add(State.Chase, chase);
        dicState.Add(State.Patrol, patrol);
        dicState.Add(State.Dead, dead);
        dicState.Add(State.Attack, attack);

        stateMachine = new StateMachine(patrol);
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        weapon = GetComponentInChildren<EnemyWeapon>();
        if(type == "boss")
        {
            bossAttack = GetComponentInChildren<BossAttack>();
            Debug.Log("boss test");
        }


        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("NavMesh 위에 있지 않습니다. 위치를 조정합니다.");

            // NavMesh 위의 가장 가까운 위치 찾기
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                // NavMesh 위로 이동
                transform.position = hit.position;
                agent.Warp(hit.position); // NavMeshAgent 업데이트
                Debug.Log("NavMesh 위로 위치를 이동했습니다.");
            }
            else
            {
                Debug.LogError("NavMesh 위로 배치할 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("이미 NavMesh 위에 있습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.DoOperateUpdate();
    }

    public void AnimationEventStart()
    {
        weapon.eventstart();
    }

    public void AnimationEventStop()
    {
        weapon.eventstop();
    }

    public void BossAnimationStart()
    {
        if(anim.GetBool("Claw") == true)
        {
            bossAttack.eventstart("Claw");
        }
        if(anim.GetBool("Flame") == true)
        {
            bossAttack.eventstart("Flame");
        }
        if(anim.GetBool("Basic") == true)
        {
            bossAttack.eventstart("Basic");
        }
        if(anim.GetBool("Scream") == true)
        {
            bossAttack.eventstart("Scream");
        }
    }
    public void BossAnimationStop()
    {
        bossAttack.eventstop();
        anim.SetBool("Claw", false);
        anim.SetBool("Flame", false);
        anim.SetBool("Basic", false);
        anim.SetBool("Scream", false);
    }

    public void Attack()
    {
        //Debug.Log("isAttack = " + isAttack);
        //Debug.Log("attack test2  " + isAttack);
        if(isAttack == false)
        {
            isAttack = true;
            Debug.Log("action.attack");
            int rand = UnityEngine.Random.Range(0, 4);
            while(beforenum == rand)
            {
                rand = UnityEngine.Random.Range(0, 4);
                Debug.Log("while");
            }
            switch(rand)
            {
                case 0:
                    anim.SetBool("Claw", true);
                    break;
                case 1:
                    anim.SetBool("Flame", true);
                    break;
                case 2:
                    anim.SetBool("Basic", true);
                    break;
                case 3:
                    anim.SetBool("Scream", true);
                    break;

            }
            beforenum = rand;
        }
        
    }
    

    public void Dead()
    {
        if(stateMachine.CurrentState != dicState[State.Dead])
        {
            stateMachine.SetState(dicState[State.Dead]);
        }
        
    }








    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PatrolRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PatrolRadius + ChaseRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PatrolRadius - AttackRadius);
    }
}
