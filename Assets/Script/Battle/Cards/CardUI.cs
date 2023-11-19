using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Text cardName;
    public Text description; 

    public N_Card bindCard;

    Vector3 defaultPosition = new Vector3(0,0,0);

    public LayerMask layerMask;
    GameObject target;

    string damageColor = "red";

    private void Awake()
    {
        if (bindCard == null)
            bindCard = GetComponent<N_Card>();
    }

    public void DisplayOnUI()
    {
        cardName.text = bindCard.cardData.name;
        if (bindCard.cardData.description.Contains("회복"))
            damageColor = "green";
        else if (bindCard.cardData.description.Contains("마법")&& bindCard.cardData.description.Contains("물리"))
            damageColor = "magenta";
        else if (bindCard.cardData.description.Contains("마법"))
            damageColor = "blue";
        else
            damageColor = "red";
        description.text = bindCard.cardData.description.Substring(0, bindCard.cardData.description.IndexOf("x"))
                         + "<b><color="+ damageColor + ">"
                         + (bindCard.CalculateCardValue()).ToString()
                         + "</color></b>"
                         + bindCard.cardData.description.Substring(bindCard.cardData.description.IndexOf("x") + 1);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        defaultPosition = transform.position;
        transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
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

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
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