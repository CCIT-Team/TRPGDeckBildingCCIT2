using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public int cost;
    private int maxCost;

    public int level;
    public int exp;
    public int maxExp;
    public int gold = 0;

    public bool isMyturn;
    public WorldState worldState = WorldState.Idle;

    private string insertQuery;

    public void SetUnitData(PlayerDefaultData stat)
    {
        maxHp = stat.hp;
        hp = maxHp;
        strength = stat.strength;
        intelligence = stat.intelligence;
        luck = stat.luck;
        speed = stat.speed;
        maxCost = stat.cost;
        cost = maxCost;
        level = stat.level;
        exp = stat.exp;
        maxExp = stat.maxExp;
    }

    public void SetUnitData(PlayerStat stat)
    {
        maxHp = stat.maxHp;
        hp = stat.hp;
        strength = stat.strength;
        intelligence = stat.intelligence;
        luck = stat.luck;
        speed = stat.speed;
        maxCost = stat.cost;
        cost = maxCost;
        level = stat.level;
        exp = stat.exp;
        maxExp = stat.maxExp;
        gold = stat.gold;
        isMyturn = stat.turn;
    }

    public string GetStatDBQuery()
    {
        insertQuery = $"INSERT INTO Stat (playerNum, strength, intelligence, luck, speed, currentHp, hp, cost, level, exp, maxExp, gold, turn) VALUES " +
            $"({GetComponent<Character_type>().playerNum}, {strength}, {intelligence}, {luck}, {speed}, {hp}, {maxHp}, {maxCost}, {level}, {exp}, {maxExp}, {gold}, {Convert.ToInt32(isMyturn)})";
        return insertQuery;
    }
}
