using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //�� ������ �����ϰ� �÷��̾��� ��񿡼� ī�带 Ȯ���� �����ϴ� ����
{

    public List<int> deck = new List<int>();
    public List<int> hand = new List<int>();
    public List<int> grave = new List<int>();

    int deckCounter;
    public int DeckCounter
    {
        get { return deckCounter; }
        set
        {
            deckCounter = value;
            if (deckCounter <= 0 && deck.Count == 0)
            {
                deck.AddRange(grave);
                grave.Clear();
                deckCounter = deck.Count;
            }
        }
    }
}
