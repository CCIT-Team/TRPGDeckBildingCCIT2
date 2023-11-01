using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDummy : Unit
{ 
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public float atk;
    public float ap;
    public float def;
    public float apdef;

    public bool isMyturn;

    protected override float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if (value <= 0)
            {
                battleState = BattleState.Death;
                N_BattleManager.instance.ExitBattle(this);
            }
            else
            {
                hp = value;
            }
        }
    }

    void Start()
    {
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
