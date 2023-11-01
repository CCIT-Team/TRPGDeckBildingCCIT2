using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSkills
{
    public class PhysicalAttack
    {
        public static void SingleAttack(Unit unit, float damage)
        {
            unit.Damaged(damage);
        }

        public static void RangeAttack(List<Unit> units, float damage)
        {
            foreach (Unit unit in units)
            {
                unit.Damaged(damage);
            }
        }

        public static void AreaAttack(List<Unit> units, float damage)
        {
            foreach (Unit unit in units)
            {
                unit.Damaged(damage);
            }
        }
    }

    public class MagicAttack
    {
        public static void SingleAttack(Unit unit, float damage)
        {
            unit.Damaged(damage);
        }

        public static void AreaAttack(List<Unit> units, float damage)
        {
            foreach (Unit unit in units)
            {
                unit.Damaged(damage);
            }
        }
    }

    public class Defence
    {

    }

    public class Special
    {

    }
}
