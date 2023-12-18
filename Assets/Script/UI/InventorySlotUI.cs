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
    private InventoryUI rootObject;
    private bool isLeft;

    private void Start()
    {
        rootObject = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<InventoryUI>();
    }

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

    public void OnPointerClick(PointerEventData evenData) //플레이어 연결
    {
        if(evenData.button == PointerEventData.InputButton.Right)
        {
            if(itemType == ItemType.item && ishave)
            {
                Debug.Log("아이템");
                UsingItem(itemNo);
            }
            else if(itemType == ItemType.weapon && ishave)
            {
                Debug.Log("무기");
                Equip(itemNo);
            }
        }
    }

    private void UsingItem(int _itemNo)
    {
        string itemName = null;
        for(int i = 0; i < DataBase.instance.itemData.Count; i++)
        {
            if(_itemNo == DataBase.instance.itemData[i].no)
            {
                itemName = DataBase.instance.itemData[i].name;
            }
        }
        Debug.Log( itemName + "아이템");
    }

    private void Equip(int _itemNo)
    {
        for(int i = 0; i < DataBase.instance.weaponData.Count; i++)
        {
            if(_itemNo == DataBase.instance.weaponData[i].no)
            {
                if(DataBase.instance.weaponData[i].type == WeaponData.Type.Shield)
                {
                    isLeft = true;
                    rootObject.equipmentLeftSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                    rootObject.equipmentLeftSlot.color = Color.white;
                    rootObject.equipmentLeftSlot.gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.weaponData[i]);
                }
                else if(DataBase.instance.weaponData[i].type == WeaponData.Type.Staff)
                {
                    rootObject.equipmentLeftSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                    rootObject.equipmentLeftSlot.color = Color.white;
                    rootObject.equipmentLeftSlot.gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.weaponData[i]);
                }
                else if(DataBase.instance.weaponData[i].type == WeaponData.Type.Wand)
                {
                    rootObject.equipmentLeftSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                    rootObject.equipmentLeftSlot.color = Color.white;
                    rootObject.equipmentLeftSlot.gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.weaponData[i]);
                }
                else
                {
                    isLeft = false;
                    rootObject.equipmentRightSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                    rootObject.equipmentRightSlot.color = Color.white;
                    rootObject.equipmentRightSlot.gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.weaponData[i]);
                }
                Equipweapon(isLeft, i, _itemNo);
            }
        }
        ResetSlot();
    }

    private void Equipweapon(bool _isLeft, int index, int _no)
    {
        if(_isLeft)
        {
            rootObject.equipmentLeftSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _no.ToString());
            rootObject.equipmentLeftSlot.color = Color.white;
            rootObject.equipmentLeftSlot.gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.weaponData[index]);
        }
        else
        {
            rootObject.equipmentRightSlot.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _no.ToString());
            rootObject.equipmentRightSlot.color = Color.white;
            rootObject.equipmentRightSlot.gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.weaponData[index]);
        }
    }

    private void ResetSlot()
    {
        image.sprite = null;
        itemNo = 0;
        amount = 0;
        ishave = false;
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
