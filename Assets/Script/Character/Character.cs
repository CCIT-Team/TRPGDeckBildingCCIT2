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
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public float atk;
    public float ap;
    public float def;
    public float apdef;
    private int currentCost;
    public int cost;

    public bool isMyturn;
    public WorldState worldState = WorldState.Idle;

    private string insertQuery;

    public void SetUnitData(PlayerStat stat)
    {
        maxHp = stat.hp;
        hp = maxHp;
        strength = stat.strength;
        intelligence = stat.intelligence;
        luck = stat.luck;
        speed = stat.speed;
        atk = stat.atk;
        ap = stat.ap;
        def = stat.def;
        apdef = stat.apdef;
        cost = stat.cost;
        currentCost = cost;
    }

    public string GetStatDBQuery()
    {
        insertQuery = $"INSERT INTO Stat (playerNum, strength, intelligence, luck, speed, currentHp, hp, atk, ap, def, apdef, cost) VALUES " +
            $"({GetComponent<Character_type>().playerNum}, {strength}, {intelligence}, {luck}, {speed}, {hp}, {maxHp}, {atk}, {ap}, {def}, {apdef}, {cost})";
        return insertQuery;
    }
}
