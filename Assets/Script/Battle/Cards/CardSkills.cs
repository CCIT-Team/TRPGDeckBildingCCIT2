using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CardSkills     //사용자, 사용 대상, 값, 추가효과 값, 토큰 수
{
    public static MethodInfo SearchSkill(string SkillName)
    {
        if (SkillName == "Dragon'sScream")
            SkillName = "DragonScream";
        else if (SkillName == "Dragon'sClaw")
            SkillName = "DragonClaw";
        else if (SkillName == "Dragon'sWind")
            SkillName = "DragonWind";

        Type type = typeof(CardSkills);
        var method = type.GetMethod(SkillName);
        return method;
    }

    static float CalculateAttackDamage(Unit target, float damage, int failedToken)
    {
        float finalDamage = damage *= 1 - (0.1f * failedToken);
        if(target.TryGetComponent(out EffectTurnChecker effectTurnChecker)
        && effectTurnChecker.isBuffRun[8 + (int)EffectType.Parry]
        && UnityEngine.Random.Range(0, 100) < 10)
        {
            finalDamage = 0;
            return finalDamage;
        }
        if (target.TryGetComponent(out Character character))
        {
            if (character.attackGuard > damage)
            {
                character.attackGuard -= (int)damage;
                finalDamage = 0;
            }
            else
            {
                finalDamage = damage - character.attackGuard;
                character.attackGuard = 0;
            }
        }
        else
        {
            Monster monster = target.GetComponent<Monster>();
            if (monster.attackGuard > damage)
            {
                monster.attackGuard -= (int)damage;
                finalDamage = 0;
            }
            else
            {
                finalDamage = damage - monster.attackGuard;
                monster.attackGuard = 0;
            }
        }
        return finalDamage;
    }

    static float CalculateMagicDamage(Unit target, float damage, int failedToken)
    {
        float finalDamage = damage *= 1 - (0.1f * failedToken);
        if (target.TryGetComponent(out EffectTurnChecker effectTurnChecker)
        && effectTurnChecker.isBuffRun[8 + (int)EffectType.Parry]
        && UnityEngine.Random.Range(0, 100) < 10)
        {
            finalDamage = 0;
            return finalDamage;
        }
        if (target.TryGetComponent(out Character character))
        {
            if (character.magicGuard > damage)
            {
                character.magicGuard -= (int)damage;
                finalDamage = 0;
            }
            else
            {
                finalDamage = damage - character.magicGuard;
                character.magicGuard = 0;
            }
        }
        else
        {
            Monster monster = target.GetComponent<Monster>();
            if (monster.magicGuard > damage)
            {
                monster.magicGuard -= (int)damage;
                finalDamage = 0;
            }
            else
            {
                finalDamage = damage - monster.magicGuard;
                monster.magicGuard = 0;
            }
        }
        return finalDamage;
    }
    #region PhysicalAttack
    //PhysicalAttack 
    public static void DefaultPhysicalAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            damage = CalculateAttackDamage(target, damage, failedToken);
            target.Damaged((int)damage);
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();    //임시
    }

    public static void DragonClaw(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken, totalToken);
    }

    public static void DragonWind(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
                DefaultPhysicalAttack(performer, unit, damage, extraEffect, failedToken, totalToken);
        }
    }

    public static void Slash(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken, totalToken);
    }

    public static void Smite(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (target.TryGetComponent(out Character character))
        {
            if (character.TryGetComponent(out EffectTurnChecker turnChecker))
            {
                turnChecker.StartEffect(EffectType.Weak, 0, extraEffect);
            }
            else
            {
                turnChecker = character.gameObject.AddComponent<EffectTurnChecker>();
                turnChecker.boundCharacter = character;
                turnChecker.StartEffect(EffectType.Weak, 0, extraEffect);
            }
        }
        else
        {
            if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            {
                turnChecker.StartEffect(EffectType.Strength_Increase, 0, extraEffect);
            }
            else
            {
                turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
                turnChecker.StartEffect(EffectType.Strength_Increase, 0, extraEffect);
            }
        }
        //부가효과 추가 필요
        //2턴 취약
    }
    public static void BitingStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            if(performer.TryGetComponent(out Character character))
            {
                damage += (character.level * 5);
            }
            else
            {
                damage += performer.GetComponent<Monster>().level * 5;
            }
            damage = CalculateAttackDamage(target, damage, failedToken);
            target.Damaged((int)damage);
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }
    public static void BoldStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            if (performer.TryGetComponent<Character>(out Character player))
                foreach (PlayerBattleUI playerUI in BattleUI.instance.playerUI)
                {
                    if (!playerUI.gameObject.activeSelf)
                        continue;
                    if (player == playerUI.boundCharacter)
                    {
                        Debug.Log("캐릭 고름");
                        int matchCardCount = 0;
                        for (int i = 0; i < playerUI.hand.childCount; i++)
                        {
                            Debug.Log("패 루프"+i);
                            GameObject card = playerUI.hand.GetChild(i).gameObject;
                            Debug.Log("카드 : "+ card.GetComponent<N_Card>().cardData.name);
                            if (card.GetComponent<N_Card>().cardData.attackType == CardData.AttackType.Attack)
                            {
                                Debug.Log("공격 카드 소모");
                                matchCardCount++;
                                playerUI.boundDeck.HandToGrave(card.GetComponent<N_Card>().cardData.no);
                                playerUI.ReturnToInstant(card);
                            }
                        }
                        damage *= matchCardCount;
                    }
                }
            damage = CalculateAttackDamage(target, damage, failedToken);
            target.Damaged((int)damage);
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }
    public static void TrickStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        foreach (PlayerBattleUI performerui in BattleUI.instance.playerUI)
        {
            if (!performerui.gameObject.activeSelf)
                continue;
            if (performerui.boundCharacter == performer)
            {
                performerui.DrawCard(1);
                return;
            }
        }
    }
    public static void DoubleAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken, totalToken);
    }
    public static void EvilStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Confusion, 0, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Confusion, 0, extraEffect);
        }
    }
    public static void Bash(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.BashEffect,2, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (performer.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.BashEffect, 2, extraEffect);
        }
    }
    public static void Cleave(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
        }

        //부가효과 추가 필요
        //출혈 부여
    }
    public static void AlphaStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            if (performer.TryGetComponent<Character>(out Character player))
                foreach (PlayerBattleUI playerUI in BattleUI.instance.playerUI)
                {
                    if (!playerUI.gameObject.activeSelf)
                        continue;
                    if (player == playerUI.boundCharacter)
                    {
                        Debug.Log("캐릭 고름");
                        int matchCardCount = 0;
                        for (int i = 0; i < playerUI.hand.childCount; i++)
                        {
                            Debug.Log("패 루프" + i);
                            GameObject card = playerUI.hand.GetChild(i).gameObject;
                            Debug.Log("카드 : " + card.GetComponent<N_Card>().cardData.name);
                            if (card.GetComponent<N_Card>().cardData.name.Contains("일격"))
                            {
                                Debug.Log("일격 카드 소모");
                                matchCardCount++;
                                playerUI.boundDeck.HandToGrave(card.GetComponent<N_Card>().cardData.no);
                                playerUI.ReturnToInstant(card);
                            }
                        }
                        damage *= matchCardCount;
                    }
                }
            damage = CalculateAttackDamage(target, damage, failedToken);
            target.Damaged((int)damage);
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }

    public static void SpinAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        foreach(Unit unit in N_BattleManager.instance.units)
        {
            if(unit.CompareTag(target.tag))
                DefaultPhysicalAttack(performer, unit, damage, extraEffect, failedToken, totalToken);
            
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
            DefaultPhysicalAttack(performer, N_BattleManager.instance.currentUnit, damage, extraEffect, failedToken, totalToken);
    }
    public static void Stroming(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            damage = 0;

        if (performer.TryGetComponent<Character>(out Character player))
        {
            damage *= player.cost;
            player.cost = 0;
        }

        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                damage = CalculateAttackDamage(unit, damage, failedToken);
                unit.Damaged((int)damage);
                unit.GetComponent<UnitAnimationControl>().GetDamage();
            }

        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
        {
            damage = CalculateAttackDamage(N_BattleManager.instance.currentUnit, damage, failedToken);
            N_BattleManager.instance.currentUnit.Damaged((int)damage);
            N_BattleManager.instance.currentUnit.GetComponent<UnitAnimationControl>().GetDamage();
        }

    }

    public static void Brandish(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ShieldSlam(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.Faint, 0, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Faint, 0, extraEffect);
        }
        //부가효과 추가 필요
        //1턴간 기절 부여
    }

    public static void Judgment(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.Faint, 0, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Faint, 0, extraEffect);
        }
        //부가효과 추가 필요
        //1턴간 기절 부여
    }

    public static void SwingAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                DefaultPhysicalAttack(performer, unit, damage, extraEffect, failedToken, totalToken);
            }
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
            DefaultPhysicalAttack(performer, N_BattleManager.instance.currentUnit, damage, extraEffect, failedToken, totalToken);

    }

    
    public static void Attack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

   
    #endregion
    # region MagicAttack

    public static void DefaultMagicAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            damage = CalculateMagicDamage(target, damage, failedToken);
            target.Damaged((int)damage);
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();    //임시
    }

    public static void DragonScream(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken <= 0)
            damage = 0;
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                damage = CalculateMagicDamage(unit, damage, failedToken);
                target.Damaged((int)damage);
            }
        }
    }
    

    public static void Pain(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken, totalToken);
    }

    public static void EnergyBall(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

    public static void HolyFlame(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken, totalToken);
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
        }
        //부가효과 추가 필요
        //2턴간 화상 부여
    }

    public static void FireBall(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void IceBall(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ThunderBolt(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ManaBullet(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ManaBomb(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken <= 0)
            damage = 0;
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                damage = CalculateMagicDamage(unit, damage, failedToken);
                target.Damaged((int)damage);
            }
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
        {
                damage = CalculateMagicDamage(N_BattleManager.instance.currentUnit, damage, failedToken);
                target.Damaged((int)damage);
        }

    }
    public static void ShootingStar(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ManaShower(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken <= 0)
            damage = 0;
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                damage = CalculateMagicDamage(unit, damage, failedToken);
                target.Damaged((int)damage);
            }
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
        {
                damage = CalculateMagicDamage(N_BattleManager.instance.currentUnit, damage, failedToken);
                target.Damaged((int)damage);
        }
    }
    public static void BurningFlame(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker effectTurnChecker) && effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition])
        {
            damage += 3;
            effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition] = false;
        }

        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
        }

    }
    public static void FlameofDragon(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker effectTurnChecker) && effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition])
        {
            damage += 3;
            effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition] = false;
        }

        if (totalToken - failedToken <= 0)
            damage = 0;

        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                damage = CalculateMagicDamage(unit, damage, failedToken);
                target.Damaged((int)damage);
                if(UnityEngine.Random.Range(0,100) < 10)
                    if (unit.TryGetComponent(out EffectTurnChecker turnChecker))
                    {
                        turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
                    }
                    else
                    {
                        turnChecker = unit.gameObject.AddComponent<EffectTurnChecker>();
                        if (target.TryGetComponent(out Character character))
                            turnChecker.boundCharacter = character;
                        else
                            turnChecker.boundMonster = unit.GetComponent<Monster>(); ;
                        turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
                    }
            }
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
        {
            damage = CalculateMagicDamage(N_BattleManager.instance.currentUnit, damage, failedToken);
            target.Damaged((int)damage);
            if (UnityEngine.Random.Range(0, 100) < 10)
                if (N_BattleManager.instance.currentUnit.TryGetComponent(out EffectTurnChecker turnChecker))
                {
                    turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
                }
                else
                {
                    turnChecker = N_BattleManager.instance.currentUnit.gameObject.AddComponent<EffectTurnChecker>();
                    if (target.TryGetComponent(out Character character))
                        turnChecker.boundCharacter = character;
                    else
                        turnChecker.boundMonster = N_BattleManager.instance.currentUnit.GetComponent<Monster>(); ;
                    turnChecker.StartEffect(EffectType.Burn, 0.07f, extraEffect);
                }
        }
    }
    public static void Flamethrower(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker effectTurnChecker) && effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition])
        {
            damage += 3;
            effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition] = false;
        }
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void Pillaroflight(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                DefaultMagicAttack(performer, unit, damage, extraEffect, failedToken, totalToken);
            }
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
        {
            DefaultMagicAttack(performer, N_BattleManager.instance.currentUnit, damage, extraEffect, failedToken, totalToken);
        }
    }
    public static void HolyArrow(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultMagicAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

    #endregion
    #region Defence

    public static void Blessofprotect(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        }
    }
    public static void BulkUp(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        }
    }

    public static void Defcon(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        }

        foreach (PlayerBattleUI targetui in BattleUI.instance.playerUI)
        {
            if (!targetui.gameObject.activeSelf)
                continue;
            if (targetui.boundCharacter == target)
            {
                targetui.DrawCard(1);
                return;
            }
        }

    }
    public static void FlameArmor(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker effectTurnChecker) && effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition])
        {
            value += 3;
            effectTurnChecker.isBuffRun[8 + (int)EffectType.Ignition] = false;
        }

        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
            turnChecker.StartEffect(EffectType.MagicMirror, value, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
            turnChecker.StartEffect(EffectType.MagicMirror, value, extraEffect);
        }
    }
    public static void Guard(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void MagicShield(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
        }
    }
    public static void Shield(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
        }
    }
    public static void Shieldoflight(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
        {
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
        }
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();
            turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
        }
        if (totalToken - failedToken == 0)
            return;
        foreach (PlayerBattleUI targetui in BattleUI.instance.playerUI)
        {
            if (!targetui.gameObject.activeSelf)
                continue;
            if (targetui.boundCharacter == target)
            {
                targetui.DrawCard(1);
                return;
            }
        }

    }
    #endregion
    #region Buff
    // Buff
    public static void DefaultBuff(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Parry(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Parry, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
            turnChecker.StartEffect(EffectType.Parry, value, extraEffect);
        }
    }
    public static void Agility(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Speed_Increase, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();

            turnChecker.StartEffect(EffectType.Speed_Increase, value, extraEffect);
        }
    }
    public static void BlessofIntelligence(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();

            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        }
    }
    public static void BlessofStrength(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
            if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            else
            {
                turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
                if (target.TryGetComponent(out Character character))
                    turnChecker.boundCharacter = character;
                else
                    turnChecker.boundMonster = target.GetComponent<Monster>();

                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            }
    }
    public static void Heal(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if(totalToken - failedToken <= 0)
        {
            target.Hp += 0;
            return;
        }
        else
        {
            target.Hp += value;
        }
    }
    public static void Focus(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if(target.TryGetComponent(out Character character))
        {
            if (character.TryGetComponent(out EffectTurnChecker turnChecker))
            {
                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            }
            else
            {
                turnChecker = character.gameObject.AddComponent<EffectTurnChecker>();
                turnChecker.boundCharacter = character;
                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            }
            
        }
        else
        {
            if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            {
                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            }
            else
            {
                turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
                turnChecker.boundMonster = target.GetComponent<Monster>(); ;
                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            }
        }
    }
    public static void DoubleCharm(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void ManaCirculation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken <= 0)
            return;

        if (target.TryGetComponent(out Character character))
        {
            if (character.cost + 1 >= character.maxCost)
                character.cost = character.maxCost;
            else
                character.cost += 1;
        }
    }
    public static void ManaAmplification(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();

            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        }
    }
    public static void Scheme(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            return;
        foreach(PlayerBattleUI targetui in BattleUI.instance.playerUI)
        {
            if (!targetui.gameObject.activeSelf)
                continue;
            if (targetui.boundCharacter == target)
            {
                targetui.DrawCard(2);
                return;
            }
        }
    }
    public static void Meditation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (target.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();

            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        }
    }
    public static void Ignition(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (performer.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();

            turnChecker.StartEffect(EffectType.Ignition, value, extraEffect);
        }
    }
    public static void Pray(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
            if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
                turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
            else
            {
                turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
                if (target.TryGetComponent(out Character character))
                    turnChecker.boundCharacter = character;
                else
                    turnChecker.boundMonster = target.GetComponent<Monster>();

                turnChecker.StartEffect(EffectType.Intelliegence_Increase, value, extraEffect);
            }
    }
    public static void Blessoflight(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
            if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
                turnChecker.StartEffect(EffectType.Regeneration, value, extraEffect);
            else
            {
                turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
                if (target.TryGetComponent(out Character character))
                    turnChecker.boundCharacter = character;
                else
                    turnChecker.boundMonster = target.GetComponent<Monster>();

                turnChecker.StartEffect(EffectType.Regeneration, value, extraEffect);
            }

        if (totalToken - failedToken == 0)
            return;
        foreach (PlayerBattleUI targetui in BattleUI.instance.playerUI)
        {
            if (!targetui.gameObject.activeSelf)
                continue;
            if (targetui.boundCharacter == target)
            {
                targetui.DrawCard(2);
                return;
            }
        }
    }
    public static void Salvation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void LightAround(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
        else
        {
            turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
            if (target.TryGetComponent(out Character character))
                turnChecker.boundCharacter = character;
            else
                turnChecker.boundMonster = target.GetComponent<Monster>();

            turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
        }
    }
    public static void Purification(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Contemplation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        target.Hp += value;
            if (performer.TryGetComponent(out EffectTurnChecker turnChecker))
            {
                turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
                turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
            }
            else
            {
                turnChecker = target.gameObject.AddComponent<EffectTurnChecker>();
                if (target.TryGetComponent(out Character character))
                    turnChecker.boundCharacter = character;
                else
                    turnChecker.boundMonster = target.GetComponent<Monster>();
                    
                turnChecker.StartEffect(EffectType.MagicGuard, value, extraEffect);
                turnChecker.StartEffect(EffectType.AttackGuard, value, extraEffect);
            }
    }

    public static void CryofVictory(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag(target.tag))
            {
                if (unit.TryGetComponent(out EffectTurnChecker turnChecker))
                    turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
                else
                {
                    turnChecker = unit.gameObject.AddComponent<EffectTurnChecker>();
                    if (unit.TryGetComponent(out Character character))
                        turnChecker.boundCharacter = character;
                    else
                        turnChecker.boundMonster = target.GetComponent<Monster>();

                    turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
                }
            }
        }
        if (N_BattleManager.instance.currentUnit.CompareTag(target.tag))
        {
            if (N_BattleManager.instance.currentUnit.TryGetComponent(out EffectTurnChecker turnChecker))
                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            else
            {
                turnChecker = N_BattleManager.instance.currentUnit.gameObject.AddComponent<EffectTurnChecker>();
                if (N_BattleManager.instance.currentUnit.TryGetComponent(out Character character))
                    turnChecker.boundCharacter = character;
                else
                    turnChecker.boundMonster = target.GetComponent<Monster>();

                turnChecker.StartEffect(EffectType.Strength_Increase, value, extraEffect);
            }
        }
    }
    #endregion
    #region DeBuff
    #endregion

    #region Coroutine


    #endregion
}
