using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Character_Equipment : MonoBehaviour
{
    public bool isHelmet;
    public bool isArmor;
    public bool isLeftWeapon;
    public bool isRightWeapon;
    public bool[] isJewel = new bool[4];
    public GameObject[] equipments = new GameObject[8];

    private void Start()
    {
        isHelmet = false;
        isArmor = false;
        isLeftWeapon = false;
        isRightWeapon = false;
        isJewel[0] = false;
        isJewel[1] = false;
        isJewel[2] = false;
        isJewel[3] = false;
    }

    public void AddStat(int[] _weaponStsts)
    {
        int index = 0;
        GetComponent<Character>().strength += _weaponStsts[index++];
        GetComponent<Character>().intelligence += _weaponStsts[index++];
        GetComponent<Character>().luck += _weaponStsts[index++];
        GetComponent<Character>().speed += _weaponStsts[index++];
    }
    public void AddDeck(List<int> _weaponCard)
    {
        GetComponent<Character_Card>().AddCardData(_weaponCard);
    }

    public void DiscardStat(int[] _weaponStsts)
    {
        int index = 0;
        GetComponent<Character>().strength -= _weaponStsts[index++];
        GetComponent<Character>().intelligence -= _weaponStsts[index++];
        GetComponent<Character>().luck -= _weaponStsts[index++];
        GetComponent<Character>().speed -= _weaponStsts[index++];
    }

    public void DiscardCard(List<int> _weaponCard)
    {
        GetComponent<Character_Card>().DeleteCard(_weaponCard);
    }

}