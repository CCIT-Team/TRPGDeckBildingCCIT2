using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardID;
    public string cardName;
    public string cardInfo;
    public int cost;
    public int tokenCount;
    public bool[] tokens;
    public CARDTYPE cardType;
    public Sprite sprite;

    public Image CardImage;
    public Text CardText;
    

    [SerializeField]
    float damage = 1;
    public float Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

    public enum CARDTYPE
    {
        None = -1,
        Attack,
        Support,
        Draw
    }

    void CardEffect()
    {

    }

    void UseCost(Character character)
    {
        character.cost -= cost;
    }

    void OnEnable()
    {
        CardImage.sprite = sprite;
        CardText.text = cardInfo;
    }
}
