using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckCardUI : MonoBehaviour
{
    public CardData cardData;
    public GameObject[] backGrounds;
    public Transform cost;
    public TMP_Text cardName;
    public Image image;
    public TMP_Text description;
    public TMP_Text amount;

    int amountValue = 1;


    public void SetAmount(int i)
    {
        amountValue = i;
        amount.text = "x" + amountValue.ToString();
    }
    public void AddAmount(int i)
    {
        amountValue += i;
        amount.text = "x" + amountValue.ToString();
    }
    public void LoadCardData(int cardID)    //���߿� �ι�° �μ� ����
    {
        cardData = DataBase.instance.cardData.Find(x => x.no == cardID);
        foreach (GameObject backGround in backGrounds)
            backGround.SetActive(false);

        switch (cardData.attackType)
        {
            case CardData.AttackType.Attack:
                backGrounds[0].SetActive(true);
                break;
            case CardData.AttackType.Defence:
                backGrounds[1].SetActive(true);
                break;
            case CardData.AttackType.Endow:
                backGrounds[2].SetActive(true);
                break;
            case CardData.AttackType.Increase:
                backGrounds[3].SetActive(true);
                break;
            case CardData.AttackType.CardDraw:
                backGrounds[4].SetActive(true);
                break;
        }

        //�̸�
        cardName.text = cardData.name;

        //�ڽ�Ʈ
        for (int i = 0; i < cost.childCount; i++)
        {
            cost.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < cardData.useCost; i++)
        {
            cost.GetChild(i).gameObject.SetActive(true);
        }
        image.sprite = Resources.Load<Sprite>("UI/Cards/" + cardData.variableName);
        if (cardData.variableName == "Defcon")
            image.sprite = Resources.Load<Sprite>("UI/Cards/Defcon_fighter");

        //����
        string dummy;

        if (cardData.description.Contains("ȸ��"))
            dummy = "green";
        else if (cardData.description.Contains("����") && cardData.description.Contains("����"))
            dummy = "magenta";
        else if (cardData.description.Contains("����"))
            dummy = "blue";
        else
            dummy = "red";
        if (!cardData.description.Contains("x"))
            description.text = cardData.description;
        else
            description.text = cardData.description.Substring(0, cardData.description.IndexOf("x"))
                         + "<b><color=" + dummy + ">"
                         + cardData.defaultXvalue
                         + "</color></b>"
                         + cardData.description.Substring(cardData.description.IndexOf("x") + 1);

        //����
        amount.text = "x" + amountValue.ToString();
    }
     public void ResetCardUI()
    {
        /*cardNo = 0;
        SwitchingBG(CardData.AttackType.Attack);
        cardIcon.sprite = null;
        for (int i = 0; i < cost.transform.childCount; i++)
        {
            cost.transform.GetChild(i).gameObject.SetActive(false);
        }
        descrtiption.text = null;
        inventoryUI = null;*/

        cardData = null;
        amount.text = null;
        for (int i = 0; i < cost.childCount; i++)
        {
            cost.GetChild(i).gameObject.SetActive(false);
        }
    }

}
