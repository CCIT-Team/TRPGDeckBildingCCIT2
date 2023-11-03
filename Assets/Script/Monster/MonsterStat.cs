using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Unit
{
    public int no;
    public string _name;
    public int level;
    public MonsterData.Type type;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public int action1;
    public int action2;
    public int action3;

    public int giveExp;
    public int dropGold;

    public int dropitem1;
    public int dropitem1Percentage;
    public int dropitem2;
    public int dropitem2Percentage;
    public int dropitem3;
    public int dropitem3Percentage;

    public void SetMonsterData(MonsterData stat)
    {
        no = stat.no;
        _name = stat.name;
        level = stat.level;
        type = stat.type;
        hp = stat.hp;
        maxHp = hp;
        strength = stat.strength;
        intelligence = stat.intelligence;
        luck = stat.luck;
        speed = stat.speed;

        action1 = stat.action1;
        action2 = stat.action2;
        action3 = stat.action3;

        giveExp = stat.giveExp;
        dropGold = stat.dropGold;

        dropitem1 = stat.dropitem1;
        dropitem1Percentage = stat.dropitem1Percentage;
        dropitem2 = stat.dropitem2;
        dropitem2Percentage = stat.dropitem2Percentage;
        dropitem3 = stat.dropitem3;
        dropitem3Percentage = stat.dropitem3Percentage;

    }
}
