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


    [Header("리소스")]
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
    public void LoadCardData(int cardID)    //나중에 두번째 인수 삭제
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

        //이름
        cardName.text = cardData.name;

        //코스트
        cost.text = cardData.useCost.ToString();

        image.texture = (Texture)Resources.Load("UI/Cards/" + cardData.variableName);
        if (cardData.variableName == "Defcon")
            image.texture = (Texture)Resources.Load("UI/Cards/Defcon_fighter");

        //설명
        string dummy;

        if (cardData.description.Contains("회복"))
            dummy = "green";
        else if (cardData.description.Contains("마법") && cardData.description.Contains("물리"))
            dummy = "magenta";
        else if (cardData.description.Contains("마법"))
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

        //개수
        amount.text = "x" + amountValue.ToString();
    }
}
