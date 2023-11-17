using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
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
        amountText.text = amount.ToString();
    }
}
