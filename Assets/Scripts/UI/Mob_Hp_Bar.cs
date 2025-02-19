using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mob_Hp_Bar : MonoBehaviour
{
    // Start is called before the first frame update
    float Max_Hp;
    float Now_Hp;

    [SerializeField] string type;
    Vector3 initialScale;
    TMP_Text childTMPText;
    EnemyStat stat;
    void Start()
    {
        stat = GetComponentInParent<EnemyStat>();
        Max_Hp = stat.MaxHp;
        //Max_Mp = stat.Maxmp;
        if(type == "HP")
        {
            childTMPText = GameObject.Find("Hp_text").GetComponent<TMP_Text>();
        }
        initialScale = transform.localScale;
        Debug.Log(initialScale + " " + transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        Now_Hp = stat.Hp;

        update_UI();
    }

    void update_UI()
    {

            float scaleRatio = Now_Hp / Max_Hp;
            if(Now_Hp >= 0)
            {
                transform.localScale = new Vector3(initialScale.x * scaleRatio, initialScale.y);
                childTMPText.text = Now_Hp +  " / " + Max_Hp;
            }
            else if(Now_Hp < 0)
            {
                transform.localScale = new Vector3(0, initialScale.y);
                //childTMPText.text = "0 / " + Max_Hp;
            }

        
    }
}
