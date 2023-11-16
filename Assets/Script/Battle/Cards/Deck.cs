using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //덱 정보를 소지하고 플레이어의 장비에서 카드를 확인해 저장하는 역할
{

    public List<int> deck;
    public List<int> hand;
    public List<int> grave;

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
