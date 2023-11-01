using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class N_Card : MonoBehaviour   //카드 정보와 효과 함수만 가질 것
{
    public delegate void CardAction();
    public CardAction cardAction;

    public Character cardOwner;


    public int cardID;
    public string cardName;
    public int cost;
    public CARDRARITY rarity;
    public int tokenAmount;
    public bool[] tokens;

    CardData cardData;

    public GameObject cardTarget;



    void OnEnable()
    {
        cardOwner = transform.parent.parent.GetComponentInParent<N_DrawSystem>().bindedCharacter;
        cardData = CardDataBase.instance.cards[cardID];
        //image.sprite = Resources.Load<Sprite>(cardData.cardImage);
        //text.text = cardData.cardText;

        cardAction = null;
        cardAction += () => UseCost(cardOwner);
        cardAction += () => CardEffect();
        cardAction += () => RemoveInHand(cardOwner.GetComponent<Deck>());
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
        print(cardOwner.name+"가 "+cardTarget.name+"에게" + cardName + "을 사용");
        CardSkills.PhysicalAttack.SingleAttack(cardTarget.GetComponent<Unit>(), 10);
        this.gameObject.SetActive(false);
    }

    public void CardSelect()
    {
        
    }





}
