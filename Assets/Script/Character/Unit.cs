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
        //ȸ��?
        Death
    }

    [SerializeField]
    protected BattleState battleState;

    [SerializeField]
    protected float hp;
    protected virtual float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if(value <= 0)
            {
                battleState = BattleState.Death;
            }
            else
            {
                hp = value;
            }
        }
    }

    [SerializeField] protected float maxHp = 0.0f;

    public float Damaged(float atk)
    {
        hp = hp - atk;
        return hp;
    }

}
