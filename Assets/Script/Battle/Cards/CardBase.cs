using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
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



    void OnEnable()
    {
        int indexNumber = 0;
        int tester = 0;
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
        var skill = CardSkills.SearchSkill(cardName);
        skill.Invoke(null, new object[] { cardTarget.GetComponent<Unit>(), 10 /*데미지*/ });
        this.gameObject.SetActive(false);
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
}
