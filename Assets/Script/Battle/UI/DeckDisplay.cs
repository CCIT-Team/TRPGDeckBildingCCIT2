using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckDisplay : MonoBehaviour
{
    public GameObject cardPreFab;
    public Deck boundDeck;

    List<GameObject> deckInventory = new List<GameObject>();
    List<GameObject> handInventory = new List<GameObject>();
    List<GameObject> graveInventory = new List<GameObject>();

    List<int> deckCards;
    List<int> handCards;
    List<int> graveCards;

    public  List<Button> inventoryButtons = new();
    public List<GameObject> cardTypes = new();
    List<int> typeCount = new() { 0,0,0,0,0 };

    public Transform inventory;
    public Transform emptyCards;

    bool inventoryOn = true;
    bool clearInventory = false;


    void Awake()
    {
        inventory.transform.SetParent(transform.GetChild(3).GetChild(0));
        inventory.transform.localPosition = new Vector3(0, 0, 0);
        ToggleDisplay();
    }

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    void ChangeInteractable(int targetButton)
    {
        for (int i = 0; i < inventoryButtons.Count; i++)
        {
            inventoryButtons[i].interactable = true;
        }
        inventoryButtons[targetButton].interactable = false;
    }

    void SetDeck(Deck deck)
    {
        boundDeck = deck;
        deckCards = deck.GetDeckList();
        handCards = deck.GetHandList();
        graveCards = deck.GetGraveList();
        //SetDisplay();
    }

    public void ToggleDisplay()
    {
        if(inventoryOn)
        {
            resetInventory();
            inventoryOn = false;
            gameObject.SetActive(false);
        }
        else
        {
            inventoryOn = true;
            foreach (PlayerBattleUI playerBattleUI in BattleUI.instance.playerUI)
            {
                if (playerBattleUI.boundCharacter == BattleUI.instance.playerBar.linkedPlayerStat && (boundDeck = playerBattleUI.boundDeck))
                {
                    SetDeck(boundDeck);
                    DisplayDeck();
                }
            }
        }
    }

    void CountType(List<GameObject> Cards)
    {
        foreach (GameObject card in Cards)
        {
            switch(card.GetComponent<DeckCardUI>().cardData.attackType)
            {
                case CardData.AttackType.Attack:
                    cardTypes[0].SetActive(true);
                    typeCount[0] += 1;
                    break;
                case CardData.AttackType.Defence:
                    cardTypes[1].SetActive(true);
                    typeCount[1] += 1;
                    break;
                case CardData.AttackType.CardDraw:
                    cardTypes[2].SetActive(true);
                    typeCount[2] += 1;
                    break;
                case CardData.AttackType.Increase:
                    cardTypes[3].SetActive(true);
                    typeCount[3] += 1;
                    break;
                case CardData.AttackType.Endow:
                    cardTypes[4].SetActive(true);
                    typeCount[4] += 1;
                    break;
            }
        }
        for (int i = 0; i < cardTypes.Count; i++)
        {
            cardTypes[i].transform.GetChild(0).GetComponent<TMP_Text>().text = "X " + typeCount[i];
        }

    }

    public void resetInventory()
    {
        deckInventory.Clear();
        handInventory.Clear();
        graveInventory.Clear();
        GameObject card;
        int count = inventory.childCount;
        for (int i = 0; i < count; i++)
        {
            card = inventory.GetChild(0).gameObject;
            card.GetComponent<DeckCardUI>().ResetCardUI();
            card.SetActive(false);
            card.transform.SetParent(emptyCards);
        }
        for (int i = 0; i < cardTypes.Count; i++)
        {
            typeCount[i] = 0;
            cardTypes[i].transform.GetChild(0).GetComponent<TMP_Text>().text = null;
            cardTypes[i].SetActive(false);
        }
        clearInventory = true;
    }

    public void DisplayDeck()
    {
        ChangeInteractable(0);
        resetInventory();
        while (!clearInventory)
        {
            
        }
        clearInventory = false;
        GameObject card;
        for (int i = 0; i < deckCards.Count; i++)
        {
            if (deckInventory.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == deckCards[i]))
            {
                deckInventory.Find(x => x.GetComponent<DeckCardUI>().cardData.no == deckCards[i]).GetComponent<DeckCardUI>().SetAmount(deckCards.FindAll(x => x == deckCards[i]).Count);
            }
            else
            {
                if(emptyCards.childCount > 0)
                {
                    card = emptyCards.GetChild(0).gameObject;
                    card.transform.SetParent(inventory);
                    card.SetActive(true);
                }
                else
                {
                    card = Instantiate(cardPreFab, inventory); 
                }
                card.GetComponent<DeckCardUI>().LoadCardData(deckCards[i]);
                card.GetComponent<DeckCardUI>().SetAmount(deckCards.FindAll(x => x == deckCards[i]).Count);
                deckInventory.Add(card);
            }
        }
    }

    public void DisplayHand()
    {
        ChangeInteractable(1);
        resetInventory();
        while (!clearInventory)
        {
            
        }
        clearInventory = false;
        GameObject card;
        for (int i = 0; i < handCards.Count; i++)
        {
            if (handInventory.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == handCards[i]))
            {
                handInventory.Find(x => x.GetComponent<DeckCardUI>().cardData.no == handCards[i]).GetComponent<DeckCardUI>().SetAmount(handCards.FindAll(x => x == handCards[i]).Count);
            }
            else
            {
                if (emptyCards.childCount > 0)
                {
                    card = emptyCards.GetChild(0).gameObject;
                    card.transform.SetParent(inventory);
                    card.SetActive(true);
                }
                else
                {
                    card = Instantiate(cardPreFab, inventory);
                }
                card.GetComponent<DeckCardUI>().LoadCardData(handCards[i]);
                card.GetComponent<DeckCardUI>().SetAmount(handCards.FindAll(x => x == handCards[i]).Count);
                handInventory.Add(card);
            }
        }
    }

    public void DisplayGrave()
    {
        ChangeInteractable(2);
        resetInventory();
        while (!clearInventory )
        {
            
        }
        clearInventory = false;
        GameObject card;
        for (int i = 0; i < graveCards.Count; i++)
        {
            if (graveInventory.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == graveCards[i]))
            {
                graveInventory.Find(x => x.GetComponent<DeckCardUI>().cardData.no == graveCards[i]).GetComponent<DeckCardUI>().SetAmount(graveCards.FindAll(x => x == graveCards[i]).Count);
            }
            else
            {
                if (emptyCards.childCount > 0)
                {
                    card = emptyCards.GetChild(0).gameObject;
                    card.transform.SetParent(inventory);
                    card.SetActive(true);
                }
                else
                {
                    card = Instantiate(cardPreFab, inventory);
                }
                card.GetComponent<DeckCardUI>().LoadCardData(graveCards[i]);
                card.GetComponent<DeckCardUI>().SetAmount(graveCards.FindAll(x => x == graveCards[i]).Count);
                graveInventory.Add(card);
            }
        }
    }
}
