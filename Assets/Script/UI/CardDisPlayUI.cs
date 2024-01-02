using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisPlayUI : MonoBehaviour
{
    private int cardNo = 0;
    public InventoryUI inventoryUI;
    public GameObject cardBG;
    public TMP_Text cardName;
    public Image cardIcon;
    public GameObject cost;
    public TMP_Text descrtiption;

    private int mainStatus = 0;

    public void SetCardUI(int _no)
    {
        cardNo = _no;
        for(int i = 0; i < DataBase.instance.cardData.Count; i++)
        {
            if(cardNo == DataBase.instance.cardData[i].no)
            {
                SwitchingBG(DataBase.instance.cardData[i].attackType);
                cardName.text = DataBase.instance.cardData[i].name;
                cardIcon.sprite = Resources.Load<Sprite>("UI/Cards/" + DataBase.instance.cardData[i].variableName);
                DisplayCost(i);
                descrtiption.text = CardDescription(i);
            }
        }
    }

    public void ResetCardUI()
    {
        cardNo = 0;
        SwitchingBG(CardData.AttackType.Attack);
        cardIcon.sprite = null;
        for (int i = 0; i < cost.transform.childCount; i++)
        {
            cost.transform.GetChild(i).gameObject.SetActive(false);
        }
        descrtiption.text = null;
        inventoryUI = null;
    }

    private void SwitchingBG(CardData.AttackType type)
    {
        for(int i = 0; i < cardBG.transform.childCount; i++)
        {
            cardBG.transform.GetChild(i).gameObject.SetActive(false);
        }

        switch(type)
        {
            case CardData.AttackType.Attack:
                cardBG.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case CardData.AttackType.Defence:
                cardBG.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case CardData.AttackType.CardDraw:
                cardBG.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case CardData.AttackType.Endow:
                cardBG.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case CardData.AttackType.Increase:
                cardBG.transform.GetChild(4).gameObject.SetActive(true);
                break;
        }
    }

    private void DisplayCost(int index)
    {
        for (int i = 0; i < cost.transform.childCount; i++)
        {
            cost.transform.GetChild(i).gameObject.SetActive(false);
        }

        for(int i = 0; i < DataBase.instance.cardData[index].useCost; i++)
        {
            cost.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private string CardDescription(int index)
    {
        string descript = null;
        string damageColor = null;

        if (DataBase.instance.cardData[index].description.Contains("회복"))
        {
            damageColor = "green";
            mainStatus = int.Parse(inventoryUI.intl.text);
        }
        else if (DataBase.instance.cardData[index].description.Contains("마법") && DataBase.instance.cardData[index].description.Contains("물리"))
        {
            damageColor = "magenta";
            mainStatus = int.Parse(inventoryUI.intl.text);
        }
        else if (DataBase.instance.cardData[index].description.Contains("마법"))
        {
            damageColor = "#00AFFF";
            mainStatus = int.Parse(inventoryUI.intl.text);
        }
        else
        {
            damageColor = "red";
            mainStatus = int.Parse(inventoryUI.str.text);
        }

        if (!DataBase.instance.cardData[index].description.Contains("x"))
            descript = DataBase.instance.cardData[index].description;
        else
            descript = DataBase.instance.cardData[index].description.Substring(0, DataBase.instance.cardData[index].description.IndexOf("x"))
                         + "<b><color=" + damageColor + ">"
                         + (CalculateCardValue(index)).ToString()
                         + "</color></b>"
                         + DataBase.instance.cardData[index].description.Substring(DataBase.instance.cardData[index].description.IndexOf("x") + 1);

        return descript;
    }

    private float CalculateCardValue(int index)
    {
        float finalVlaue = 0;
        finalVlaue = DataBase.instance.cardData[index].defaultXvalue * mainStatus * 0.02f;
        return finalVlaue;
    }
}
