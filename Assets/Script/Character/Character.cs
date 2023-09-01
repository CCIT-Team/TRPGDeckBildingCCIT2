using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    public enum WorldState
    {
        None,
        Idle,
        Walking,
        Death
    }
    public int playerNum;
    [SerializeField] private PlayerStat.AvatarType type;
    [SerializeField] private PlayerStat.Major major;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public float atk;
    public float def;
    public int cost;

    public bool isMyturn;
    public WorldState worldState;


    public void SetUnitData(PlayerStat stat)
    {
        playerNum = stat.playerNum;
        type = stat.type;
        major = stat.major;
        maxHp = stat.hp;
        hp = maxHp;
        strength = stat.strength;
        intelligence = stat.intelligence;
        luck = stat.luck;
        speed = stat.speed;
        atk = stat.atk;
        def = stat.def;
        cost = stat.cost;
    }
}
