using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardUI : MonoBehaviour
{
    public CardData cardData;
    public Image backGround;
    public Text cost;
    public Image type;
    public Image nameBox;
    public Text cardName;
    public RawImage image;
    public Text description;
    public Text amount;

    int amountValue = 1;


    [Header("���ҽ�")]
    public Sprite[] nameBoxSprits;
    public Sprite[] typeSprits;
    public Sprite[] backgroundSprits;

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
        switch (cardData.attackType)
        {
            case CardData.AttackType.Attack:
                nameBox.sprite = nameBoxSprits[0];
                type.sprite = typeSprits[0];
                backGround.sprite = backgroundSprits[0];
                break;
            case CardData.AttackType.Defence:
                nameBox.sprite = nameBoxSprits[1];
                type.sprite = typeSprits[1];
                backGround.sprite = backgroundSprits[1];
                break;
            case CardData.AttackType.Endow:
                nameBox.sprite = nameBoxSprits[2];
                type.sprite = typeSprits[2];
                backGround.sprite = backgroundSprits[2];
                break;
            case CardData.AttackType.Increase:
                nameBox.sprite = nameBoxSprits[3];
                type.sprite = typeSprits[3];
                backGround.sprite = backgroundSprits[3];
                break;
            case CardData.AttackType.CardDraw:
                nameBox.sprite = nameBoxSprits[3];
                type.sprite = typeSprits[3];
                backGround.sprite = backgroundSprits[4];
                break;
        }

        //�̸�
        cardName.text = cardData.name;

        //�ڽ�Ʈ
        cost.text = cardData.useCost.ToString();

        image.texture = (Texture)Resources.Load("UI/Cards/" + cardData.variableName);
        if (cardData.variableName == "Defcon")
            image.texture = (Texture)Resources.Load("UI/Cards/Defcon_fighter");

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
}
