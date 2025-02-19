using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : IState
{
    private Action action;
    public Patrol(Action action)
    {
        this.action = action;
    }
    
    public void OperateEnter()
    {
        //Debug.Log("Patrol enter");
        action.anim.SetBool("DoRun", false);
        action.anim.SetBool("DoAttack", false);
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(action.transform.position, action.PatrolRadius , LayerMask.GetMask("Player"));
        if(colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                //Debug.Log($"Detected object patrol: {collider.name}");
            }
            action.stateMachine.SetState(action.dicState[Action.State.Chase]);
        }
        else
        {

        }
        if (!action.agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent가 NavMesh 위에 있지 않습니다.");

        }
    }

    
}

