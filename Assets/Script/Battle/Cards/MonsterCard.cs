using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCard : MonoBehaviour
{
    public delegate void CardAction();
    public CardAction cardAction;

    public int cardID;
    public bool[] tokens;

    public CardData cardData;

    public GameObject cardTarget;

    public void UseCard()
    {
        cardAction();
    }



    void RemoveInHand(Deck deck)
    {
        deck.hand.Remove(cardID);
        deck.grave.Add(cardID);
    }

    public void CardEffect()
    {
        var skill = CardSkills.SearchSkill(cardData.variableName);
        Debug.Log(gameObject.name + "�� "+ cardTarget.name +"���� " + cardData.name + "��(��) ���");
        skill.Invoke(null, new object[] { GetComponent<MonsterStat>(),          //�����
                                          cardTarget.GetComponent<Unit>(),  //��� ���
                                          cardData.defaultXvalue,           //�⺻��
                                          cardData.effectUseTurn,           //�߰�ȿ�� ��
                                          cardData.token                    //��ū ��
                                         });
    }

    public void SetCardAction()
    {
        SetCardData(cardID);
        cardAction = null;
        cardAction += () => RemoveInHand(GetComponent<Deck>());
        cardAction += () => N_BattleManager.instance.IsAction = true;
        //�ִϸ��̼� �ʿ�
        cardAction += () => CardEffect();
        cardAction += () => GetComponent<Monster>().IsMyturn = false;
    }

    public void SetCardData(int id)
    {
        int indexNumber = int.Parse(id.ToString().Substring(2));
        int weaponType = int.Parse(id.ToString().Substring(0, 2));
        switch (weaponType)
        {
            case 50:    //������
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[0]];
                break;
            case 51:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[1]];
                break;
            case 52:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[2]];
                break;
            case 53:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[3]];
                break;
            case 54:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[4]];
                break;
            case 55:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[5]];
                break;
            case 56:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[6]];
                break;
            case 57:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[7]];
                break;
            case 58:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[8]];
                break;
            case 59:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[9]];
                break;
            case 60:    //������
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[10]];
                break;
            case 70:    //Ŭ����
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[11]];
                break;
        }
    }
}
