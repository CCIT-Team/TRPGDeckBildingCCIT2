using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum BattleState
    {
        None,
        Idle,
        Attack,
        Hit,
        //È¸ÇÇ?
        Death
    }

    [SerializeField]
    protected BattleState battleState;

    [SerializeField]
    public float hp;
    public virtual float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if(value <= 0)
            {
                hp = 0;
                battleState = BattleState.Death;
            }
            else
            {
                hp = value;
            }
        }
    }

    [SerializeField] public float maxHp = 0.0f;

    public float Damaged(float atk)
    {
        Hp = Hp - atk;
        return Hp;
    }

}
