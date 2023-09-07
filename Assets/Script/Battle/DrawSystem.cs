using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour
{
    public List<string> Deck;
    public int cardCount;

    List<Card> deck;
    List<Card> hand;
    List<Card> grave;
    float startHandCount = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
            //DrawCard(Deck,cardCount);
    }

    void EnterBattle(List<Card> inputDeck)
    {
        for(int i = 0;i < startHandCount; i++)
        {
            int cardNumber = Random.Range(0, deck.Count);
            hand.Add(deck[cardNumber]);
            deck.RemoveAt(cardNumber);
        }
    }

    void DrawCard(List<Card> inputDeck,int drawCount)
    {
        for (int i = 0; i < drawCount; i++)
        {
            int cardNumber = Random.Range(0, deck.Count);
            hand.Add(deck[cardNumber]);
            deck.RemoveAt(cardNumber);
        }
    }

    void CheckHand(int handlimit = 7)
    {
        if(hand.Count >= handlimit)
        {

        }
    }
}
