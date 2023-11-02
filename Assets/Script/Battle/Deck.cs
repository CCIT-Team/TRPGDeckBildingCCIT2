using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //�� ������ �����ϰ� �÷��̾��� ��񿡼� ī�带 Ȯ���� �����ϴ� ����
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
            if (deckCounter == 0)
            {
                deck.AddRange(grave);
                grave.Clear();
            }
        }
    }
}
