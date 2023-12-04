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

    StatusType MainStaus = StatusType.None;
    int mainStatus = 0;
    public float finalVlaue = 0;
    public int token_Fail = 0;

    public void UseCard()
    {
        GetComponent<UnitAnimationControl>().targetControler = cardTarget.GetComponent<UnitAnimationControl>();
        GetComponent<UnitAnimationControl>().ATEvent = () => CardEffect();
        GetComponent<UnitAnimationControl>().AttackType = SimpleTypeSelect();
        cardAction();
    }

    public float CalculateCardValue()
    {
        finalVlaue = cardData.defaultXvalue * mainStatus * 0.02f;
        return finalVlaue;
    }


    void RemoveInHand(Deck deck)
    {
        deck.hand.Remove(cardID);
        deck.grave.Add(cardID);
    }

    public void CardEffect()
    {
        var skill = CardSkills.SearchSkill(cardData.variableName);
        BattleUI.instance.AddLog(gameObject.name + "(이)가 " + cardTarget.name + "에게 " + cardData.name + "을(를) 사용");
        skill.Invoke(null, new object[] { GetComponent<MonsterStat>(),      //사용자
                                         cardTarget.GetComponent<Unit>(),   //사용 대상
                                         CalculateCardValue(),              //값
                                         cardData.effectUseTurn,            //추가효과 턴
                                         token_Fail,                        //실패 토큰 수
                                         cardData.token});                  //총 토큰 수

        GetComponent<Monster>().IsMyturn = false;
    }

    public void SetCardAction()
    {
        SetCardData(cardID);
        cardAction = null;
        cardAction += () => RemoveInHand(GetComponent<Deck>());
        cardAction += () => N_BattleManager.instance.IsAction = true;
        cardAction += () => token_Fail = RollToken(MainStaus,mainStatus, cardData.token);
        cardAction += () => GetComponent<UnitAnimationControl>().AttackAnimation();
    }

    public void SetCardData(int id)
    {
        int indexNumber = int.Parse(id.ToString().Substring(2));
        int weaponType = int.Parse(id.ToString().Substring(0, 2));
        switch (weaponType)
        {
            case 50:    //워리어
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[0]];
                break;
            case 51:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[3]];
                break;
            case 52:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[4]];
                break;
            case 53:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[5]];
                break;
            case 54:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[6]];
                break;
            case 55:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[7]];
                break;
            case 56:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[8]];
                break;
            case 57:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[9]];
                break;
            case 58:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[10]];
                break;
            case 59:
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[11]];
                break;
            case 60:    //메지션
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[1]];
                break;
            case 70:    //클레릭
                cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[2]];
                break;
        }
        if (cardData.description.Contains("회복"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = GetComponent<Monster>().intelligence;
        }
        else if (cardData.description.Contains("마법") && cardData.description.Contains("물리"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = GetComponent<Monster>().intelligence;
        }
        else if (cardData.description.Contains("마법"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = GetComponent<Monster>().intelligence;
        }
        else
        {
            MainStaus = StatusType.Strength;
            mainStatus = GetComponent<Monster>().strength;
        }

        if (cardData.type != CardData.CardType.SingleAttack && cardData.type != CardData.CardType.MultiAttack && cardData.type != CardData.CardType.AllAttack)
        {
            cardTarget = gameObject;
        }
            
    }

    public int RollToken(StatusType statusType, int mainStatus, int tokenAmount)
    {
        int rollResult = 0;
        for (int i = 0; i < tokenAmount; i++)
        {
            int x = Random.Range(0, 100);
            if (x > mainStatus)
            {
                rollResult--;
            }
        }
        return rollResult;
    }

    int SimpleTypeSelect()
    {
        string nameSlot = cardData.variableName;
        switch (nameSlot)
        {
            default:
                return 0;
            case "AlphaStrike":
                return 0;
            case "Bash":
                return 1;
            case "BitingStrike":
                return 2;
            case "BoldStrike":
                return 3;
            case "BulkUp":
                return 4;
            case "Cleave":
                return 5;
            case "CryofVictory":
                return 4;
            case "Defcon":
                return 6;
            case "DoubleAttack":
                return 7;
            case "EvilStrike":
                return 8;
            case "Focus":
                return 9;
            case "Parry":
                return 10;
            case "Slash":
                return 11;
            case "Smite":
                return 12;
            case "SpinAttack":
                return 0;
            case "Stroming":
                return 0;
            case "TrickStrike":
                return 13;
            case "ShootingStar":
                return 0;
            case "ManaShower":
                return 0;
            case "ManaBomb":
                return 1;
            case "BurningFlame":
                return 1;
            case "DoubleCharm":
                return 2;
            case "ManaCirculation":
                return 2;
            case "ManaAmplification":
                return 2;
            case "Agility":
                return 2;
            case "Scheme":
                return 2;
            case "FlameArmor":
                return 2;
            case "Ignition":
                return 2;
            case "FlameofDragon":
                return 3;
            case "FireBall":
                return 4;
            case "IceBall":
                return 4;
            case "ThunderBolt":
                return 4;
            case "ManaBullet":
                return 4;
            case "Flamethrower":
                return 4;
        }
    }
}
