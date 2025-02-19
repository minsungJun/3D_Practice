using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage = 10;
    public float rate;
    public BoxCollider meleearea;
    public TrailRenderer traileffect;
    float time1 = 0;
    float time2 = 0;
    float time3 = 0;
    

    public void Use(int damage, float time1, float time2, float time3)
    {
        //Debug.Log("Weapon Use");
        this.damage = damage;
        this.time1 = time1;
        this.time2 = time2;
        this.time3 = time3;

        StopCoroutine("Swing");
        StartCoroutine("Swing");
        
        
    }
    public void eventstart()
    {
        meleearea.enabled = true;
        traileffect.enabled = true;
    }
    public void eventstop()
    {
        meleearea.enabled = false;
        traileffect.enabled = false;
        gameObject.GetComponentInParent<Action>().isAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "감지 끝!" + gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("case 1");
            EnemyStat stat = other.gameObject.GetComponentInParent<EnemyStat>();
            stat.Hp -= damage;
            Debug.Log(stat.Hp);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("case 2");
            Debug.Log(LayerMask.LayerToName(other.gameObject.layer) + " " + LayerMask.LayerToName(gameObject.layer));
            PlayerStat stat = other.gameObject.GetComponentInParent<PlayerStat>();
            stat.Hp -= damage;
            Debug.Log(stat.Hp);
        }
    }


}
