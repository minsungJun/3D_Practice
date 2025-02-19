using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    // Start is called before the first frame update
    public CapsuleCollider capcollider;
    public float Hp;
    public float MaxHp;
    public float Mp;
    public Action action;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }
    void Dead()
    {
        if(Hp <= 0)
        {
            action.Dead();
            capcollider.enabled = false;
            Invoke("exit", 5f);
        }
    }
    void exit()
    {
        Destroy(this.gameObject);
    }
}
