using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum ItemType
    {
        none,
        item,
        weapon,
        armor,
        jewel
    }
    public ItemType itemType;
    public int slotIndex;
    public bool ishave;

    public int itemNo;
    public Image image;


    public int amount;
    public TMP_Text amountText;


    public void SetSlotNumber(ItemType _itemType, int _slotIndex)
    {
        itemType = _itemType;
        slotIndex = _slotIndex;
        ishave = false;
    }

    public void SetSlotItem(int _itemNo, int _amount)
    {
        ishave = true;
        itemNo = _itemNo;
        amount = _amount;
        image.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
        if(amount <= 1)
        {
            amountText.gameObject.SetActive(false);
        }
        else
        {
            amountText.gameObject.SetActive(true);
            amountText.text = amount.ToString();
        }
    }

    public void OnPointerClick(PointerEventData evenData)
    {
        if(evenData.button == PointerEventData.InputButton.Right)
        {
            if(itemType == ItemType.item)
            {
                Debug.Log("아이템");
            }
            else if(itemType == ItemType.weapon)
            {
                Debug.Log("무기");
                transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<InventoryUI>().equipmentSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + itemNo.ToString());
                transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<InventoryUI>().equipmentSlot.color = new Color(1, 1, 1, 1);
                GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        Debug.Log(itemNo.ToString() + "올라옴");
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        Debug.Log(itemNo.ToString() + "나감");
    }
}
