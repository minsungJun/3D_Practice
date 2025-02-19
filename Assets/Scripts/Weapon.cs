using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage;
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

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(time1);
        meleearea.enabled = true;
        traileffect.enabled = true;

        yield return new WaitForSeconds(time2);
        meleearea.enabled = false;

        yield return new WaitForSeconds(time3);
        traileffect.enabled = false;
            
    }

    public void stop()
    {
        Debug.Log("stop");
        meleearea.enabled = false;
        traileffect.enabled = false;
        gameObject.GetComponentInParent<Action>().isAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "감지 끝!");
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyStat stat = other.gameObject.GetComponentInParent<EnemyStat>();
            stat.Hp -= damage;
            Debug.Log(stat.Hp); 
        }
    }


}
