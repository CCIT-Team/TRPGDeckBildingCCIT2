using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class N_Card : MonoBehaviour   //카드 정보와 효과 함수만 가질 것
{
    public delegate void CardAction();
    public CardAction cardAction;

    public PlayerBattleUI playerUI;

    public CardData cardData;
    /*
     * no
     * name
     * variable
     * type
     * description
     * defaultx
     * effectturn
     * cost
     * token
     */

    public bool[] tokens;

    

    public GameObject cardTarget;


    public void UseCard()
    {
        cardAction();
    }

    void UseCost(Character character)
    {
        character.cost -= cardData.useCost;
    }

    void RemoveInHand(Deck deck)
    {
        deck.hand.Remove(cardData.no);
        deck.grave.Add(cardData.no);
    }

    public void CardEffect()
    {
        print(playerUI.boundCharacter.name+"이(가) "+cardTarget.name+"에게" + cardData.name + "을(를) 사용");
        var skill = CardSkills.SearchSkill(cardData.variableName);
        skill.Invoke(null, new object[] { cardTarget.GetComponent<Unit>(), 10 });
        this.gameObject.SetActive(false);
    }

    public void CardSelect()
    {
        
    }

    public void GetCardData(int no)
    {
        int loopCounter = 0;
        while(playerUI.boundCharacter == null) { if (loopCounter++ >= 100) break; }

        int indexNumber = int.Parse(no.ToString().Substring(2));
        Debug.Log(indexNumber);
        int weaponType = int.Parse(no.ToString().Substring(0, 2));
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
        cardAction = null;
        cardAction += () => UseCost(playerUI.boundCharacter);
        cardAction += () => RemoveInHand(playerUI.GetComponent<Deck>());
        cardAction += () => playerUI.ReturnToInstant(gameObject);
        cardAction += () => N_BattleManager.instance.IsAction = true;
        //애니메이션 필요
        cardAction += () => CardEffect();
    }
}
