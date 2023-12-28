using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    public GameObject cardPreFab_D;
    List<GameObject> displayDeck = new List<GameObject>();
    List<GameObject> displayHand = new List<GameObject>();
    List<GameObject> displayGrave = new List<GameObject>();
    public Deck boundDeck;
    List<int> deckCards;
    List<int> handCards;
    List<int> graveCards;

    public Transform deckTransform;
    public Transform handTransform;
    public Transform graveTransform;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo()
    {

    }

    public void SetDeck(Deck deck)
    {
        boundDeck = deck;
        deckCards = deck.GetDeckList();
        handCards = deck.GetHandList();
        graveCards = deck.GetGraveList();
        SetDisplay();
    }

    public void SetDisplay()
    {
        GameObject card;
        for(int i = 0; i < boundDeck.DeckCount; i++) 
        {
            if(displayDeck.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == deckCards[i]))
            {
                displayDeck.Find(x => x.GetComponent<DeckCardUI>().cardData.no == deckCards[i]).GetComponent<DeckCardUI>().SetAmount(deckCards.FindAll(x => x == deckCards[i]).Count);
            }
            else
            {
                card = Instantiate(cardPreFab_D, deckTransform);
                card.GetComponent<DeckCardUI>().LoadCardData(deckCards[i]);
                card.GetComponent<DeckCardUI>().SetAmount(deckCards.FindAll(x => x == deckCards[i]).Count);
                displayDeck.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }
        for (int i = 0; i < boundDeck.HandCount; i++)
        {
            if (displayHand.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == handCards[i]))
            {
                displayHand.Find(x => x.GetComponent<DeckCardUI>().cardData.no == handCards[i]).GetComponent<DeckCardUI>().SetAmount(handCards.FindAll(x => x == handCards[i]).Count);
            }
            else
            {
                card = Instantiate(cardPreFab_D, handTransform);
                card.GetComponent<DeckCardUI>().LoadCardData(handCards[i]);
                card.GetComponent<DeckCardUI>().SetAmount(handCards.FindAll(x => x == handCards[i]).Count);
                displayHand.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }
        for (int i = 0; i < boundDeck.GraveCount; i++)
        {
            if (displayGrave.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == graveCards[i]))
            {
                displayGrave.Find(x => x.GetComponent<DeckCardUI>().cardData.no == graveCards[i]).GetComponent<DeckCardUI>().SetAmount(graveCards.FindAll(x => x == graveCards[i]).Count);
            }
            else
            {
                card = Instantiate(cardPreFab_D, graveTransform);
                card.GetComponent<DeckCardUI>().LoadCardData(graveCards[i]);
                card.GetComponent<DeckCardUI>().SetAmount(graveCards.FindAll(x => x == graveCards[i]).Count);
                displayGrave.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }
    }
}
