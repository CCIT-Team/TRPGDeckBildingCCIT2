using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class N_Card : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler   //카드 정보와 효과 함수만 가질 것
{
    public delegate void CardAction();
    public CardAction shotEffect;

    public Character cardOwner;


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
        cardOwner = transform.parent.parent.GetComponentInParent<N_DrawSystem>().bindedCharacter;
        cardData = CardDataBase.instance.cards[cardID];
        image.sprite = Resources.Load<Sprite>(cardData.cardImage);
        text.text = cardData.cardText;

        shotEffect = null;
        shotEffect += () => UseCost(cardOwner);
        shotEffect += () => CardEffect();
    }

    public void ShotEffect()
    {
        shotEffect();
    }

    public void CardEffect()
    {
        print(cardTarget.name+"에게" + this.gameObject.name + "실행됨");
        CardSkills.PhysicalAttack.SingleAttack(cardTarget.GetComponent<Unit>(), 10);
        this.gameObject.SetActive(false);
    }

    public void CardSelect()
    {
        
    }


    void UseCost(Character character)
    {
        character.cost -= cost;
    }

    IEnumerator SelectCard()
    {

        yield return new WaitUntil(() => cardTarget != null);
    }

    private void OnMouseDown()
    {
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OPD");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OBD");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OED");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("ODg");
    }
}
