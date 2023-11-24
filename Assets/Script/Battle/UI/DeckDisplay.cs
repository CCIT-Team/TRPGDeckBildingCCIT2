using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    public GameObject cardPreFab_D;
    List<GameObject> displayDeck = new List<GameObject>();

    public Transform deckTransform;

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

    public void SetDisplay(Deck deck)
    {
        GameObject card;
        foreach(int i in deck.deck)
        {
            if(displayDeck.Exists(x => x.GetComponent<DeckCardUI>().cardData.no == i))
            {
                displayDeck.Find(x => x.GetComponent<DeckCardUI>().cardData.no == i).GetComponent<DeckCardUI>().AddAmount(1);
            }
            else
            {
                card = Instantiate(cardPreFab_D, deckTransform);
                //직업덱 사라지면 수정
                card.GetComponent<DeckCardUI>().LoadCardData(i, transform.parent.parent.GetComponent<PlayerBattleUI>().boundCharacter.GetComponent<Character_type>().major);
                displayDeck.Add(card);
            }
            //deckTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(0,80 + 255 * transform.childCount);
        }

    }
}
