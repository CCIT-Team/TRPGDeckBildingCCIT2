using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class N_Card : MonoBehaviour   //ī�� ������ ȿ�� �Լ��� ���� ��
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
        playerUI.boundCharacter.GetComponent<UnitAnimationControl>().soundindex = SimpleSoundSelect();
        playerUI.boundCharacter.GetComponent<UnitAnimationControl>().particleindex = SimpleParticleSelect();
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
        BattleUI.instance.AddLog(playerUI.boundCharacter.name + "��(��) " + cardTarget.name + "���� " + cardData.name + "��(��) ���");
        var skill = CardSkills.SearchSkill(cardData.variableName);
        skill.Invoke(null, new object[] { playerUI.boundCharacter,          //�����
                                         cardTarget.GetComponent<Unit>(),   //��� ���
                                         finalVlaue,                        //��
                                         cardData.effectUseTurn,            //�߰�ȿ�� ��
                                         token_Fail,                        //���� ��ū ��
                                         cardData.token});                  //�� ��ū ��
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
        //    case 50:    //������
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
        //    case 60:    //������
        //        cardData = DataBase.instance.cardData[indexNumber + N_BattleManager.instance.CardStartIndexOfType[1]];
        //        break;
        //    case 70:    //Ŭ����
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


        if (cardData.description.Contains("ȸ��"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = playerUI.boundCharacter.intelligence;
        }
        else if (cardData.description.Contains("����") && cardData.description.Contains("����"))
        {
            MainStaus = StatusType.Intelligence;
            mainStatus = playerUI.boundCharacter.intelligence;
        } 
        else if (cardData.description.Contains("����"))
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
        cardAction += () => StartCoroutine(BattleUI.instance.RollToken(MainStaus,mainStatus, cardData.token));
        cardAction += () => StartCoroutine(WaitTokenRolling(cardData.token));

    }
    
    IEnumerator DoCardAction()
    {
        UseCost(playerUI.boundCharacter);
        yield return new WaitUntil(() => costOver || costUsed);
        if(costOver)
        {
            Debug.Log("�ڽ�Ʈ ����! \n" + cardData.useCost + " / "+ playerUI.boundCharacter.cost);
            transform.position = GetComponent<CardUI>().defaultPosition;
        }
        else if(costUsed)
        {
            cardAction();
        }
        costOver = false;
        costUsed = false;
    }

    IEnumerator WaitTokenRolling(int token)
    {
        yield return new WaitForSeconds(token * 0.5f);
        token_Fail = BattleUI.instance.faildTokens;
        playerUI.boundCharacter.GetComponent<UnitAnimationControl>().AttackAnimation();
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
            //----------------------------------
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
            //----------------------------------
            case "ShieldOfLight":
                return 14;
            case "ShieldSlam":
                return 15;
            case "Shield":
                return 16;
            case "Blessoflight":
                return 17;
            case "SwingAttack":
                return 3;
            case "Pillaroflight":
                return 17;
            case "Judgment":
                return 8;
            case "HolyArrow":
                return 5;
            case "HolyFlame":
                return 5;
            case "Pain":
                return 5;
            case "Blessofprotect":
                return 17;
            case "LightAround":
                return 17;
            case "Contemplation":
                return 4;
            case "BlessofStrength":
                return 17;
            case "Pray":
                return 17;
            case "BlessofIntelligence":
                return 17;
            case "Attack":
                return 11;
        }
    }

    int SimpleSoundSelect()
    {
        string nameSlot = cardData.variableName;
        switch (nameSlot)
        {
            default:
                return 0;
                //----------------------------------
            case "ShootingStar":
                return 19;
            case "ManaShower":
                return 18;
            case "ManaBomb":
                return 18;
            case "BurningFlame":
                return 13;
            case "DoubleCharm":
                return 20;
            case "ManaCirculation":
                return 20;
            case "ManaAmplification":
                return 20;
            case "Agility":
                return 21;
            case "Scheme":
                return 20;
            case "FlameArmor":
                return 14;
            case "Ignition":
                return 18;
            case "FlameofDragon":
                return 16;
            case "FireBall":
                return 11;
            case "IceBall":
                return 22;
            case "ThunderBolt":
                return 24;
            case "ManaBullet":
                return 18;
            case "Flamethrower":
                return 12;
                //-------------------------------
            case "ShieldOfLight":
                return 11;
            case "ShieldSlam":
                return 11;
            case "Shield":
                return 11;
            case "Blessoflight":
                return 14;
            case "SwingAttack":
                return 19;
            case "Pillaroflight":
                return 14;
            case "Judgment":
                return 17;
            case "HolyArrow":
                return 14;
            case "HolyFlame":
                return 18;
            case "Pain":
                return 15;
            case "Blessofprotect":
                return 14;
            case "LightAround":
                return 14;
            case "Contemplation":
                return 14;
            case "BlessofStrength":
                return 14;
            case "Pray":
                return 14;
            case "BlessofIntelligence":
                return 14;
            case "Attack":
                return 12;
        }
    }

    int SimpleParticleSelect()
    {
        string nameSlot = cardData.variableName;
        switch (nameSlot)
        {
            default:
                return 0;
            //----------------------------------
            case "ShootingStar":
                return 14;
            case "ManaShower":
                return 12;
            case "ManaBomb":
                return 10;
            case "BurningFlame":
                return 1;
            case "DoubleCharm":
                return 13;
            case "ManaCirculation":
                return 13;
            case "ManaAmplification":
                return 13;
            case "Agility":
                return 0;
            case "Scheme":
                return 13;
            case "FlameArmor":
                return 4;
            case "Ignition":
                return 8;
            case "FlameofDragon":
                return 5;
            case "FireBall":
                return 3;
            case "IceBall":
                return 7;
            case "ThunderBolt":
                return 15;
            case "ManaBullet":
                return 11;
            case "Flamethrower":
                return 6;
            //----------------------------------
            case "ShieldOfLight":
                return 3;
            case "ShieldSlam":
                return 2;
            case "Shield":
                return 8;
            case "Blessoflight":
                return 3;
            case "SwingAttack":
                return 9;
            case "Pillaroflight":
                return 7;
            case "Judgment":
                return 5;
            case "HolyArrow":
                return 3;
            case "HolyFlame":
                return 4;
            case "Pain":
                return 6;
            case "Blessofprotect":
                return 3;
            case "LightAround":
                return 3;
            case "Contemplation":
                return 3;
            case "BlessofStrength":
                return 3;
            case "Pray":
                return 3;
            case "BlessofIntelligence":
                return 3;
            case "Attack":
                return 2;
        }
    }
}
