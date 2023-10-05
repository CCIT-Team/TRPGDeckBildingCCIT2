using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour   //
{
    public delegate void CardAction();
    public CardAction cardEffect;

    public int cardID;
    public string cardName;
    public int cost;
    public CARDRARITY rarity;
    public Sprite cardImage;
    public string cardText;
    public int tokenAmount;
    public bool[] tokens;

    CardData cardData;

    public GameObject cardTarget;

    public Image image;
    public Text text;


    public void CardEffect()
    {
        print(cardTarget.name+"¿¡°Ô" + this.gameObject.name + "½ÇÇàµÊ");
    }

    void UseCost(Character character)
    {
        character.cost -= cost;
    }

    void OnEnable()
    {
        cardData = CardDataBase.instance.cards[cardID];
        image.sprite = cardImage;
        text.text = cardText;
        cardEffect += CardEffect;
    }
}
