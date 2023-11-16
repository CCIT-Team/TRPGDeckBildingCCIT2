using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCard : MonoBehaviour
{
    public delegate void CardAction();
    public CardAction cardAction;

    public int cardID;
    public bool[] tokens;

    public CardData cardData;

    public GameObject cardTarget;

    public void UseCard()
    {
        cardAction();
    }



    void RemoveInHand(Deck deck)
    {
        deck.hand.Remove(cardID);
        deck.grave.Add(cardID);
    }

    public void CardEffect()
    {
        var skill = CardSkills.SearchSkill(cardData.variableName);
        Debug.Log(gameObject.name + "가 "+ cardTarget +"에게" + cardData.name + "공격");
        skill.Invoke(null, new object[] { GetComponent<Monster>(), cardTarget.GetComponent<Unit>(), cardData.defaultXvalue, cardData.effectUseTurn, cardData.token });//사용자, 사용 대상, 값, 추가효과 값, 토큰 수
    }

    public void SetCardAction()
    {
        SetCardData(cardID);
        cardAction = null;
        //cardAction += () => RemoveInHand();
        cardAction += () => N_BattleManager.instance.IsAction = true;
        //애니메이션 필요
        cardAction += () => CardEffect();
        cardAction += () => GetComponent<Monster>().IsMyturn = false;
    }

    public void SetCardData(int id)
    {
        int indexNumber = int.Parse(id.ToString().Substring(2));
        int weaponType = int.Parse(id.ToString().Substring(0, 2));
        switch (weaponType)
        {
            case 50:    //워리어
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[0]];
                break;
            case 51:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[1]];
                break;
            case 52:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[2]];
                break;
            case 53:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[3]];
                break;
            case 54:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[4]];
                break;
            case 55:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[5]];
                break;
            case 56:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[6]];
                break;
            case 57:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[7]];
                break;
            case 58:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[8]];
                break;
            case 59:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[9]];
                break;
            case 60:    //메지션
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[10]];
                break;
            case 70:    //클레릭
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[11]];
                break;
        }
    }
}
