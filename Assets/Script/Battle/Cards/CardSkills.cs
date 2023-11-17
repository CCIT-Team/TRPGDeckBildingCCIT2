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
    //PhysicalAttack 

        public static void DefaultPhysicalAttack(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            int mainStat = 0;
            int succeceCount = 0;
            if (performer.TryGetComponent(out Character character))
            {
                mainStat = character.strength;
                for (int i = 0; i < token; i++)
                {
                    int a = UnityEngine.Random.Range(0, 100);
                    if (a <= mainStat)
                        Debug.Log("성공");
                    else
                        Debug.Log("실패");
                }
            }


            else
                Debug.Log(0);
            target.Damaged(damage);
        }

        public static void Slash(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void Smite(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void BitingStrike(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void BoldStrike(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void TrickStrike(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void DoubleAttack(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void EvilStrike(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Bash(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void Cleave(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void AlphaStrike(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void SpinAttack(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Stroming(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void Brandish(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void ShieldSlam(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        
        public static void Judgment(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        
        public static void SwingAttack(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void HolyFlame(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Pain(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void SwingAttackv(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Attack(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void EnergyBall(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

    // MagicAttack

        public static void DefaultMagicAttack(Unit performer, Unit target, int damage, int extraEffect, int token)
        {target.Damaged(damage);
        }

        public static void FireBall(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void IceBall(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void ThunderBolt(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void ManaBullet(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void ManaBomb(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void ShootingStar(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void ManaShower(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void BurningFlame(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void FlameofDragon(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Flamethrower(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Pillaroflight(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void HolyArrow(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

    // Defence

        public static void Blessofprotect(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void BulkUp(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }

        public static void Defcon(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void FlameArmor(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Guard(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void MagicShield(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Shield(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }
        public static void Shieldoflight(Unit performer, Unit target, int damage, int extraEffect, int token)
        {
            target.Damaged(damage);
        }


    // Buff
        public static void DefaultBuff(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Parry(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Agility(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void BlessofIntelligence(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void BlessofStrength(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Heal(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Focus(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void DoubleCharm(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void ManaCirculation(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void ManaAmplification(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Scheme(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Meditation(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Ignition(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Pray(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Blessoflight(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Salvation(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void LightAround(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Purification(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }
        public static void Contemplation(Unit performer, Unit target, float value, int extraEffect, int token )
        {

        }

    // DeBuff
}
