using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : IState
{
    private Action action;
    public Attack(Action action)
    {
        this.action = action;
    }
    
    public void OperateEnter()
    {
        Debug.Log("Attack enter");
        action.anim.SetBool("DoRun", false);
        action.anim.SetBool("DoAttack", false);
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(action.transform.position, action.PatrolRadius - action.AttackRadius, LayerMask.GetMask("Player"));
        if(colliders.Length > 0)
        {
            //Debug.Log("attack" + action.type);
            action.agent.ResetPath();
            Transform target = colliders[0].transform;
            Vector3 directionToTarget = (target.position - action.transform.position).normalized;
            if (directionToTarget != Vector3.zero) // Avoid errors when direction is zero
            {
                
                    Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                    action.transform.rotation = Quaternion.Slerp(
                    action.transform.rotation,
                    lookRotation,
                    Time.deltaTime * 5f // Adjust rotation speed here
                    );
                
                
                
            }
            if(action.type == "normal")
            {
                action.anim.SetBool("DoAttack", true);
                
            }
            if(action.type == "boss")
            {
                action.Attack();
                //Debug.Log("attack test1");
            }
            
        }
        else
        {
            if(action.type == "boss")
            {
                action.anim.SetBool("Claw", false);
                action.anim.SetBool("Flame", false);
                action.anim.SetBool("Basic", false);
                action.anim.SetBool("Scream", false);
            }
            else
            {
                action.anim.SetBool("DoAttack", false);
            }
            action.stateMachine.SetState(action.dicState[Action.State.Patrol]);
        }
    }
}

