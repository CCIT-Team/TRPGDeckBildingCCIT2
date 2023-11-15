using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Text text;

    public N_Card bindCard;

    Vector3 defaultPosition = new Vector3(0,0,0);

    public LayerMask layerMask;
    GameObject target;

    void Start()
    {

    }

    void OnEnable()
    {
        bindCard = GetComponent<N_Card>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
        defaultPosition = transform.position;
        transform.position = Input.mousePosition;
        //StartCoroutine(SelectCard());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointerUp");
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
        Debug.Log("EndDrag");
    }
}