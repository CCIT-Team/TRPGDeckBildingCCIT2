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
        helmet,
        item,
        weapon,
        armor,
        jewel
    }
    public ItemType itemType;
    public GameObject[] selectObject;
    public GameObject cardImage;

    public int itemNo;
    public Image image;

    public int amount;
    public TMP_Text nameText;
    public TMP_Text statText;
    public TMP_Text cardAmountText;

    private InventoryUI rootObject;
    private bool isLeft;

    private void Start()
    {
        rootObject = transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<InventoryUI>();
    }

    public void SetSlotType(ItemType _itemType)
    {
        itemType = _itemType;
        if(itemType == ItemType.item)
        {
            cardImage.SetActive(false);
        }
    }

    public void SetSlotItem(int _itemNo, int _amount)
    {
        itemNo = _itemNo;
        amount = _amount;
        int temp = 0;
        image.sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
        if(itemType == ItemType.helmet)
        {
            nameText.text = DataBase.instance.SelectArmor(_itemNo).name;
            statText.text = "힘 + " + DataBase.instance.SelectArmor(_itemNo).strength.ToString() +
                            "\n지능 + " + DataBase.instance.SelectArmor(_itemNo).intelligence.ToString() +
                            "\n운 + " + DataBase.instance.SelectArmor(_itemNo).luck.ToString() +
                            "\n속도 + " + DataBase.instance.SelectArmor(_itemNo).speed.ToString();
            temp = DataBase.instance.SelectArmor(_itemNo).getCard1Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard2Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard3Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard4Count;
            cardAmountText.text = "x" + temp.ToString();
        }
        else if(itemType == ItemType.armor)
        {
            nameText.text = DataBase.instance.SelectArmor(_itemNo).name;
            statText.text = "힘 + " + DataBase.instance.SelectArmor(_itemNo).strength.ToString() +
                            "\n지능 + " + DataBase.instance.SelectArmor(_itemNo).intelligence.ToString() +
                            "\n운 + " + DataBase.instance.SelectArmor(_itemNo).luck.ToString() +
                            "\n속도 + " + DataBase.instance.SelectArmor(_itemNo).speed.ToString();
            temp = DataBase.instance.SelectArmor(_itemNo).getCard1Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard2Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard3Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard4Count;
            cardAmountText.text = "x" + temp.ToString();
        }
        else if(itemType == ItemType.weapon)
        {
            nameText.text = DataBase.instance.SelectWeapon(_itemNo).name;
            statText.text = "힘 + " + DataBase.instance.SelectWeapon(_itemNo).strength.ToString() + 
                            "\n지능 + " + DataBase.instance.SelectWeapon(_itemNo).intelligence.ToString() +
                            "\n운 + " + DataBase.instance.SelectWeapon(_itemNo).luck.ToString() +
                            "\n속도 + " + DataBase.instance.SelectWeapon(_itemNo).speed.ToString();
            temp = DataBase.instance.SelectWeapon(_itemNo).getCard1Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard2Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard3Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard4Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard5Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard6Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard7Count +
                   DataBase.instance.SelectWeapon(_itemNo).getCard8Count;
            cardAmountText.text = "x"+temp.ToString();
        }
        else if(itemType == ItemType.jewel)
        {
            nameText.text = DataBase.instance.SelectArmor(_itemNo).name;
            statText.text = "힘 + " + DataBase.instance.SelectArmor(_itemNo).strength.ToString() +
                            "\n지능 + " + DataBase.instance.SelectArmor(_itemNo).intelligence.ToString() +
                            "\n운 + " + DataBase.instance.SelectArmor(_itemNo).luck.ToString() +
                            "\n속도 + " + DataBase.instance.SelectArmor(_itemNo).speed.ToString();
            temp = DataBase.instance.SelectArmor(_itemNo).getCard1Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard2Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard3Count +
                   DataBase.instance.SelectArmor(_itemNo).getCard4Count;
            cardAmountText.text = "x" + temp.ToString();
        }
        else if (itemType == ItemType.item)
        {
            for(int i = 0; i < DataBase.instance.itemData.Count; i++)
            {
                if(_itemNo == DataBase.instance.itemData[i].no)
                {
                    nameText.text = DataBase.instance.itemData[i].name;
                    statText.text = DataBase.instance.itemData[i].effect;
                    cardAmountText.text = "x" + _amount.ToString();
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData evenData)
    {
        if(evenData.button == PointerEventData.InputButton.Right)
        {
            if(itemType == ItemType.helmet)
            {
                Debug.Log("머리");
                EquipArmor(itemNo);
            }
            else if (itemType == ItemType.armor)
            {
                Debug.Log("갑옷");
                EquipArmor(itemNo);
            }
            else if(itemType == ItemType.weapon)
            {
                Debug.Log("무기");
                Equip(itemNo);
            }
            else if (itemType == ItemType.jewel)
            {
                Debug.Log("보석");
                EquipArmor(itemNo);
            }
            else if (itemType == ItemType.item)
            {
                Debug.Log("아이템");
                UsingItem(itemNo);
            }
            CardUnDisplay();
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
    private void EquipArmor(int _itemNo)
    {
        if (DataBase.instance.SelectArmor(_itemNo).type == ArmorData.Type.Head)
        {
            if (!rootObject.likedPlayer.GetComponent<Character_Equipment>().isHelmet)
            {
                rootObject.likedPlayer.GetComponent<Character_Equipment>().isHelmet = true;
                rootObject.equipmentSlot[0].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                rootObject.equipmentSlot[0].color = Color.white;
                rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().SetArmorType(DataBase.instance.SelectArmor(_itemNo));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetArmorCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
            else
            {
                rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().UnEquip(); //교체되는 부분

                rootObject.likedPlayer.GetComponent<Character_Equipment>().isHelmet = true;
                rootObject.equipmentSlot[0].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                rootObject.equipmentSlot[0].color = Color.white;
                rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().SetArmorType(DataBase.instance.SelectArmor(_itemNo));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetArmorCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[0].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
        }
        else if (DataBase.instance.SelectArmor(_itemNo).type == ArmorData.Type.Armor)
        {
            if (!rootObject.likedPlayer.GetComponent<Character_Equipment>().isArmor)
            {
                rootObject.likedPlayer.GetComponent<Character_Equipment>().isArmor = true;
                rootObject.equipmentSlot[1].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                rootObject.equipmentSlot[1].color = Color.white;
                rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().SetArmorType(DataBase.instance.SelectArmor(_itemNo));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetArmorCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
            else
            {
                rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().UnEquip(); //교체되는 부분

                rootObject.likedPlayer.GetComponent<Character_Equipment>().isArmor = true;
                rootObject.equipmentSlot[1].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                rootObject.equipmentSlot[1].color = Color.white;
                rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().SetArmorType(DataBase.instance.SelectArmor(_itemNo));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetArmorCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[1].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
        }
        else if (DataBase.instance.SelectArmor(_itemNo).type == ArmorData.Type.Jewel)
        {
            int boolCount = 0;
            for(int i = 0; i < rootObject.likedPlayer.GetComponent<Character_Equipment>().isJewel.Length; i++)
            {
                if(!rootObject.likedPlayer.GetComponent<Character_Equipment>().isJewel[i])
                {
                    rootObject.likedPlayer.GetComponent<Character_Equipment>().isJewel[i] = true;
                    rootObject.equipmentSlot[i+4].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                    rootObject.equipmentSlot[i+4].color = Color.white;
                    rootObject.equipmentSlot[i+4].gameObject.GetComponent<ArmorEquipmentSlotUI>().SetArmorType(DataBase.instance.SelectArmor(_itemNo));

                    rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[i+4].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetArmorCard());
                    rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[i+4].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetStats());
                    rootObject.UpdateStat();
                    break;
                }
                boolCount += 1;
            }
            if(boolCount == 4)
            {
                rootObject.equipmentSlot[4].gameObject.GetComponent<ArmorEquipmentSlotUI>().UnEquip(); //교체되는 부분

                rootObject.likedPlayer.GetComponent<Character_Equipment>().isArmor = true;
                rootObject.equipmentSlot[4].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _itemNo.ToString());
                rootObject.equipmentSlot[4].color = Color.white;
                rootObject.equipmentSlot[4].gameObject.GetComponent<ArmorEquipmentSlotUI>().SetArmorType(DataBase.instance.SelectArmor(_itemNo));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[4].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetArmorCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[4].gameObject.GetComponent<ArmorEquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
                boolCount = 0;
            }
        }
        ResetSlot();
    }
    private void Equip(int _itemNo)
    {
        if (DataBase.instance.SelectWeapon(_itemNo).type == WeaponData.Type.Shield)
        {
            isLeft = true;
        }
        else if (DataBase.instance.SelectWeapon(_itemNo).type == WeaponData.Type.Staff)
        {
            isLeft = true;
        }
        else if (DataBase.instance.SelectWeapon(_itemNo).type == WeaponData.Type.Wand)
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }

        Equipweapon(isLeft, _itemNo);
        ResetSlot();
    }

    private void Equipweapon(bool _isLeft, int _no)
    {
        if (_isLeft)
        {
            if (!rootObject.likedPlayer.GetComponent<Character_Equipment>().isLeftWeapon)
            {
                rootObject.likedPlayer.GetComponent<Character_Equipment>().isLeftWeapon = true;
                rootObject.equipmentSlot[2].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _no.ToString());
                rootObject.equipmentSlot[2].color = Color.white;
                rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.SelectWeapon(_no));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().GetWeaponCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
            else
            {
                rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().UnEquip(); //교체되는 부분

                rootObject.likedPlayer.GetComponent<Character_Equipment>().isLeftWeapon = true;
                rootObject.equipmentSlot[2].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _no.ToString());
                rootObject.equipmentSlot[2].color = Color.white;
                rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.SelectWeapon(_no));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().GetWeaponCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[2].gameObject.GetComponent<EquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
        }
        else
        {
            if (!rootObject.likedPlayer.GetComponent<Character_Equipment>().isRightWeapon)
            {
                rootObject.likedPlayer.GetComponent<Character_Equipment>().isRightWeapon = true;
                rootObject.equipmentSlot[3].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _no.ToString());
                rootObject.equipmentSlot[3].color = Color.white;
                rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.SelectWeapon(_no));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().GetWeaponCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
            else
            {
                rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().UnEquip(); //교체되는 부분

                rootObject.likedPlayer.GetComponent<Character_Equipment>().isRightWeapon = true;
                rootObject.equipmentSlot[3].sprite = Resources.Load<Sprite>("Test_Assets/UI/" + _no.ToString());
                rootObject.equipmentSlot[3].color = Color.white;
                rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().SetWeaponType(DataBase.instance.SelectWeapon(_no));

                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddDeck(rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().GetWeaponCard());
                rootObject.likedPlayer.GetComponent<Character_Equipment>().AddStat(rootObject.equipmentSlot[3].gameObject.GetComponent<EquipmentSlotUI>().GetStats());
                rootObject.UpdateStat();
            }
        }
    }

    private void ResetSlot()
    {
        if(itemType == ItemType.helmet)
        {
            rootObject.helmetIndex.Remove(this);
            Destroy(gameObject);
        }
        else if(itemType == ItemType.armor)
        {
            rootObject.armorIndex.Remove(this);
            Destroy(gameObject);
        }
        else if(itemType == ItemType.weapon)
        {
            rootObject.weaponIndex.Remove(this);
            Destroy(gameObject);
        }
        else if(itemType == ItemType.jewel)
        {
            rootObject.jewelIndex.Remove(this);
            Destroy(gameObject);
        }
    }

    private void CardDisplay()
    {
        if (itemType == ItemType.weapon)
        {
            int[] cardNos = new int[8];
            int[] cardcount = new int[8];
            int temp = 0;
            cardNos[0] = DataBase.instance.SelectWeapon(itemNo).getCard1;
            cardNos[1] = DataBase.instance.SelectWeapon(itemNo).getCard2;
            cardNos[2] = DataBase.instance.SelectWeapon(itemNo).getCard3;
            cardNos[3] = DataBase.instance.SelectWeapon(itemNo).getCard4;
            cardNos[4] = DataBase.instance.SelectWeapon(itemNo).getCard5;
            cardNos[5] = DataBase.instance.SelectWeapon(itemNo).getCard6;
            cardNos[6] = DataBase.instance.SelectWeapon(itemNo).getCard7;
            cardNos[7] = DataBase.instance.SelectWeapon(itemNo).getCard8;

            cardcount[0] = DataBase.instance.SelectWeapon(itemNo).getCard1Count;
            cardcount[1] = DataBase.instance.SelectWeapon(itemNo).getCard2Count;
            cardcount[2] = DataBase.instance.SelectWeapon(itemNo).getCard3Count;
            cardcount[3] = DataBase.instance.SelectWeapon(itemNo).getCard4Count;
            cardcount[4] = DataBase.instance.SelectWeapon(itemNo).getCard5Count;
            cardcount[5] = DataBase.instance.SelectWeapon(itemNo).getCard6Count;
            cardcount[6] = DataBase.instance.SelectWeapon(itemNo).getCard7Count;
            cardcount[7] = DataBase.instance.SelectWeapon(itemNo).getCard8Count;


            for (int i = 0; i < cardNos.Length; i++)
            {
                if (cardNos[i] != 0)
                {
                    for (int j = 0; j < cardcount[i]; j++)
                    {
                        rootObject.cardGrid.transform.GetChild(temp).GetComponent<CardDisPlayUI>().inventoryUI = rootObject;
                        rootObject.cardGrid.transform.GetChild(temp).GetComponent<CardDisPlayUI>().SetCardUI(cardNos[i]);
                        rootObject.cardGrid.transform.GetChild(temp++).gameObject.SetActive(true);
                    }
                }
            }
        }
        else if(itemType == ItemType.helmet || itemType == ItemType.armor || itemType == ItemType.jewel)
        {
            int[] cardNos = new int[4];
            int[] cardcount = new int[4];
            int temp = 0;
            cardNos[0] = DataBase.instance.SelectArmor(itemNo).getCard1;
            cardNos[1] = DataBase.instance.SelectArmor(itemNo).getCard2;
            cardNos[2] = DataBase.instance.SelectArmor(itemNo).getCard3;
            cardNos[3] = DataBase.instance.SelectArmor(itemNo).getCard4;

            cardcount[0] = DataBase.instance.SelectArmor(itemNo).getCard1Count;
            cardcount[1] = DataBase.instance.SelectArmor(itemNo).getCard2Count;
            cardcount[2] = DataBase.instance.SelectArmor(itemNo).getCard3Count;
            cardcount[3] = DataBase.instance.SelectArmor(itemNo).getCard4Count;

            for (int i = 0; i < cardNos.Length; i++)
            {
                if (cardNos[i] != 0)
                {
                    for (int j = 0; j < cardcount[i]; j++)
                    {
                        rootObject.cardGrid.transform.GetChild(temp).GetComponent<CardDisPlayUI>().inventoryUI = rootObject;
                        rootObject.cardGrid.transform.GetChild(temp).GetComponent<CardDisPlayUI>().SetCardUI(cardNos[i]);
                        rootObject.cardGrid.transform.GetChild(temp++).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void CardUnDisplay()
    {
        for(int i = 0; i < rootObject.cardGrid.transform.childCount; i++)
        {
            rootObject.cardGrid.transform.GetChild(i).GetComponent<CardDisPlayUI>().ResetCardUI();
            rootObject.cardGrid.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        selectObject[0].SetActive(true);
        selectObject[1].SetActive(true);
        CardDisplay();
        Debug.Log(itemNo.ToString() + "올라옴");
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        selectObject[0].SetActive(false);
        selectObject[1].SetActive(false);
        CardUnDisplay();
        Debug.Log(itemNo.ToString() + "나감");
    }
}
