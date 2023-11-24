using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Image backGroundImage;
    public Image image;
    public Text cardName;
    public Image nameBoxImage;
    public Image typeImage;
    public Text cost;
    public Text description;

    [Header("리소스")]
    public Sprite[] nameBoxSprits;
    public Sprite[] typeSprits;
    public Sprite[] backGroundSprites;

    public N_Card bindCard;

    public Vector3 defaultPosition = new Vector3(0,0,0);

    public LayerMask layerMask;
    GameObject target;

    string damageColor = "red";

    bool isSelected = false;

    private void Awake()
    {
        if (bindCard == null)
            bindCard = GetComponent<N_Card>();
    }

    public void DisplayOnUI()
    {
        //이름상자,타입
        switch(bindCard.cardData.type)
        {
            case CardData.CardType.SingleAttack:
            case CardData.CardType.MultiAttack:
            case CardData.CardType.AllAttack:
                nameBoxImage.sprite = nameBoxSprits[0];
                typeImage.sprite = typeSprits[0];
                backGroundImage.sprite = backGroundSprites[0];
                layerMask = 1 << LayerMask.NameToLayer("Monster");
                break;
            case CardData.CardType.SingleDefence:
            case CardData.CardType.MultiDefence:
            case CardData.CardType.AllDenfence:
                nameBoxImage.sprite = nameBoxSprits[1];
                typeImage.sprite = typeSprits[1];
                backGroundImage.sprite = backGroundSprites[1];
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.CardType.SingleEndow:
            case CardData.CardType.MultiEndow:
            case CardData.CardType.AllEndow:
                nameBoxImage.sprite = nameBoxSprits[2];
                typeImage.sprite = typeSprits[2];
                backGroundImage.sprite = backGroundSprites[2];
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.CardType.SingleIncrease:
            case CardData.CardType.MultiIncrease:
            case CardData.CardType.AllIncrease:
                nameBoxImage.sprite = nameBoxSprits[3];
                typeImage.sprite = typeSprits[3];
                backGroundImage.sprite = backGroundSprites[3];
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.CardType.CardDraw:
                nameBoxImage.sprite = nameBoxSprits[3];
                typeImage.sprite = typeSprits[3];
                backGroundImage.sprite = backGroundSprites[4];
                layerMask = 0;
                break;
        }
        
        //이름
        cardName.text = bindCard.cardData.name;

        //코스트
        cost.text = bindCard.cardData.useCost.ToString();

        //설명
        if (bindCard.cardData.description.Contains("회복"))
            damageColor = "green";
        else if (bindCard.cardData.description.Contains("마법")&& bindCard.cardData.description.Contains("물리"))
            damageColor = "magenta";
        else if (bindCard.cardData.description.Contains("마법"))
            damageColor = "blue";
        else
            damageColor = "red";
        if (!bindCard.cardData.description.Contains("x"))
            description.text = bindCard.cardData.description;
        else
            description.text = bindCard.cardData.description.Substring(0, bindCard.cardData.description.IndexOf("x"))
                         + "<b><color="+ damageColor + ">"
                         + (bindCard.CalculateCardValue()).ToString()
                         + "</color></b>"
                         + bindCard.cardData.description.Substring(bindCard.cardData.description.IndexOf("x") + 1);
    }
    List<GameObject> tokenPreview = new List<GameObject>();
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(!isSelected)
            for (int i = 0; i < bindCard.cardData.token; i++)
            {
                Token token = Instantiate(BattleUI.instance.tokenPrefab, BattleUI.instance.tokenPosition.transform);
                token.CheckToken(bindCard.MainStaus, true);
                token.transform.localPosition = new Vector2((-bindCard.cardData.token / 2 + i + (bindCard.cardData.token + 1) % 2 / 2f) * 160, 0);
                tokenPreview.Add(token.gameObject);
            }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < tokenPreview.Count; i++)
        {
            Destroy(tokenPreview[i]);
        }
        tokenPreview.Clear();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
            return;
        isSelected = true;
        for (int i = 0; i < tokenPreview.Count; i++)
        {
            tokenPreview[i].SetActive(false);
        }
        defaultPosition = transform.position;
        transform.position = Input.mousePosition;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (layerMask == 0)
        {
            target = bindCard.playerUI.boundCharacter.gameObject;
            bindCard.cardTarget = target;
            bindCard.UseCard();
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 80, layerMask))
            {
                target = hit.transform.gameObject;
                bindCard.cardTarget = target;
                bindCard.UseCard();
            }
            else
            {
                target = null;
                transform.position = defaultPosition;
            }
        }
        isSelected = false;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isSelected)
            return;
        transform.position = Input.mousePosition;
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 80,layerMask))
        {
            target = hit.transform.gameObject;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void TransferUI()
    {
        transform.position = new Vector2(Camera.main.pixelWidth*2,0);
    }
}