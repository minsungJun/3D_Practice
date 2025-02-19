using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : IState
{
    private Action action;

    private Transform target; // 추적할 대상
    public Chase(Action action)
    {
        this.action = action;
    }
    
    
    public void OperateEnter()
    {
        Debug.Log("StateChase enter");
        action.anim.SetBool("DoRun", false);
        action.anim.SetBool("DoAttack", false);
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(action.transform.position, action.PatrolRadius + action.ChaseRadius, LayerMask.GetMask("Player"));
        if(colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                //Debug.Log($"Detected object chase: {collider.name}");
            }

            target = colliders[0].transform; // 첫 번째 감지된 대상
            action.agent.SetDestination(target.position); // NavMeshAgent로 추적
            action.anim.SetBool("DoRun", true);

            Collider[] colliders1 = Physics.OverlapSphere(action.transform.position, action.PatrolRadius - action.AttackRadius, LayerMask.GetMask("Player"));//공격범위 판정
            if(colliders1.Length > 0)
            {
                action.anim.SetBool("DoRun", false);
                action.stateMachine.SetState(action.dicState[Action.State.Attack]);
                
            }
        }
        else
        {
            target = null; // 대상을 잃으면 멈춤
            action.agent.ResetPath();
            action.stateMachine.SetState(action.dicState[Action.State.Patrol]);
        }
        
    }
}

