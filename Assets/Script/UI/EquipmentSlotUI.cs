using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryUI rootObject;
    private List<int> weaponCard = new List<int>();
    private int[] stats = new int[] { 0, 0, 0, 0 };
    private bool isHave = false;

    public int playerNum;
    public int no;
    public string itemName;
    public WeaponData.Type type;
    public WeaponData.EquipType equipType;
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
        isHave = true;
        no = weapon.no;
        itemName = weapon.name;
        type = weapon.type;
        equipType = weapon.equipType;
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
    public int[] GetStats()
    {
        int index = 0;
        stats[index++] = strength;
        stats[index++] = intelligence;
        stats[index++] = luck;
        stats[index++] = speed;

        return stats;
    }
    public List<int> GetWeaponCard()
    {
        int cardCount = 0;
        cardCount = card1Count;
        for (int j = 0; j < cardCount; j++)
        {
            weaponCard.Add(card1);
        }
        for (int j = cardCount; j < cardCount + card2Count; j++)
        {
            weaponCard.Add(card2);
        }
        for (int j = cardCount; j < cardCount + card3Count; j++)
        {
            weaponCard.Add(card3);
        }
        for (int j = cardCount; j < cardCount + card4Count; j++)
        {
            weaponCard.Add(card4);
        }
        for (int j = cardCount; j < cardCount + card5Count; j++)
        {
            weaponCard.Add(card5);
        }
        for (int j = cardCount; j < cardCount + card6Count; j++)
        {
            weaponCard.Add(card6);
        }
        for (int j = cardCount; j < cardCount + card7Count; j++)
        {
            weaponCard.Add(card7);
        }
        for (int j = cardCount; j < cardCount + card8Count; j++)
        {
            weaponCard.Add(card8);
        }

        return weaponCard;
    }

    public void OnPointerClick(PointerEventData evenData)
    {
        if (evenData.button == PointerEventData.InputButton.Right)
        {
            if (isHave)
            {
                Debug.Log("탈착");
                UnEquip();
            }
        }
    }
    public void OnPointerEnter(PointerEventData pointerEvent)
    {
        Debug.Log("올라옴");
    }

    public void OnPointerExit(PointerEventData pointerEvent)
    {
        Debug.Log("나감");
    }

    public void UnEquip()
    {
        Debug.Log("교체");
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = Color.clear;
        rootObject.SetInvenItem(no,1);
        rootObject.likedPlayer.GetComponent<Character_Equipment>().isRightWeapon = false;
        rootObject.likedPlayer.GetComponent<Character_Equipment>().DiscardStat(stats);
        rootObject.likedPlayer.GetComponent<Character_Equipment>().DiscardCard(weaponCard);
        ClearData();
    }

    private void ClearData()
    {
        isHave = false;
        no = 0;
        itemName = null;
        type = WeaponData.Type.None;
        equipType = WeaponData.EquipType.None;
        grade = Grade.none;
        level = 0;
        strength = 0;
        intelligence = 0;
        luck = 0;
        speed = 0;

        card1 = 0;
        card1Count = 0;
        card2 = 0;
        card2Count = 0;
        card3 = 0;
        card3Count = 0;
        card4 = 0;
        card4Count = 0;
        card5 = 0;
        card5Count = 0;
        card6 = 0;
        card6Count = 0;
        card7 = 0;
        card7Count = 0;
        card8 = 0;
        card8Count = 0;

        buygold = 0;
        sellgold = 0;

        int index = 0;
        stats[index++] = 0;
        stats[index++] = 0;
        stats[index++] = 0;
        stats[index++] = 0;

        weaponCard.Clear();
    }

}
