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
    public static void DefaultPhysicalAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
            target.Damaged(damage*(1-(0.1f*failedToken)));
        target.GetComponent<UnitAnimationControl>().GetDamage();    //임시
    }

    public static void Slash(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
            target.Damaged(damage * (1 - (0.1f * failedToken)));
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }

    public static void Smite(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //2턴 취약
    }
    public static void BitingStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //레벨당 데미지+5
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
                    if (player == playerUI.boundCharacter)
                    {
                        Debug.Log("캐릭 고름");
                        int matchCardCount = 0;
                        for (int i = 0; i <= playerUI.handUI.transform.childCount; i++)
                        {
                            Debug.Log("패 루프"+i);
                            GameObject card = playerUI.handUI.transform.GetChild(i).gameObject;
                            Debug.Log("카드 : "+ card.GetComponent<N_Card>().cardData.name);
                            if (card.GetComponent<N_Card>().cardData.type == CardData.CardType.SingleAttack ||
                                card.GetComponent<N_Card>().cardData.type == CardData.CardType.MultiAttack ||
                                card.GetComponent<N_Card>().cardData.type == CardData.CardType.AllAttack )
                            {
                                Debug.Log("공격 카드 소모");
                                matchCardCount++;
                                playerUI.boundDeck.hand.Remove(card.GetComponent<N_Card>().cardData.no);
                                playerUI.boundDeck.grave.Add(card.GetComponent<N_Card>().cardData.no);
                                playerUI.ReturnToInstant(card);
                            }
                        }
                        damage *= matchCardCount;
                    }
                }
            target.Damaged(damage * (1 - (0.1f * failedToken)));
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
        //부가효과 추가 필요
        //패에 모든 공격카드 소모, 소모당 피해량 증가
    }
    public static void TrickStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //1장 드로우
    }
    public static void DoubleAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            target.Damaged(damage * (1 - (0.1f * failedToken)));
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }
    public static void EvilStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //2턴 혼란 부여
    }
    public static void Bash(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //카드 사용시 전투동안 피해량 2 영구증가
    }
    public static void Cleave(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
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
                    if (player == playerUI.boundCharacter)
                    {
                        Debug.Log("캐릭 고름");
                        int matchCardCount = 0;
                        for (int i = 0; i < playerUI.handUI.transform.childCount; i++)
                        {
                            Debug.Log("패 루프" + i);
                            GameObject card = playerUI.handUI.transform.GetChild(i).gameObject;
                            Debug.Log("카드 : " + card.GetComponent<N_Card>().cardData.name);
                            if (card.GetComponent<N_Card>().cardData.name.Contains("일격"))
                            {
                                Debug.Log("일격 카드 소모");
                                matchCardCount++;
                                playerUI.boundDeck.hand.Remove(card.GetComponent<N_Card>().cardData.no);
                                playerUI.boundDeck.grave.Add(card.GetComponent<N_Card>().cardData.no);
                                playerUI.ReturnToInstant(card);
                            }
                        }
                        damage *= matchCardCount;
                    }
                }
            target.Damaged(damage * (1 - (0.1f * failedToken)));
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
        //부가효과 추가 필요
        //패에 일격 카드 전부 소모, 소모당 피해량 증가
    }

    public static void SpinAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //광역 공격
    }
    public static void Stroming(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            target.Damaged(0);
        else
        {
            float damageAddCost = damage;
            if (performer.TryGetComponent<Character>(out Character player))
            {
                damage *= player.cost;
                player.cost = 0;
                target.Damaged(damage * (1 - (0.1f * failedToken)));
            }
            else
            {
                target.Damaged(damage * (1 - (0.1f * failedToken)));
            }
        }
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }

    public static void Brandish(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ShieldSlam(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //1턴간 기절 부여
    }

    public static void Judgment(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //1턴간 기절 부여
    }

    public static void SwingAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

    public static void Pain(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
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
            target.Damaged(damage * (1 - (0.1f * failedToken)));
        target.GetComponent<UnitAnimationControl>().GetDamage();
    }
    
    public static void EnergyBall(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

    public static void HolyFlame(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken, totalToken);
        //부가효과 추가 필요
        //2턴간 화상 부여
    }

    public static void FireBall(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void IceBall(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ThunderBolt(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ManaBullet(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ManaBomb(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ShootingStar(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void ManaShower(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void BurningFlame(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        // 2턴간 화상 부여

    }
    public static void FlameofDragon(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //부가효과 추가 필요
        //10% 확률로 2턴간 화상 부여
    }
    public static void Flamethrower(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void Pillaroflight(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void HolyArrow(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

    #endregion
    #region Defence

    public static void Blessofprotect(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        
    }
    public static void BulkUp(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }

    public static void Defcon(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void FlameArmor(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void Guard(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void MagicShield(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void Shield(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    public static void Shieldoflight(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
    }
    #endregion
    #region Buff
    // Buff
    public static void DefaultBuff(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Parry(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Agility(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void BlessofIntelligence(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void BlessofStrength(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Heal(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Focus(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if(target.TryGetComponent<Character>(out Character character))
        {
            if (character.TryGetComponent<EffectTurnChecker>(out EffectTurnChecker turnChecker))
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
    }
    public static void DoubleCharm(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void ManaCirculation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void ManaAmplification(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Scheme(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {
        if (totalToken - failedToken == 0)
            return;
        foreach(PlayerBattleUI targetui in BattleUI.instance.playerUI)
        {
            if(targetui.boundCharacter == target)
            {
                targetui.DrawCard(2);
                return;
            }
        }
    }
    public static void Meditation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Ignition(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Pray(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Blessoflight(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Salvation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void LightAround(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Purification(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    public static void Contemplation(Unit performer, Unit target, float value, int extraEffect, int failedToken, int totalToken)
    {

    }
    #endregion
    #region DeBuff
    #endregion

    #region Coroutine
    

    #endregion
}
