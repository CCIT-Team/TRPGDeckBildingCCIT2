using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour   //카드 정보와 효과 함수만 가질 것
{
    public delegate void CardAction();
    public CardAction cardEffect;

    public int cardID;
    public string cardName;
    public int cost;
    public CARDRARITY rarity;
    public Sprite cardImage;
    public int tokenAmount;
    public bool[] tokens;

    CardData cardData;

    public GameObject cardTarget;

    [SerializeField]
    Image image;
    [SerializeField]
    Text text;

    void OnEnable()
    {
        cardData = CardDataBase.instance.cards[cardID];

        image.sprite = Resources.Load<Sprite>(cardData.cardImage);
        text.text = cardData.cardText;

        cardEffect = null;
        cardEffect += CardEffect;
        switch(cardData.effect1)
        {
            case CARDEFFECT.SingleAttack:
                //cardEffect += CardAttack;
                break;
            case CARDEFFECT.Buff:
                break;
            case CARDEFFECT.Draw:
                break;
        }
        
    }

    public void CardEffect()
    {
        print(cardTarget.name+"에게" + this.gameObject.name + "실행됨");
        cardTarget.GetComponent<Unit>().Damaged(10);
        gameObject.SetActive(false);
    }

    public void CardSelect()
    {
        BattleManager.instance.CardSelect(this);
    }


    void UseCost(Character character)
    {
        character.cost -= cost;
    }
}
