using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Image backGroundImage;
    public RawImage image;
    public Text cardName;
    public Image nameBoxImage;
    public Image typeImage;
    public Text cost;
    public Text description;
    public GameObject backSide;

    [Header("���ҽ�")]
    public Sprite[] nameBoxSprits;
    public Sprite[] typeSprits;
    public Sprite[] backGroundSprites;

    public N_Card bindCard;

    public Vector3 defaultPosition = new Vector3(0,0,0);
    public Transform defaultParent;
    public int childeIndex = 0;
    public int listIndex = 0;
    Vector2 defaultSize = new Vector2(165, 247.5f);
    Vector3 positionDistance = new Vector3();

    public LayerMask layerMask;
    List<GameObject> canUseList = new();
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
        defaultSize = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);

        //�̸�����,Ÿ��
        switch (bindCard.cardData.attackType)
        {
            case CardData.AttackType.Attack:
                nameBoxImage.sprite = nameBoxSprits[0];
                typeImage.sprite = typeSprits[0];
                backGroundImage.sprite = backGroundSprites[0];
                layerMask = 1 << LayerMask.NameToLayer("Monster");
                break;
            case CardData.AttackType.Defence:
                nameBoxImage.sprite = nameBoxSprits[1];
                typeImage.sprite = typeSprits[1];
                backGroundImage.sprite = backGroundSprites[1];
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.AttackType.Endow:
                nameBoxImage.sprite = nameBoxSprits[2];
                typeImage.sprite = typeSprits[2];
                backGroundImage.sprite = backGroundSprites[2];
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.AttackType.Increase:
                nameBoxImage.sprite = nameBoxSprits[3];
                typeImage.sprite = typeSprits[3];
                backGroundImage.sprite = backGroundSprites[3];
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.AttackType.CardDraw:
                nameBoxImage.sprite = nameBoxSprits[3];
                typeImage.sprite = typeSprits[3];
                backGroundImage.sprite = backGroundSprites[4];
                layerMask = 0;
                break;
        }
        
        //�̸�
        cardName.text = bindCard.cardData.name;

        //�ڽ�Ʈ
        if (bindCard.cardData.useCost == -1)
            cost.text = "All";
        else
            cost.text = bindCard.cardData.useCost.ToString();

        //�̹���
        image.texture = (Texture)Resources.Load("UI/Cards/" + bindCard.cardData.variableName);
        if(bindCard.cardData.variableName == "Defcon")
            image.texture = (Texture)Resources.Load("UI/Cards/Defcon_fighter");




        //����
        if (bindCard.cardData.description.Contains("ȸ��"))
            damageColor = "green";
        else if (bindCard.cardData.description.Contains("����")&& bindCard.cardData.description.Contains("����"))
            damageColor = "magenta";
        else if (bindCard.cardData.description.Contains("����"))
            damageColor = "#00AFFF";
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
        {
            for (int i = 0; i < bindCard.cardData.token; i++)
            {
                Token token = Instantiate(BattleUI.instance.tokenPrefab, BattleUI.instance.tokenPosition.transform);
                token.SetToken(bindCard.MainStaus);
                token.transform.localPosition = new Vector2((-bindCard.cardData.token / 2 + i + (bindCard.cardData.token + 1) % 2 / 2f) * 160, 0);
                tokenPreview.Add(token.gameObject);
            }
            childeIndex = transform.parent.GetSiblingIndex();
            transform.parent.SetAsLastSibling();
            GetComponent<RectTransform>().sizeDelta *= 1.2f;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        for (int i = 0; i < tokenPreview.Count; i++)
        {
            Destroy(tokenPreview[i]);
        }
        tokenPreview.Clear();
        transform.parent.SetSiblingIndex(childeIndex);
        GetComponent<RectTransform>().sizeDelta = defaultSize;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0)&&!isSelected)
        {
            isSelected = true;
            listIndex = transform.parent.parent.parent.parent.GetComponent<PlayerBattleUI>().handList.FindIndex(x => x == transform.parent.gameObject);
            transform.parent.parent.parent.parent.GetComponent<PlayerBattleUI>().handList.Remove(transform.parent.gameObject);
            for (int i = 0; i < tokenPreview.Count; i++)
            {
                tokenPreview[i].SetActive(false);
            }
            defaultPosition = transform.position;
            positionDistance = defaultPosition - Input.mousePosition;
            transform.position = Input.mousePosition + positionDistance;
        }
        if (layerMask == 1 << N_BattleManager.instance.currentUnit.gameObject.layer)
        {
            canUseList.Add(Instantiate(BattleUI.instance.targetIndicator, N_BattleManager.instance.currentUnit.gameObject.transform.position, Quaternion.identity));
        }
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (layerMask == 1 << unit.gameObject.layer)
            {
                canUseList.Add(Instantiate(BattleUI.instance.targetIndicator, unit.transform.position, Quaternion.identity));
            }
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
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
                    transform.parent.parent.parent.parent.GetComponent<PlayerBattleUI>().handList.Insert(listIndex,transform.parent.gameObject);
                }
            }

            isSelected = false;
        }
        foreach(GameObject indicator in canUseList)
        {
            Destroy(indicator);
        }
        canUseList.Clear();
        BattleUI.instance.currentTargetIndicator.SetActive(false);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isSelected)
            return;
        transform.position = Input.mousePosition + positionDistance;
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 80,layerMask))
        {
            target = hit.transform.gameObject;
            BattleUI.instance.currentTargetIndicator.transform.position = target.transform.position;
            BattleUI.instance.currentTargetIndicator.SetActive(true);
        }
        else
        {
            target = null;
            BattleUI.instance.currentTargetIndicator.SetActive(false);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void TransferUI()
    {
        GameObject parent = transform.parent.gameObject;
        transform.SetParent(transform.parent.parent.parent);
        transform.parent.parent.GetComponent<PlayerBattleUI>().handList.Remove(parent);
        Destroy(parent);
        transform.position = new Vector2(Camera.main.pixelWidth*2,0);
    }
}