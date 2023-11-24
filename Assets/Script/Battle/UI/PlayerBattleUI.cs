using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleUI : MonoBehaviour
{
    public Character boundCharacter;
    Deck boundDeck;

    public PlayerStatUI statUI;

    [SerializeField]
    GameObject cardPrefab;
    float[] cardSize = {150, 225};    //1920, 1080

    Queue<GameObject> waitCardInstant = new Queue<GameObject>();
    List<GameObject> cardInstant = new List<GameObject>();
    public GameObject handUI;

    bool firstturn = true; //첫 턴 여부 확인

    private void Awake()
    {
        boundDeck = GetComponent<Deck>();

        cardSize[0] = Camera.main.pixelWidth / 1920 * cardSize[0];
        cardSize[1] = Camera.main.pixelHeight / 1080 * cardSize[1];
        cardPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(cardSize[0], cardSize[1]);
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
        SetHandPosition();
    }

    public void SetHandPosition()
    {
        for(int i = 0; i< boundDeck.hand.Count;i++)
        {
            Debug.Log("i: " + i);
            Debug.Log("CS = " + cardSize[0]);
            Debug.Log("CINS = " + cardInstant[i]);
            cardInstant[i].transform.localPosition = new Vector2((-cardInstant.Count/2 + i + (cardInstant.Count+1) % 2 /2f) * cardSize[0], 0);
        }
    }

    public void ReturnToInstant(GameObject gameObject)
    {
        cardInstant.Remove(gameObject);
        waitCardInstant.Enqueue(gameObject);
        SetHandPosition();
    }

    public void Onclick()
    {
        boundCharacter.isMyturn = false;
    }

    public void BindCharacter(Character character)
    {
        boundCharacter = character;
        name = boundCharacter.name;
        //statUI.character = boundCharacter;
        //statUI.character_Type = boundCharacter.GetComponent<Character_type>();
        statUI.gameObject.SetActive(true);
        foreach(int id in boundCharacter.GetComponent<Character_Card>().cardID)
        {
            boundDeck.deck.Add(int.Parse(id.ToString()));
        }
        boundDeck.deck.RemoveAll(x => x == 0);
        boundDeck.DeckCounter = boundDeck.deck.Count;
        if (!boundCharacter.isMyturn)
            transform.GetChild(0).gameObject.SetActive(false);
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
            if (firstturn)
            {
                DrawCard(5);
                firstturn = false;
            }
            else
                DrawCard();
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitUntil(() => !boundCharacter.isMyturn);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}