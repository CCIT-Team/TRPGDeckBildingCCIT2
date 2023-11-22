using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CardSkills     //사용자, 사용 대상, 값, 추가효과 값, 토큰 수
{
    public static MethodInfo SearchSkill(string SkillName)
    {
        Type type = typeof(CardSkills);
        var method = type.GetMethod(SkillName);
        return method;
    }
    #region PhysicalAttack
    //PhysicalAttack 
    public static void DefaultPhysicalAttack(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        int strength = 0;   //사용하는 주스탯
        int succeceCount = successToken;
        if (performer.TryGetComponent(out Character character))
            strength = character.strength; 
        else
            strength = performer.GetComponent<Monster>().strength;

        target.Damaged(damage);
        target.GetComponent<UnitAnimationControl>().GetDamage();    //영상용 임시
    }

    public static void Slash(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        {
            int strength = 0;   //사용하는 주스탯
            int succeceCount = successToken;
            if (performer.TryGetComponent(out Character character))
                strength = character.strength;
            else
                strength = performer.GetComponent<Monster>().strength;

            Debug.Log("기본데미지 : " + damage + "이번데미지 : " + (damage * (1 - (0.1f * succeceCount))));
            target.Damaged(damage * (1 - (0.1f*succeceCount)));
            //영상용 임시
            target.GetComponent<UnitAnimationControl>().GetDamage();

        }
    }

    public static void Smite(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void BitingStrike(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void BoldStrike(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void TrickStrike(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void DoubleAttack(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void EvilStrike(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Bash(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void Cleave(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void AlphaStrike(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void SpinAttack(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Stroming(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void Brandish(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void ShieldSlam(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void Judgment(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void SwingAttack(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void HolyFlame(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Pain(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void SwingAttackv(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Attack(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void EnergyBall(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    #endregion
    # region MagicAttack

    public static void DefaultMagicAttack(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        int intelligence = 0;
        int succeceCount = successToken;
        if (performer.TryGetComponent(out Character character))
            intelligence = character.intelligence;
        else
            intelligence = performer.GetComponent<Monster>().strength;

        for (int i = 0; i < successToken; i++)
        {
            int x = UnityEngine.Random.Range(0, 100);
            if (x <= intelligence)
            {
                Debug.Log("성공");
            }
            else
            {
                Debug.Log("실패");
                succeceCount--;
            }
        }

        target.Damaged(damage);
    }

    public static void FireBall(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void IceBall(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void ThunderBolt(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void ManaBullet(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void ManaBomb(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void ShootingStar(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void ManaShower(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void BurningFlame(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void FlameofDragon(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Flamethrower(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Pillaroflight(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void HolyArrow(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    #endregion
    #region Defence

    public static void Blessofprotect(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void BulkUp(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }

    public static void Defcon(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void FlameArmor(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Guard(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void MagicShield(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Shield(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    public static void Shieldoflight(Unit performer, Unit target, float damage, int extraEffect, int successToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, successToken);
    }
    #endregion
    #region Buff
    // Buff
    public static void DefaultBuff(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Parry(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Agility(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void BlessofIntelligence(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void BlessofStrength(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Heal(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Focus(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void DoubleCharm(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void ManaCirculation(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void ManaAmplification(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Scheme(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Meditation(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Ignition(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Pray(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Blessoflight(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Salvation(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void LightAround(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Purification(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    public static void Contemplation(Unit performer, Unit target, float value, int extraEffect, int successToken)
    {

    }
    #endregion
    #region DeBuff
    #endregion
}
