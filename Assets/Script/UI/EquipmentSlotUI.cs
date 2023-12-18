using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlotUI : MonoBehaviour
{
    public int playerNum;
    public int no;
    public string itemName;
    public WeaponData.Type type;
    public Grade grade;
    public int level;
    public int strength;
    public int intelligence;
    public int luck;
    public int speed;

    public int card1;
    public int card1Count;
    public int card2;
    public int card2Count;
    public int card3;
    public int card3Count;
    public int card4;
    public int card4Count;
    public int card5;
    public int card5Count;
    public int card6;
    public int card6Count;
    public int card7;
    public int card7Count;
    public int card8;
    public int card8Count;

    public int buygold;
    public int sellgold;

    public void SetWeaponType(WeaponData weapon)
    {
        //playernum 플레이어연결하고 값 넣기
        no = weapon.no;
        itemName = weapon.name;
        type = weapon.type;
        grade = weapon.grade;
        level = weapon.level;
        strength = weapon.strength;
        intelligence = weapon.intelligence;
        luck = weapon.luck;
        speed = weapon.speed;

        card1 = weapon.getCard1;
        card1Count = weapon.getCard1Count;
        card2 = weapon.getCard2;
        card2Count = weapon.getCard2Count;
        card3 = weapon.getCard3;
        card3Count = weapon.getCard3Count;
        card4 = weapon.getCard4;
        card4Count = weapon.getCard4Count;
        card5 = weapon.getCard5;
        card5Count = weapon.getCard5Count;
        card6 = weapon.getCard6;
        card6Count = weapon.getCard6Count;
        card7 = weapon.getCard7;
        card7Count = weapon.getCard7Count;
        card8 = weapon.getCard8;
        card8Count = weapon.getCard8Count;

        buygold = weapon.buyGold;
        sellgold = weapon.sellGold;
    }

}
