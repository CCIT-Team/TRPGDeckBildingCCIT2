using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private GameObject inventory;

    public Image[] equipmentSlot;
    public TMP_Text[] equipmentSlotText;
    public GameObject[] scrollViewBox;
    public GameObject itemSlot;

    public TMP_Text str;
    public TMP_Text intl;
    public TMP_Text luk;
    public TMP_Text spd;
    public TMP_Text money;

    public GameObject[] avatar;
    public GameObject cardGrid;

    public List<InventorySlotUI> helmetIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> armorIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> weaponIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> jewelIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> itemIndex = new List<InventorySlotUI>();
    public GameObject likedPlayer;
    private GameObject slot;

    private void Start()
    {
        inventory = transform.GetChild(0).gameObject;
        str.text = likedPlayer.GetComponent<Character>().strength.ToString();
        intl.text = likedPlayer.GetComponent<Character>().intelligence.ToString();
        luk.text = likedPlayer.GetComponent<Character>().luck.ToString();
        spd.text = likedPlayer.GetComponent<Character>().speed.ToString();
        money.text = likedPlayer.GetComponent<Character>().gold.ToString();

        if(likedPlayer.GetComponent<Character_type>().major == PlayerType.Major.Fighter)
        {
            avatar[0].SetActive(true);
            avatar[1].SetActive(false);
            avatar[2].SetActive(false);
        }
        else if(likedPlayer.GetComponent<Character_type>().major == PlayerType.Major.Wizard)
        {
            avatar[0].SetActive(false);
            avatar[1].SetActive(true);
            avatar[2].SetActive(false);
        }
        else if (likedPlayer.GetComponent<Character_type>().major == PlayerType.Major.Cleric)
        {
            avatar[0].SetActive(false);
            avatar[1].SetActive(false);
            avatar[2].SetActive(true);
        }

        inventory.SetActive(false);
    }

    public void UpdateStat()
    {
        str.text = likedPlayer.GetComponent<Character>().strength.ToString();
        intl.text = likedPlayer.GetComponent<Character>().intelligence.ToString();
        luk.text = likedPlayer.GetComponent<Character>().luck.ToString();
        spd.text = likedPlayer.GetComponent<Character>().speed.ToString();
    }

    public void SetInvenItem(int number, int amount)
    {
        int index = int.Parse(number.ToString().Substring(0, 5));
        switch (index)
        {
            case 12000:
                slot = Instantiate(itemSlot);
                slot.transform.SetParent(scrollViewBox[4].transform);
                slot.GetComponent<InventorySlotUI>().SetSlotType(InventorySlotUI.ItemType.item);
                itemIndex.Add(slot.GetComponent<InventorySlotUI>());
                itemIndex[itemIndex.Count - 1].SetSlotItem(number, amount);
                break;
            case 12001:
                slot = Instantiate(itemSlot);
                slot.transform.SetParent(scrollViewBox[2].transform);
                slot.GetComponent<InventorySlotUI>().SetSlotType(InventorySlotUI.ItemType.weapon);
                weaponIndex.Add(slot.GetComponent<InventorySlotUI>());
                weaponIndex[weaponIndex.Count-1].SetSlotItem(number, 1);
                break;
            case 22000:
                for(int i = 0; i < DataBase.instance.armorData.Count; i++)
                {
                    if (DataBase.instance.armorData[i].no == number && DataBase.instance.armorData[i].type == ArmorData.Type.Armor)
                    {
                        slot = Instantiate(itemSlot);
                        slot.transform.SetParent(scrollViewBox[1].transform);
                        slot.GetComponent<InventorySlotUI>().SetSlotType(InventorySlotUI.ItemType.armor);
                        armorIndex.Add(slot.GetComponent<InventorySlotUI>());
                        armorIndex[armorIndex.Count - 1].SetSlotItem(number, 1);
                    }
                    else if(DataBase.instance.armorData[i].no == number && DataBase.instance.armorData[i].type == ArmorData.Type.Head)
                    {
                        slot = Instantiate(itemSlot);
                        slot.transform.SetParent(scrollViewBox[0].transform);
                        slot.GetComponent<InventorySlotUI>().SetSlotType(InventorySlotUI.ItemType.helmet);
                        helmetIndex.Add(slot.GetComponent<InventorySlotUI>());
                        helmetIndex[helmetIndex.Count - 1].SetSlotItem(number, 1);
                    }
                    else if (DataBase.instance.armorData[i].no == number && DataBase.instance.armorData[i].type == ArmorData.Type.Jewel)
                    {
                        slot = Instantiate(itemSlot);
                        slot.transform.SetParent(scrollViewBox[3].transform);
                        slot.GetComponent<InventorySlotUI>().SetSlotType(InventorySlotUI.ItemType.jewel);
                        jewelIndex.Add(slot.GetComponent<InventorySlotUI>());
                        jewelIndex[jewelIndex.Count - 1].SetSlotItem(number, 1);
                    }
                }
                break;
        }
    }
    public void UpdateMoneyUI()
    {
        money.text = likedPlayer.GetComponent<Character>().gold.ToString();
    }
    public void ButtonOpen()
    {
        if (likedPlayer.GetComponent<Character>().isMyturn)
        {
            inventory.SetActive(true);
        }
    }

    public void SetItemButton()
    {
        //SetInvenItem(12001001, 10);
        SetInvenItem(12001012, 10);
    }
    private void Update()
    {
        if(likedPlayer.GetComponent<Character>().isMyturn)
        {
            if (Input.GetKeyDown(KeyCode.I) && !inventory.activeInHierarchy)
            {
                Map.instance.isOutofUI = true;
                inventory.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.I) && inventory.activeInHierarchy)
            {
                Map.instance.isOutofUI = false;
                inventory.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (likedPlayer.GetComponent<Character_type>().major == PlayerType.Major.Fighter)
                {
                    SetInvenItem(12001001, 10);
                    SetInvenItem(22000001, 10);
                    SetInvenItem(22000002, 10);
                }
                else if(likedPlayer.GetComponent<Character_type>().major == PlayerType.Major.Wizard)
                {
                    SetInvenItem(12001018, 10);
                    SetInvenItem(22000021, 10);
                    SetInvenItem(22000022, 10);
                }
                else if (likedPlayer.GetComponent<Character_type>().major == PlayerType.Major.Cleric)
                {
                    SetInvenItem(12001026, 10);
                    SetInvenItem(12001016, 10);
                    SetInvenItem(22000001, 10);
                    SetInvenItem(22000002, 10);
                }
            }
        }
        else if(!likedPlayer.GetComponent<Character>().isMyturn)
        {
            inventory.SetActive(false);
        }
       
    }
}
