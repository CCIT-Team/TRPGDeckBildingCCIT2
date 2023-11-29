using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class N_Card : MonoBehaviour   //카드 정보와 효과 함수만 가질 것
{
    public delegate void CardAction();
    public CardAction cardAction;
    public PlayerBattleUI playerUI;
    public CardData cardData;
    /*
     * no
     * name
     * variable
     * type
     * description
     * defaultx
     * effectturn
     * cost
     * token
     */
    public GameObject cardTarget;
    int token_Fail;

    public StatusType MainStaus = StatusType.None;
    int mainStatus = 0;
    public float finalVlaue = 0;

    bool costOver = false;
    bool costUsed = false;

    public void UseCard()
    {
        playerUI.boundCharacter.GetComponent<UnitAnimationControl>().targetControler = cardTarget.GetComponent<UnitAnimationControl>();
        playerUI.boundCharacter.GetComponent<UnitAnimationControl>().ATEvent = () => CardEffect();
        playerUI.boundCharacter.GetComponent<UnitAnimationControl>().AttackType = SimpleTypeSelect();
        StartCoroutine(DoCardAction());
    }

    void UseCost(Character character)
    {
        if(cardData.useCost == -1)
        {
            costUsed = true;
        }
        else if (character.cost - cardData.useCost < 0)
        {
            costOver = true;
            return;
        }
        character.cost -= cardData.useCost;
        costUsed = true;
    }

    void RemoveInHand(Deck deck)
    {
        deck.hand.Remove(cardData.no);
        deck.grave.Add(cardData.no);
    }

    public void CardEffect()
    {
        Debug.Log(playerUI.boundCharacter.name + "이(가) " + cardTarget.name + "에게 " + cardData.name + "을(를) 사용");
        var skill = CardSkills.SearchSkill(cardData.variableName);
        skill.Invoke(null, new object[] { playerUI.boundCharacter,          //사용자
                                         cardTarget.GetComponent<Unit>(),   //사용 대상
                                         finalVlaue,                        //값
                                         cardData.effectUseTurn,            //추가효과 턴
                                         token_Fail,                        //실패 토큰 수
                                         cardData.token});                  //총 토큰 수
        this.gameObject.SetActive(false);
    }

    public float CalculateCardValue()
    {
        finalVlaue = cardData.defaultXvalue * mainStatus * 0.02f;
        return finalVlaue;
    }

    public void GetCardData(int no)
    {
        int loopCounter = 0;
        while(playerUI.boundCharacter == null) { if (loopCounter++ >= 100) break; }

        int indexNumber = int.Parse(no.ToString().Substring(2));
        int weaponType = int.Parse(no.ToString().Substring(0, 2));
        //switch (weaponType)
        //{
        //    case 50:    //워리어
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[0]];
        //        break;
        //    case 51:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[3]];
        //        break;
        //    case 52:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[4]];
        //        break;
        //    case 53:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[5]];
        //        break;
        //    case 54:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[6]];
        //        break;
        //    case 55:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[7]];
        //        break;
        //    case 56:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[8]];
        //        break;
        //    case 57:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[9]];
        //        break;
        //    case 58:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[10]];
        //        break;
        //    case 59:
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[11]];
        //        break;
        //    case 60:    //메지션
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[1]];
        //        break;
        //    case 70:    //클레릭
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[2]];
        //        break;
        //}
        switch(playerUI.boundCharacter.GetComponent<Character_type>().major)
        {
            case PlayerType.Major.Fighter:
                {
                    foreach(CardData cData in DataBase.instance.fighterCardData)
                    {
                        if (weaponType == int.Parse(cData.no.ToString().Substring(0,2)) && indexNumber == int.Parse(cData.no.ToString().Substring(2)))
                            cardData = cData;
                    }
                }
                break;
            case PlayerType.Major.Wizard:
                {
                    foreach (CardData cData in DataBase.instance.wizardCardData)
                    {
                        if (weaponType == int.Parse(cData.no.ToString().Substring(0, 2)) && indexNumber == int.Parse(cData.no.ToString().Substring(2)))
                            cardData = cData;
                    }
                }
                break;
            case PlayerType.Major.Cleric:
                {
                    foreach (CardData cData in DataBase.instance.clericCardData)
                    {
                        if (weaponType == int.Parse(cData.no.ToString().Substring(0, 2)) && indexNumber == int.Parse(cData.no.ToString().Substring(2)))
                            cardData = cData;
                    }
                }
                break;
        }


        if (cardData.description.Contains("회복"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = playerUI.boundCharacter.intelligence;
        }
        else if (cardData.description.Contains("마법") && cardData.description.Contains("물리"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = playerUI.boundCharacter.intelligence;
        } 
        else if (cardData.description.Contains("마법"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = playerUI.boundCharacter.intelligence;
        }
        else
        {
            MainStaus = StatusType.Strength;
            mainStatus = playerUI.boundCharacter.strength;
        }   
        SetCardAction();
    }

    void SetCardAction()
    {
        cardAction = null;
        cardAction += () => GetComponent<CardUI>().TransferUI();
        cardAction += () => RemoveInHand(playerUI.GetComponent<Deck>());
        cardAction += () => playerUI.ReturnToInstant(gameObject);
        cardAction += () => N_BattleManager.instance.IsAction = true;
        cardAction += () => token_Fail =  BattleUI.instance.RollToken(MainStaus,mainStatus, cardData.token);
        cardAction += () => playerUI.boundCharacter.GetComponent<UnitAnimationControl>().AttackAnimation();
    }
    
    IEnumerator DoCardAction()
    {
        UseCost(playerUI.boundCharacter);
        yield return new WaitUntil(() => costOver || costUsed);
        if(costOver)
        {
            Debug.Log("코스트 부족! \n" + cardData.useCost + " / "+ playerUI.boundCharacter.cost);
            transform.position = GetComponent<CardUI>().defaultPosition;
        }
        else if(costUsed)
        {
            cardAction();
        }
        costOver = false;
        costUsed = false;
    }

    int SimpleTypeSelect()
    {
        string nameSlot = cardData.variableName;
        switch(nameSlot)
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
            case "ShieldOfLight":
                return 14;
            case "ShieldSlam":
                return 15;
            case "Shield":
                return 16;
        }
    }
}
