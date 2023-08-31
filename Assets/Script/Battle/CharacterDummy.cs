using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { None = -1, Wait, MyTurn, Dead }

public class CharacterDummy : MonoBehaviour
{
    State state = State.Wait;

    [Header("Stats")]
    [SerializeField]
    int vital = 0;
    [SerializeField]
    int strength = 0;
    [SerializeField]
    int intelligence = 0;
    [SerializeField]
    int luck = 0;
    [SerializeField]
    int hit = 0;
    [SerializeField]
    int speed = 0;

    public State State
    {
        get { return state; }
        set
        { 
            state = value;
            switch (state)
            {
                case State.Wait:
                    break;
                case State.MyTurn:
                    break;
                case State.Dead:
                    break;
            }
        }
    }
    public float VIT
    {
        get { return vital; }
        set { value = vital; }
    }
    public float STR
    {
        get { return strength; }
        set { value = strength; }
    }
    public float INT
    {
        get { return intelligence; }
        set { value = intelligence; }
    }
    public float LUK
    {
        get { return luck; }
        set { value = luck; }
    }
    public float HIT
    {
        get { return hit; }
        set { value = hit; }
    }
    public float SPD
    {
        get { return speed; }
        set { value = speed; }
    }

    public delegate void Test(CharacterDummy o,int i);
    public Test ttt;

    void Start()
    {
        //BattleManager.instance.characters.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.MyTurn)
        {
            print(this.name + ": My turn!");
            
        }
    }


}
