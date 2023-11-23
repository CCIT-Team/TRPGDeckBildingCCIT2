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
    public int maxCost;

    public int level;
    public int exp;
    public int maxExp;
    public int gold = 0;

    public int attackGuard = 0;
    public int magicGuard = 0;

    public int portionRegular = 0;
    public int portionLarge = 0;

    public bool isMyturn;
    public WorldState worldState = WorldState.Idle;

    private string insertQuery;

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
        attackGuard = 0;
        magicGuard = 0;
    }

    public string GetStatDBQuery()
    {
        insertQuery = $"INSERT INTO Stat (playerNum, strength, intelligence, luck, speed, currentHp, hp, cost, level, exp, maxExp, gold, turn) VALUES " +
            $"({GetComponent<Character_type>().playerNum}, {strength}, {intelligence}, {luck}, {speed}, {hp}, {maxHp}, {maxCost}, {level}, {exp}, {maxExp}, {gold}, {Convert.ToInt32(isMyturn)})";
        return insertQuery;
    }

    public void SetExp(int _exp)
    {
        int temp = 0;
        temp = exp + _exp;

        if(temp >= maxExp)
        {
            LevelUp(temp);
        }
    }

    private void LevelUp(int temp)
    {
        level++;
        exp = temp - maxExp;
        maxExp = 100 + (level - 1) * 50;

        maxHp = maxHp + DataBase.instance.defaultData[0].hpRise;
        strength = strength + DataBase.instance.defaultData[0].strengthRise;
        intelligence = intelligence + DataBase.instance.defaultData[0].intelligenceRise;
        luck = luck + DataBase.instance.defaultData[0].luckRise;
        speed = speed + DataBase.instance.defaultData[0].speedRise;

        if(level % 8 == 0)
        {
            maxCost++;
        }
    }
}
