using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSystem : MonoBehaviour //드로우, 셔플, 패 갱신 등 덱,카드 관련 함수 모음
{
    Deck playerDeck;
    public int cardCount;
    int startHandCount = 5;
    [SerializeField]
    List<Card> cards;

    [SerializeField]
    GameObject CardPrefab;

    Stack<GameObject> CardStack;

    public GameObject CardUI;

    void Start()
    {
        CardStack = new Stack<GameObject>();
        GameObject card;
        for (int i = 0; i < 10; i++)
        {
            card = Instantiate(CardPrefab,CardUI.transform);
            CardStack.Push(card);
            card.SetActive(false);
        }
        Debug.Log(CardStack.Count);
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
            if (CardStack.Count > 0)
            {
                cards.Add(CardStack.Pop().GetComponent<Card>());
                cards[cards.Count-1].transform.localPosition = new Vector3((cards.Count*125-675),0);
                cards[cards.Count - 1].gameObject.SetActive(true);
            }
        }
    }

    void CheckHand(int handlimit = 7)
    {
        if(playerDeck.hand.Count >= handlimit)
        {

        }
    }
}
