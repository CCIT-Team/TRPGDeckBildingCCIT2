using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleUI : MonoBehaviour
{
    public Character boundCharacter;
    Deck boundDeck;

    [SerializeField]
    GameObject cardPrefab;
    float cardWidth;

    Queue<GameObject> waitCardInstant = new Queue<GameObject>();
    List<GameObject> cardInstant = new List<GameObject>();
    public GameObject handUI;

    private void Awake()
    {
        boundDeck = GetComponent<Deck>();
        cardWidth = cardPrefab.GetComponent<RectTransform>().rect.width;
    }

    void Start()
    {
        GameObject card;
        for (int i = 0; i < 4; i++)
        {
            card = Instantiate(cardPrefab, handUI.transform);
            waitCardInstant.Enqueue(card);
            card.SetActive(false);
        }
        
    }

    public void DrawCard(int drawCount = 1)
    {
        if (drawCount > boundDeck.deck.Count + boundDeck.grave.Count)
            return;
        for (int i = 0; i < drawCount; i++)
        {
            int cardIndex = Random.Range(0, boundDeck.deck.Count);
            int cardID = boundDeck.deck[cardIndex];
            boundDeck.hand.Add(cardID);
            boundDeck.deck.RemoveAt(cardIndex);
            boundDeck.DeckCounter--;
            GameObject cardObject;
            if (waitCardInstant.Count > 0)
            {
                cardObject = waitCardInstant.Dequeue();
                cardInstant.Add(cardObject);
            }
            else
            {
                cardObject = Instantiate(cardPrefab, handUI.transform);
                cardInstant.Add(cardObject);
            }
            cardObject.GetComponent<N_Card>().playerUI = this;
            cardObject.SetActive(true);
            cardObject.GetComponent<N_Card>().GetCardData(cardID);
            cardObject.GetComponent<CardUI>().DisplayOnUI();
        }
        CompareHand();
    }

    public void CompareHand()
    {
        for(int i = 0; i< boundDeck.hand.Count;i++)
        {
            cardInstant[i].transform.localPosition = new Vector2((cardInstant.Count/2 - i - (cardInstant.Count+1) % 2 /2f) * cardWidth, 0);
        }
    }

    public void ReturnToInstant(GameObject gameObject)
    {
        cardInstant.Remove(gameObject);
        waitCardInstant.Enqueue(gameObject);
    }

    public void Onclick()
    {
        boundCharacter.isMyturn = false;
    }

    public void BindCharacter(Character character)
    {
        boundCharacter = character;
        name = boundCharacter.name;
        boundDeck.deck = boundCharacter.GetComponent<Character_Card>().cardID;
        boundDeck.deck.RemoveAll(x => x == 0);
        boundDeck.DeckCounter = boundDeck.deck.Count;
    }

    public void UnBindCharacter()
    {
        boundCharacter = null;
    }

    public IEnumerator ActIfTurn()
    {
        if (boundCharacter.isMyturn)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            DrawCard();
        }  
        yield return new WaitUntil(() => !boundCharacter.isMyturn);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}