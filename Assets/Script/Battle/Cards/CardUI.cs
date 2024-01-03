using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public List<GameObject> backGround;
    public Image image;
    public TMP_Text cardName;
    public Transform cost;
    public TMP_Text description;
    string[] descripion_Text = new string[3];
    public GameObject backSide;
    public GameObject highlight;

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
    public bool isDumping = false;

    private void Awake()
    {
        if (bindCard == null)
            bindCard = GetComponent<N_Card>();
    }

    public void DisplayOnUI()
    {
        defaultSize = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);

        SetBackGround();
        SetImage();
        SetLayerMask();
        cardName.text = bindCard.cardData.name;
        gameObject.name = bindCard.cardData.variableName;
        SetDescription();
        StartCoroutine(UpdateCardData());
    }

    void SetBackGround()
    {
        switch (bindCard.cardData.attackType)
        {
            case CardData.AttackType.Attack:
                backGround[0].SetActive(true);
                break;
            case CardData.AttackType.Defence:
                backGround[1].SetActive(true);
                break;
            case CardData.AttackType.Endow:
                backGround[2].SetActive(true);
                break;
            case CardData.AttackType.Increase:
                backGround[3].SetActive(true);
                break;
            case CardData.AttackType.CardDraw:
                backGround[4].SetActive(true);
                layerMask = 0;
                break;
        }
    }

    void SetImage()
    {
        image.sprite = Resources.Load<Sprite>("UI/Cards/" + bindCard.cardData.variableName);
        if (bindCard.cardData.variableName == "Defcon")
            image.sprite = Resources.Load<Sprite>("UI/Cards/Defcon_fighter");
    }

    void SetLayerMask()
    {
        switch (bindCard.cardData.skillType)
        {
            case CardData.SkillType.MultiEnemy:
            case CardData.SkillType.SingleEnemy:
                layerMask = 1 << LayerMask.NameToLayer("Monster");
                break;
            case CardData.SkillType.SingleFriendly:
                layerMask = 1 << LayerMask.NameToLayer("Player");
                break;
            case CardData.SkillType.SingleMyself:
            case CardData.SkillType.MultiMyself:
                layerMask = 0;
                break;
            case CardData.SkillType.All:
                layerMask = 1 << LayerMask.NameToLayer("Monster");
                layerMask += 1 << LayerMask.NameToLayer("Player");
                break;
        }
    }

    void SetCost()
    {
        for (int i = 0; i < cost.childCount; i++)
        {
            cost.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < bindCard.cardData.useCost; i++)
        {
            cost.GetChild(i).gameObject.SetActive(true);
        }
    }

    void SetDescription()
    {
        if (!bindCard.cardData.description.Contains("x"))
        {
            descripion_Text[0] = bindCard.cardData.description;
            descripion_Text[1] = "";
            descripion_Text[2] = "";
        }
            
        else
        {
            if (bindCard.cardData.description.Contains("회복"))
                damageColor = "green";
            else if (bindCard.cardData.description.Contains("마법") && bindCard.cardData.description.Contains("물리"))
                damageColor = "A0009A4";
            else if (bindCard.cardData.description.Contains("마법"))
                damageColor = "#00AFFF";
            else
                damageColor = "red";

            descripion_Text[0] = bindCard.cardData.description.Substring(0, bindCard.cardData.description.IndexOf("x"))
                               + "<b><color=" + damageColor + ">";
            descripion_Text[1] = (bindCard.CalculateCardValue()).ToString();
            descripion_Text[2] = "</color></b>"
                               + bindCard.cardData.description.Substring(bindCard.cardData.description.IndexOf("x") + 1);
        }     
    }

    void UpdateDescription()
    {
        descripion_Text[1] = (bindCard.CalculateCardValue()).ToString();
        description.text = descripion_Text[0] + descripion_Text[1] + descripion_Text[2];
    }

    IEnumerator UpdateCardData()
    {
        while(gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.1f);
            SetCost();
            UpdateDescription();
        }
    }


    List<GameObject> tokenPreview = new List<GameObject>();
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(!isSelected)
        {
            highlight.SetActive(true);
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
        highlight.SetActive(false);
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
        if (Input.GetMouseButtonDown(0)&&!isSelected)
        {
            isSelected = true;
            highlight.SetActive(false);
            if (transform.parent.parent != BattleUI.instance.extraCardTransform)
            {
                listIndex = bindCard.playerUI.handList.FindIndex(x => x == transform.parent.gameObject);
                bindCard.playerUI.handList.Remove(transform.parent.gameObject);
            }
            for (int i = 0; i < tokenPreview.Count; i++)
            {
                tokenPreview[i].SetActive(false);
            }
            defaultPosition = transform.position;
            positionDistance = defaultPosition - Input.mousePosition;
            transform.position = Input.mousePosition + positionDistance;

            if(N_BattleManager.instance.isHandOver)
            {
                BattleUI.instance.cardDumpZone.SetCardRect(gameObject);
            }
            else
            {
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
        }

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(N_BattleManager.instance.isHandOver)
            {
                if (isDumping)
                {
                    if (BattleUI.instance.extraCardTransform.GetChild(0) != null && BattleUI.instance.extraCardTransform.GetChild(0) != transform.parent)
                    {
                        bindCard.playerUI.handList.Add(BattleUI.instance.extraCardTransform.GetChild(0).gameObject);
                        BattleUI.instance.extraCardTransform.GetChild(0).name = "card";
                        BattleUI.instance.extraCardTransform.GetChild(0).SetParent(bindCard.playerUI.hand);
                    }
                    bindCard.playerUI.boundDeck.HandToGrave(bindCard.cardData.no);
                    BattleUI.instance.cardDumpZone.SetCardRect();
                    N_BattleManager.instance.isHandOver = false;
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    transform.position = defaultPosition;
                    if(transform.parent.parent != BattleUI.instance.extraCardTransform)
                        bindCard.playerUI.handList.Insert(listIndex, transform.parent.gameObject);
                }
            }
            else if (layerMask == 0)
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
                    bindCard.playerUI.handList.Insert(listIndex,transform.parent.gameObject);
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
        if(!N_BattleManager.instance.isHandOver)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 80, layerMask))
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
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void TransferUI()
    {
        GameObject parent = transform.parent.gameObject;
        transform.SetParent(bindCard.playerUI.transform);
        bindCard.playerUI.handList.Remove(parent);
        Destroy(parent);
        transform.position = new Vector2(Camera.main.pixelWidth*2,0);
    }

    
}