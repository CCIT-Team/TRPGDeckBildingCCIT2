using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleUI : MonoBehaviour
{
    public Character boundCharacter;
    public Deck boundDeck;

    [SerializeField]
    GameObject cardPrefab;
    Vector2 cardSize = new Vector2(165, 247.5f);    //1920 x 1080

    Queue<GameObject> waitCardInstant = new Queue<GameObject>();
    List<GameObject> cardInstant = new List<GameObject>();
    public GameObject handUI;

    public DeckDisplay deckDisplay;

    bool firstturn = true; //첫 턴 여부 확인

    private void Awake()
    {
        boundDeck = GetComponent<Deck>();

        cardSize.x = Camera.main.pixelWidth / 1920 * cardSize.x;
        cardSize.y = Camera.main.pixelHeight / 1080 * cardSize.y;
        cardPrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(cardSize.x, cardSize.y);
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
            GetComponent<AudioSource>().Play();
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
        for (int i = 0; i < boundDeck.hand.Count; i++)
        {
            cardInstant[i].transform.localPosition = new Vector2((-cardInstant.Count / 2 + i + (cardInstant.Count + 1) % 2 / 2f) * cardSize.x, 0);
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
        foreach (int id in boundCharacter.GetComponent<Character_Card>().cardID)
        {
            boundDeck.deck.Add(int.Parse(id.ToString()));
        }
        boundDeck.deck.RemoveAll(x => x == 0);
        boundDeck.DeckCounter = boundDeck.deck.Count;
        deckDisplay.SetDisplay(boundDeck);
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
                DrawCard(N_BattleManager.instance.startHandCount);
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

    bool deckCorutinRunning = false;
    bool isopen = false;
    Vector2 targetPosition;

    public void ToggleDeck()
    {
        StartCoroutine(ToggleDeckDisplay(!isopen));
    }

    IEnumerator ToggleDeckDisplay(bool open)
    {
        RectTransform deckTransform = deckDisplay.GetComponent<RectTransform>();
        if (open)
            targetPosition = Vector2.zero;
        else
            targetPosition = new Vector3(600, 0);

        isopen = !isopen;

        if (!deckCorutinRunning)
        {
            deckCorutinRunning = true;
            while (deckCorutinRunning)
            {
                yield return new WaitForSeconds(0.1f);
                deckTransform.anchoredPosition = Vector2.Lerp(deckTransform.anchoredPosition, targetPosition, 0.2f);
                if(Mathf.Abs(deckTransform.anchoredPosition.x - targetPosition.x) <= 0.3f) 
                {
                    deckTransform.anchoredPosition = targetPosition;
                    deckCorutinRunning = false;
                    break;
                }
            }
        }
    }
}