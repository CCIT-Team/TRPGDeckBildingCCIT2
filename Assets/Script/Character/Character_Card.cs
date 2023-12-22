using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Card : MonoBehaviour
{
    [SerializeField] public int playerNum;
    public List<int> cardID = new List<int>();

    private string insertQuery;
    public void SetUnitCard(PlayerCard card)
    {
        for (int i = 0; i < card.no.Length; i++)
        {
            cardID.Add(0);
        }

        playerNum = card.playerNum;
        for(int i = 0; i < card.no.Length; i++)
        {
            cardID[i] = card.no[i];
        }
    }

    public void ChangeCard(int[] _deleteCardID, int[] _addCardID )
    {
        //DeleteCard(_deleteCardID);
        //AddCardData(_addCardID);
    }

    public void DeleteCard(List<int> _cardList)
    {
        int index = 0;
        for(int i = 0; i < cardID.Count; i++)
        {
            if(cardID[i] == _cardList[index])
            {
                cardID[i] = 0;
                index++;
                i = 0;
            }

            if (index >= _cardList.Count)
                break;
        }
        cardID.Sort(new Comparison<int>((n1, n2) => n2.CompareTo(n1))); //내림차순 정렬
    }

    public void AddCardData(List<int> _cardList)
    {
        for (int i = 0; i < cardID.Count; i++)
        {
            if(cardID[i] == 0)
            {
                for(int j = 0; j < _cardList.Count; j++)
                {
                    cardID[i + j] = _cardList[j];
                }
                break; 
            }
        }
    }

    public string GetCardDBQuery()
    {
        Debug.Log(cardID.Count);
        string query = "INSERT INTO Deck (playerNum";
        for (int i = 1; i < cardID.Count+1; i++)
        {
            query += ", no" +i.ToString();
        }
        query += ") VALUES (" + GetComponent<Character_type>().playerNum;

        for (int i = 0; i < cardID.Count; i++)
        {
            query += ", " + cardID[i];
        }
        query += ")";
        insertQuery = query;
        return insertQuery;
    }

}
