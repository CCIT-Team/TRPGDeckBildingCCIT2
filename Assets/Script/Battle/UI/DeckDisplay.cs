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
                displayDeck.Find(x => x.GetComponent<DeckCardUI>().cardData.no == deckCards[i]).GetComponent<DeckCardUI>().AddAmount(1);
            }
            else
            {
                card = Instantiate(cardPreFab_D, deckTransform);
                //직업덱 사라지면 수정
                card.GetComponent<DeckCardUI>().LoadCardData(deckCards[i], transform.parent.parent.GetComponent<PlayerBattleUI>().boundCharacter.GetComponent<Character_type>().major);
                displayDeck.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }
        for (int i = 0; i < boundDeck.HandCount; i++)
        {
            if (displayHand.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == handCards[i]))
            {
                displayHand.Find(x => x.GetComponent<DeckCardUI>().cardData.no == handCards[i]).GetComponent<DeckCardUI>().AddAmount(1);
            }
            else
            {
                card = Instantiate(cardPreFab_D, handTransform);
                //직업덱 사라지면 수정
                card.GetComponent<DeckCardUI>().LoadCardData(handCards[i], transform.parent.parent.GetComponent<PlayerBattleUI>().boundCharacter.GetComponent<Character_type>().major);
                displayHand.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }
        for (int i = 0; i < boundDeck.GraveCount; i++)
        {
            if (displayGrave.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == graveCards[i]))
            {
                displayGrave.Find(x => x.GetComponent<DeckCardUI>().cardData.no == graveCards[i]).GetComponent<DeckCardUI>().AddAmount(1);
            }
            else
            {
                card = Instantiate(cardPreFab_D, graveTransform);
                //직업덱 사라지면 수정
                card.GetComponent<DeckCardUI>().LoadCardData(graveCards[i], transform.parent.parent.GetComponent<PlayerBattleUI>().boundCharacter.GetComponent<Character_type>().major);
                displayGrave.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }
    }
}
