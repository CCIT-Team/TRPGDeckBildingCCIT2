using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour //��ο�, ����, �� ���� �� ��,ī�� ���� �Լ� ����
{
    Deck playerDeck;
    public int cardCount;
    int startHandCount = 5;
    [SerializeField]
    List<Card> cards;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void EnterBattle(Deck inputDeck = null)
    {
        playerDeck = inputDeck;
        DrawCard(startHandCount);
    }

    public void EndBattle(Deck inputDeck)
    {
        playerDeck = inputDeck;
        playerDeck.deck.AddRange(playerDeck.hand);
        playerDeck.hand.Clear();
        playerDeck.deck.AddRange(playerDeck.grave);
        playerDeck.grave.Clear();
    }    

    public void DrawCard(int drawCount = 1)
    {
        for (int i = 0; i < drawCount; i++)
        {
            int cardNumber = Random.Range(0, playerDeck.deck.Count);
            playerDeck.hand.Add(playerDeck.deck[cardNumber]);
            playerDeck.deck.RemoveAt(cardNumber);
        }
    }

    void CheckHand(int handlimit = 7)
    {
        if(playerDeck.hand.Count >= handlimit)
        {

        }
    }
}
