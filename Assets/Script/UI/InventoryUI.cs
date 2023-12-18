using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private GameObject inventory;

    public Image equipmentLeftSlot;
    public Image equipmentRightSlot;
    public GameObject[] scrollViewBox;
    public GameObject itemSlot;
    
    public List<InventorySlotUI> totalIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> itemIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> weaponIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> armorIndex = new List<InventorySlotUI>();
    public List<InventorySlotUI> jewelIndex = new List<InventorySlotUI>();
    private GameObject slot;
    private bool isOpen;
    private void Start()
    {
        inventory = transform.GetChild(0).gameObject;

        int temp = 0;
        for (int i = 0; i < scrollViewBox.Length; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < 200; j++)
                {
                    slot = Instantiate(itemSlot);
                    slot.transform.SetParent(scrollViewBox[temp].transform);
                    slot.GetComponent<InventorySlotUI>().SetSlotNumber(InventorySlotUI.ItemType.none, j);
                    totalIndex.Add(slot.GetComponent<InventorySlotUI>());
                }
                temp++;
            }
            else
            {
                for (int k = 0; k < 50; k++)
                {
                    slot = Instantiate(itemSlot);
                    slot.transform.SetParent(scrollViewBox[temp].transform);
                    slot.GetComponent<InventorySlotUI>().SetSlotNumber(InventorySlotUI.ItemType.none + temp, k);
                    switch(temp)
                    {
                        case 1:
                            itemIndex.Add(slot.GetComponent<InventorySlotUI>());
                            break;
                        case 2:
                            weaponIndex.Add(slot.GetComponent<InventorySlotUI>());
                            break;
                        case 3:
                            armorIndex.Add(slot.GetComponent<InventorySlotUI>());
                            break;
                        case 4:
                            jewelIndex.Add(slot.GetComponent<InventorySlotUI>());

                            break;
                    }
                }
                temp++;
            }
        }
    }

    public void SetInvenItem(int number, int amount)
    {
        int index = int.Parse(number.ToString().Substring(0, 5));
        int emptySlot = 0;
        switch (index)
        {
            case 12000:
                emptySlot = IsHaveSlot(itemIndex);
                itemIndex[emptySlot].SetSlotItem(number, amount);
                break;
            case 12001:
                emptySlot = IsHaveSlot(weaponIndex);
                weaponIndex[emptySlot].SetSlotItem(number, 1);
                break;
            case 22000:
                emptySlot = IsHaveSlot(armorIndex);
                armorIndex[emptySlot].SetSlotItem(number, 1);
                break;
            case 32000:
                emptySlot = IsHaveSlot(jewelIndex);
                jewelIndex[emptySlot].SetSlotItem(number, 1);
                break;
        }
    }

    public void GetInvenItem()
    {

    }

    private int IsHaveSlot(List<InventorySlotUI> invenList)
    {
        for(int i = 0; i < invenList.Count; i++)
        {
            if(!invenList[i].ishave)
            {
                return i;
            }
        }
        return 0;
    }
    private void SortingTotal()
    {
        for(int i = 0; i < itemIndex.Count; i++)
        {
            if(itemIndex[i].ishave)
            {
                totalIndex[IsHaveSlot(totalIndex)] = itemIndex[i];
            }
        }
    }
    private IEnumerator OpenCloseLerp()
    {
        float timer = 0.0f;
        float durtion = 0.25f;
        float t = 0.0f;
        if(isOpen)
        {
            while (timer <= durtion)
            {
                isOpen = false;
                timer += Time.deltaTime;
                t = timer / durtion;
                inventory.transform.localPosition = Vector3.Lerp(new Vector3(960, 0, 0), new Vector3(1609, 0, 0), t);
                yield return null;
            }
        }
        else
        {
            while (timer <= durtion)
            {
                isOpen = true;
                timer += Time.deltaTime;
                t = timer / durtion;
                inventory.transform.localPosition = Vector3.Lerp(new Vector3(1609, 0, 0), new Vector3(960, 0, 0), t);
                yield return null;
            }
        }
        yield return null;
    }

    public void ButtonCoroutie()
    {
        StartCoroutine(OpenCloseLerp());
    }

    public void SetItemButton()
    {
        //SetInvenItem(12001001, 10);
        SetInvenItem(12001012, 10);
        SortingTotal();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(OpenCloseLerp());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetInvenItem(12000001, 10);
            SortingTotal();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetInvenItem(12001001, 10);
            SortingTotal();
        }
    }
}
