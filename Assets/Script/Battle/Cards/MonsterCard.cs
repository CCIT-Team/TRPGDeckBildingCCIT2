using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCard : MonoBehaviour
{
    public delegate void CardAction();
    public CardAction cardAction;

    public int cardID;
    public string cardName;
    public int cost;
    public int tokenAmount;
    public bool[] tokens;

    public CardData_ cardData;

    public GameObject cardTarget;

    public void UseCard()
    {
        cardAction();
    }

    void UseCost(Character character)
    {
        character.cost -= cost;
    }

    void RemoveInHand(Deck deck)
    {
        deck.hand.Remove(cardID);
        deck.grave.Add(cardID);
    }

    public void CardEffect()
    {
        var skill = CardSkills.SearchSkill(cardName);
        skill.Invoke(null, new object[] { cardTarget.GetComponent<Unit>(), 10 /*데미지*/ });
    }

    public void SetCardAction()
    {
        cardAction = null;
        //cardAction += () => UseCost();
        //cardAction += () => RemoveInHand();
        cardAction += () => N_BattleManager.instance.IsAction = true;
        //애니메이션 필요
        cardAction += () => CardEffect();
    }

    public void SetCardData(int id)
    {
        int i = int.Parse(id.ToString().Substring(0, 2));
        switch (i)
        {
            case 50:    //워리어
                break;
            case 51:
                break;
            case 52:
                break;
            case 53:
                break;
            case 54:
                break;
            case 55:
                break;
            case 56:
                break;
            case 57:
                break;
            case 58:
                break;
            case 59:
                break;
            case 60:    //메지션
                break;
            case 70:    //클레릭
                break;
        }
            cardData = DataBase.instance.cardData[i];
    }
}
