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
        DeleteCard(_deleteCardID);
        AddCardData(_addCardID);
    }

    public void DeleteCard(int[] _cardID)
    {
        for(int i = 0; i < _cardID.Length; i++)
            cardID.Remove(_cardID[i]);
    }

    public void AddCardData(int[] _cardID)
    {
        cardID.AddRange(_cardID);
    }

    public string GetCardDBQuery()
    {
        string query = "INSERT INTO Deck playerNum";
        for (int i = 1; i < cardID.Count+1; i++)
        {
            query += ", no" +i.ToString();
        }
        query += " VALUES " + GetComponent<Character_type>().playerNum;

        for (int i = 1; i < cardID.Count + 1; i++)
        {
            query += ", " + cardID[i];
        }
        query += ")";
        insertQuery = query;
        return insertQuery;
    }

}
