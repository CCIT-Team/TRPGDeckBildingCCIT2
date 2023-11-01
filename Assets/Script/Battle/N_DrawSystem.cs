using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N_DrawSystem : MonoBehaviour
{
    public Character bindedCharacter;
    public Deck bindedDeck;

    [SerializeField]
    GameObject cardPrefab;
    float cardWidth;

    Stack<GameObject> waitCardInstant = new Stack<GameObject>();
    List<GameObject> cardInstant = new List<GameObject>();
    public GameObject handUI;


    void Start()
    {
        cardWidth = cardPrefab.GetComponent<RectTransform>().rect.width;
        GameObject card;
        for (int i = 0; i < 10; i++)
        {
            card = Instantiate(cardPrefab, handUI.transform);
            waitCardInstant.Push(card);
            card.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawCard(int drawCount = 1)
    {
        for (int i = 0; i < drawCount; i++)
        {
            int cardIndex = Random.Range(0, bindedDeck.deck.Count);
            bindedDeck.hand.Add(bindedDeck.deck[cardIndex]);
            bindedDeck.deck.RemoveAt(cardIndex);
            bindedDeck.DeckCounter--;
            if (waitCardInstant.Count > 0)
            {
                cardInstant.Add(waitCardInstant.Pop());
            }
            else
            {
                cardInstant.Add(Instantiate(cardPrefab, handUI.transform));
            }
        }
        CompareHand();
    }

    public void CompareHand()
    {
        for(int i = 0; i< bindedDeck.hand.Count;i++)
        {
            cardInstant[i].transform.localPosition = new Vector2((cardInstant.Count/2 - i - (cardInstant.Count+1) % 2 /2f) * cardWidth, 0);
            cardInstant[i].SetActive(true);
        }
    }
    //-----------------------------------------------
    // UI로 옮겨야 할듯함
    public void Onclick()
    {
        bindedCharacter.isMyturn = false;
    }

    public void BindCharacter(Character character)
    {
        bindedCharacter = character;
        bindedDeck = bindedCharacter.GetComponent<Deck>();
        bindedDeck.BIndUI(this.gameObject);
        CheckTurn();
    }

    void UnBindCharacter()
    {
        bindedCharacter = null;
    }

    public void CheckTurn()
    {
        if (bindedCharacter.isMyturn)
        {
            StartCoroutine(UI_Control());
            DrawCard();
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator UI_Control()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => !bindedCharacter.isMyturn);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
