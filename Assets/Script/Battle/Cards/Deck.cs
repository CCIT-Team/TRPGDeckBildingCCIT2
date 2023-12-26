using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //�� ������ �����ϰ� �÷��̾��� ��񿡼� ī�带 Ȯ���� �����ϴ� ����
{
    List<int> deck = new List<int>();
    List<int> hand = new List<int>();
    List<int> grave = new List<int>();

    public int DeckCount { get { return deck.Count; } }
    public int HandCount { get { return hand.Count; } }
    public int GraveCount { get { return grave.Count; } }

    public int DrawCard(int index)
    {
        int id;
        id = deck[index];
        hand.Add(id);
        deck.RemoveAt(index);
        return id;
    }

    public void HandToGrave(int id)
    {
        hand.Remove(id);
        grave.Add(id);
    }

    public int FindCard(int id)
    {
        int index = deck.Find(x => x == id);
        return index;
    }

    public int GetCard(int index)
    {
        int id = deck[index];
        return id;
    }

    public void OrganizeDeck()
    {
        deck.RemoveAll(x => x == 0);
    }

    public void AddCard(int id)
    {
        deck.Add(id);
    }

    public void Refill()
    {
        deck.AddRange(grave);
        grave.Clear();
    }
}
