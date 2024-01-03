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

    public void CardEffect()
    {
        var skill = CardSkills.SearchSkill(cardData.variableName);
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
        cardAction += () => GetComponent<Deck>().HandToGrave(cardID);
        cardAction += () => N_BattleManager.instance.IsAction = true;
        cardAction += () => token_Fail = RollToken(MainStaus,mainStatus, cardData.token);
        cardAction += () => GetComponent<UnitAnimationControl>().AttackAnimation();
    }

    public void SetCardData(int id)
    {
        cardData = DataBase.instance.cardData.Find(x => x.no == id);
        
        switch (cardData.useStatType)
        {
            case CardData.UseStatType.None:
                MainStaus = StatusType.Luck;
                mainStatus = GetComponent<Monster>().luck;
                break;
            case CardData.UseStatType.Strength:
                MainStaus = StatusType.Strength;
                mainStatus = GetComponent<Monster>().strength;
                break;
            case CardData.UseStatType.Intellect:
                MainStaus = StatusType.Intelligence;
                mainStatus = GetComponent<Monster>().intelligence;
                break;
            default:
                break;
        }

        if (cardData.skillType == CardData.SkillType.SingleFriendly || cardData.skillType == CardData.SkillType.SingleMyself || cardData.skillType == CardData.SkillType.MultiMyself)
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
            case "Dragon'sScream":
                return 0;
            case "Dragon'sClaw":
                return 1;
            case "Dragon'sWind":
                return 2;
        }
    }

    public int TypeSelectByNo(int no)
    {
        switch(int.Parse(no.ToString().Substring(0, 2) + no.ToString().Substring(no.ToString().Length - 2)))
        {
            default:
                return 0;
            case 51000001:
                return 0;
            case 51000002:
                return 0;
            case 51000003:
                return 0;
            case 51000004:
                return 0;
            case 51000007:
                return 0;
            case 51000009:
                return 0;
            case 51000010:
                return 0;
            case 51000011:
                return 0;
            case 51000013:
                return 0;
            case 51000015:
                return 0;
            case 51000016:
                return 0;
            case 51000017:
                return 0;
            case 51000018:
                return 0;
            case 51000019:
                return 0;
            case 52000004:
                return 0;
            case 52000010:
                return 0;
            case 52000011:
                return 0;
            case 52000014:
                return 0;
            case 54000002:
                return 0;
            case 54000003:
                return 0;
            case 54000006:
                return 0;
            case 54000007:
                return 0;
            case 54000008:
                return 0;
            case 54000015:
                return 0;
            case 54000017:
                return 0;
            case 54000019:
                return 0;
            case 54000020:
                return 0;
            case 54000021:
                return 0;
            case 54000024:
                return 0;
            case 54000041:
                return 0;
            case 54000043:
                return 0;
            case 54000044:
                return 0;
            case 54000045:
                return 0;
            case 54000046:
                return 0;
            case 54000048:
                return 0;
            case 54000053:
                return 0;
            case 54000054:
                return 1;
            case 54000055:
                return 2;
            case 56000001:
                return 0;
            case 56000005:
                return 0;
            case 58000002:
                return 0;
            case 58000003:
                return 0;
            case 58000007:
                return 0;
            case 58000010:
                return 0;
            case 58000012:
                return 0;
            case 59000002:
                return 0;
            case 59000003:
                return 0;
            case 59000011:
                return 0;
            case 60000001:
                return 0;
            case 60000002:
                return 0;
            case 60000003:
                return 0;

        }
    }
}
