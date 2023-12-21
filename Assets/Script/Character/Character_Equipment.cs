using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Character_Equipment : MonoBehaviour
{
    public bool isRightWeapon;
    public List<int> rightWeaponCard = new List<int>();
    public GameObject[] equipments = new GameObject[8];
    
    public Transform[] position = new Transform[4];

    private void Start()
    {
        isRightWeapon = false;
    }

    public void AddDeck(WeaponData _weaponNo)
    {
        int cardCount = 0;
        cardCount = _weaponNo.getCard1Count;
        Debug.Log(cardCount);
        for (int j = 0; j < cardCount; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard1);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard2Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard2);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard3Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard3);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard4Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard4);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard5Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard5);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard6Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard6);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard7Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard7);
        }
        for (int j = cardCount; j < cardCount + _weaponNo.getCard8Count; j++)
        {
            rightWeaponCard.Add(_weaponNo.getCard8);
        }


        GetComponent<Character_Card>().AddCardData(rightWeaponCard);
    }
}