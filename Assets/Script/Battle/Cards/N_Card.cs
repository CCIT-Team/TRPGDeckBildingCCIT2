using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class N_Card : MonoBehaviour   //카드 정보와 효과 함수만 가질 것
{
    public delegate void CardAction();
    public CardAction cardAction;

    public PlayerBattleUI playerUI;


    public int cardID;
    public string cardName;
    public int cost;
    public CARDRARITY rarity;
    public int tokenAmount;
    public bool[] tokens;

    public CardData_ cardData;

    public GameObject cardTarget;



    void OnEnable()
    {
        int indexNumber = 0;
        int tester = 0;
        while(playerUI.boundCharacter == null || cardID == 0) { if (tester++ >= 30) break; }
        switch (playerUI.boundCharacter.GetComponent<Character_type>().major)
        {
            case PlayerType.Major.Fighter:
                indexNumber = cardID - 50000000;
                break;
            case PlayerType.Major.Wizard:
                indexNumber = cardID - 60000000;
                break;
            case PlayerType.Major.Cleric:
                indexNumber = cardID - 70000000;
                break;
        }
        Debug.Log("indexNumber = " + indexNumber + "\n AddNumber = " + N_BattleManager.instance.majorCardStartNo[(int)playerUI.boundCharacter.GetComponent<Character_type>().major]);
        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.majorCardStartNo[(int)playerUI.boundCharacter.GetComponent<Character_type>().major]];

        cardAction = null;
        cardAction += () => UseCost(playerUI.boundCharacter);
        cardAction += () => CardEffect();
        cardAction += () => RemoveInHand(playerUI.GetComponent<Deck>());
    }

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
        print(playerUI.boundCharacter.name+"이(가) "+cardTarget.name+"에게" + cardName + "을(를) 사용");
        var skill = CardSkills.SearchSkill(cardName);
        skill.Invoke(null, new object[] { cardTarget.GetComponent<Unit>(), 10 });
        this.gameObject.SetActive(false);
    }

    public void CardSelect()
    {
        
    }





}
