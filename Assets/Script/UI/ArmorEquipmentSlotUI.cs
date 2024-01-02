using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ArmorEquipmentSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryUI rootObject;
    private List<int> armorCard = new List<int>();
    private int[] stats = new int[] { 0, 0, 0, 0 };
    private bool isHave = false;

    public int playerNum;
    public int no;
    public string itemName;
    public ArmorData.Type amorType;
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

    public int buygold;
    public int sellgold;

    public TMP_Text cardCount;
    private void Start()
    {
        cardCount.text = "x0";
    }
    public void SetArmorType(ArmorData armor)
    {
        //playernum 플레이어연결하고 값 넣기
        isHave = true;
        no = armor.no;
        itemName = armor.name;
        amorType = armor.type;
        grade = armor.grade;
        level = armor.level;
        strength = armor.strength;
        intelligence = armor.intelligence;
        luck = armor.luck;
        speed = armor.speed;

        card1 = armor.getCard1;
        card1Count = armor.getCard1Count;
        card2 = armor.getCard2;
        card2Count = armor.getCard2Count;
        card3 = armor.getCard3;
        card3Count = armor.getCard3Count;
        card4 = armor.getCard4;
        card4Count = armor.getCard4Count;

        buygold = armor.buyGold;
        sellgold = armor.sellGold;

        int temp = card1Count + card2Count + card3Count + card4Count;
        cardCount.text = "x" + temp.ToString();
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
        rootObject.SetInvenItem(no, 1);
        rootObject.likedPlayer.GetComponent<Character_Equipment>().DiscardStat(stats);
        rootObject.likedPlayer.GetComponent<Character_Equipment>().DiscardCard(armorCard);
        rootObject.UpdateStat();
        if(amorType == ArmorData.Type.Head)
            rootObject.likedPlayer.GetComponent<Character_Equipment>().isHelmet = false;
        else if(amorType == ArmorData.Type.Armor)
            rootObject.likedPlayer.GetComponent<Character_Equipment>().isArmor = false;
        else if (amorType == ArmorData.Type.Jewel)
            rootObject.likedPlayer.GetComponent<Character_Equipment>().isJewel[0] = false;
        ClearData();
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
    public List<int> GetArmorCard()
    {
        int cardCount = 0;
        cardCount = card1Count;
        for (int j = 0; j < cardCount; j++)
        {
            armorCard.Add(card1);
        }
        for (int j = cardCount; j < cardCount + card2Count; j++)
        {
            armorCard.Add(card2);
        }
        for (int j = cardCount; j < cardCount + card3Count; j++)
        {
            armorCard.Add(card3);
        }
        for (int j = cardCount; j < cardCount + card4Count; j++)
        {
            armorCard.Add(card4);
        }

        return armorCard;
    }
    private void ClearData()
    {
        isHave = false;
        no = 0;
        itemName = null;
        amorType = ArmorData.Type.None;
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

        buygold = 0;
        sellgold = 0;

        int index = 0;
        stats[index++] = 0;
        stats[index++] = 0;
        stats[index++] = 0;
        stats[index++] = 0;

        armorCard.Clear();
    }
}
