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

    private void Awake()
    {
        if (bindCard == null)
            bindCard = GetComponent<N_Card>();
    }

    public void DisplayOnUI()
    {
        cardName.text = bindCard.cardData.name;
        description.text = bindCard.cardData.description.Substring(0, bindCard.cardData.description.IndexOf("x"))
                         + "<b><color=red>"
                         + (bindCard.CalculateCardValue()).ToString()
                         + "</color></b>"
                         + bindCard.cardData.description.Substring(bindCard.cardData.description.IndexOf("x") + 1);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
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
        Debug.Log("BeginDrag");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
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
}