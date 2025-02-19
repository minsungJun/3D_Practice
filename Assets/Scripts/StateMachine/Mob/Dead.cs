using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : IState
{
    private Action action;
    public Dead(Action action)
    {
        this.action = action;
    }
    
    public void OperateEnter()
    {
        //Debug.Log("Dead enter");
        action.anim.SetBool("DoRun", false);
        action.anim.SetBool("DoAttack", false);
        action.anim.SetBool("DoDead", true);
    }

    public void OperateExit()
    {
        
    }

    public void OperateUpdate()
    {
        
    }
}



