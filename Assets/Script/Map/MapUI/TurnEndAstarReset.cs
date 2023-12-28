using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TurnEndAstarReset : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!Map.instance.isPlayerMoving)
        {
            transform.GetComponentInChildren<Button>().interactable = true;
            Map.instance.isOutofUI = true;
            Map.instance.ResetAstarPath();
        }
        else
        {
            transform.GetComponentInChildren<Button>().interactable = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Map.instance.isPlayerMoving)
        {
            transform.GetComponentInChildren<Button>().interactable = true;
            Map.instance.isOutofUI = false;
            Map.instance.ResetAstarPath();
        }
    }
}
