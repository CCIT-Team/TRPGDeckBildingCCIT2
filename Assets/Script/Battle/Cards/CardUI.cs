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

    bool isselected = false;
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

    IEnumerator SelectCard()
    {

        yield return new WaitUntil(() => false);
    }

    private void OnMouseDown()
    {
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OPD");
        defaultPosition = transform.position;
        transform.position = Input.mousePosition;
        isselected = true;
        //StartCoroutine(SelectCard());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OPU");
        transform.position = defaultPosition;
        bindCard.cardTarget = target;
        bindCard.UseCard();
        isselected = false;
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
        transform.position = Input.mousePosition;
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 80,layerMask))
        {
            target = hit.transform.gameObject;
        }
    }
}