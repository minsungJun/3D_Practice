using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 10;
    public float rate;
    public BoxCollider basicarea;
    public BoxCollider flamearea;
    public BoxCollider clawarea;
    public TrailRenderer traileffect;

    public void eventstart(string name)
    {
        //Debug.Log("attack test3");
        if(name == "Claw")
        {
            clawarea.enabled = true;
            traileffect.enabled = true;
        }
        if(name == "Flame")
        {
            flamearea.enabled = true;
            traileffect.enabled = true;
        }
        if(name == "Basic")
        {
            basicarea.enabled = true;
            traileffect.enabled = true;
        }
        if(name == "Scream")
        {

        }
    }
    public void eventstop()
    {
        Debug.Log("attack test4");
        clawarea.enabled = false;
        flamearea.enabled = false;
        basicarea.enabled = false;
        traileffect.enabled = false;
        gameObject.GetComponentInParent<Action>().isAttack = false;
    }
}
