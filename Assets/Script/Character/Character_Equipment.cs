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