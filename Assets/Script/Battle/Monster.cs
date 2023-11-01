using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { None = 0 ,Undead };

public class Monster : Unit
{
    public int monsterNo;   //30000001
    string monsterName;     //해골 병사   
    int level;	            // 3
    MonsterType type;       //언데드
    float strength;         //70
    float intelligence;     //20
    float luck;	            //49
    public int speed;     //45

    public float atk;
    public float ap;
    public float def;
    public float apdef;

    int action1;	            //50000001
    int action2	;               //50000002
    int action3	;               //50000003
    float giveExp	;           //15
    int dropGold;	            //15
    int dropitem1;	            //12001002	
    int percentage1	;           //10	
    int dropitem2;	            //22000002	
    int percentage2;	        //10
    int dropitem3	;           //22000003
    int percentage3;            //10

    public bool isMyturn;

    protected override float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                battleState = BattleState.Death;
                N_BattleManager.instance.ExitBattle(this);
            }
        }
    }   //40

    void Start()
    {
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
