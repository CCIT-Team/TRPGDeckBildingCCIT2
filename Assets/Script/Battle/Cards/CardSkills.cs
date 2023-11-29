using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CardSkills     //�����, ��� ���, ��, �߰�ȿ�� ��, ��ū ��
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
        target.GetComponent<UnitAnimationControl>().GetDamage();    //�ӽ�
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
        //�ΰ�ȿ�� �߰� �ʿ�
        //2�� ���
    }
    public static void BitingStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //�ΰ�ȿ�� �߰� �ʿ�
        //������ ������+5
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
                        Debug.Log("ĳ�� ��");
                        int matchCardCount = 0;
                        for (int i = 0; i <= playerUI.handUI.transform.childCount; i++)
                        {
                            Debug.Log("�� ����"+i);
                            GameObject card = playerUI.handUI.transform.GetChild(i).gameObject;
                            Debug.Log("ī�� : "+ card.GetComponent<N_Card>().cardData.name);
                            if (card.GetComponent<N_Card>().cardData.type == CardData.CardType.SingleAttack ||
                                card.GetComponent<N_Card>().cardData.type == CardData.CardType.MultiAttack ||
                                card.GetComponent<N_Card>().cardData.type == CardData.CardType.AllAttack )
                            {
                                Debug.Log("���� ī�� �Ҹ�");
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
        //�ΰ�ȿ�� �߰� �ʿ�
        //�п� ��� ����ī�� �Ҹ�, �Ҹ�� ���ط� ����
    }
    public static void TrickStrike(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //�ΰ�ȿ�� �߰� �ʿ�
        //1�� ��ο�
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
        //�ΰ�ȿ�� �߰� �ʿ�
        //2�� ȥ�� �ο�
    }
    public static void Bash(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //�ΰ�ȿ�� �߰� �ʿ�
        //ī�� ���� �������� ���ط� 2 ��������
    }
    public static void Cleave(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //�ΰ�ȿ�� �߰� �ʿ�
        //���� �ο�
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
                        Debug.Log("ĳ�� ��");
                        int matchCardCount = 0;
                        for (int i = 0; i < playerUI.handUI.transform.childCount; i++)
                        {
                            Debug.Log("�� ����" + i);
                            GameObject card = playerUI.handUI.transform.GetChild(i).gameObject;
                            Debug.Log("ī�� : " + card.GetComponent<N_Card>().cardData.name);
                            if (card.GetComponent<N_Card>().cardData.name.Contains("�ϰ�"))
                            {
                                Debug.Log("�ϰ� ī�� �Ҹ�");
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
        //�ΰ�ȿ�� �߰� �ʿ�
        //�п� �ϰ� ī�� ���� �Ҹ�, �Ҹ�� ���ط� ����
    }

    public static void SpinAttack(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //���� ����
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
        //�ΰ�ȿ�� �߰� �ʿ�
        //1�ϰ� ���� �ο�
    }

    public static void Judgment(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //�ΰ�ȿ�� �߰� �ʿ�
        //1�ϰ� ���� �ο�
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
        //�ΰ�ȿ�� �߰� �ʿ�
        //2�ϰ� ȭ�� �ο�
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
        //�ΰ�ȿ�� �߰� �ʿ�
        // 2�ϰ� ȭ�� �ο�

    }
    public static void FlameofDragon(Unit performer, Unit target, float damage, int extraEffect, int failedToken, int totalToken)
    {
        DefaultPhysicalAttack(performer, target, damage, extraEffect, failedToken,totalToken);
        //�ΰ�ȿ�� �߰� �ʿ�
        //10% Ȯ���� 2�ϰ� ȭ�� �ο�
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
