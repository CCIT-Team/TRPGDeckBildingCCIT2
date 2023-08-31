using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum State
    {
        None,
        Idle,
        Attack,
        Hit,
        //È¸ÇÇ?
        Death
    }

    [SerializeField]
    protected State CurrentState;
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
                CurrentState = State.Death;
            }
            else
            {
                hp = value;
            }
        }
    }

    public float Damaged(float atk)
    {
        hp = hp - atk;
        return hp;
    }

}
