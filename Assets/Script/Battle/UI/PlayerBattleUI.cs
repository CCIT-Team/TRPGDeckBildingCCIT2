using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBattleUI : MonoBehaviour
{
    public Character boundCharacter;
    public Deck boundDeck;

    [SerializeField]
    GameObject cardPrefab;
    Vector2 cardSize = new Vector2(165, 247.5f);    //1920 x 1080

    Queue<GameObject> waitCardInstant = new Queue<GameObject>();
    public Transform emptyCards;
    List<GameObject> cardInstant = new List<GameObject>();
    public Transform hand;
    public Transform DeckTransform;

    public float radius = 0;
    public float angle = 0;
    public int maxHandCount = 5;

    public DeckDisplay deckDisplay;

    public TMP_Text playerCost;

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
            card = Instantiate(cardPrefab, emptyCards);
            waitCardInstant.Enqueue(card);
            card.SetActive(false);
        }

    }

    private void Update()
    {
        for(int i = 0; i < hand.childCount; i++)
        {
            hand.GetChild(i).localPosition = new Vector2((-hand.childCount / 2 + i + (hand.childCount + 1) % 2 / 2f) * cardSize.x * 0.95f, 0);
            //hand.GetChild(i).localPosition = new Vector2(Mathf.Cos((-hand.childCount / 2 + i + (hand.childCount + 1) % 2 / 2f)*angle + Mathf.PI/2),Mathf.Sin((-hand.childCount / 2 + i + (hand.childCount + 1) % 2 / 2f)*angle + Mathf.PI/2)) * radius;
            //hand.GetChild(i).rotation = Quaternion.Euler(0, 0, (angle));
        }

        playerCost.text = boundCharacter.cost + "/" + boundCharacter.maxCost;
    }

    public void DrawCard(int drawCount = 1)
    {
        if (drawCount > boundDeck.DeckCount + boundDeck.GraveCount)
        {
            drawCount = boundDeck.DeckCount + boundDeck.GraveCount;
        }
        if (boundDeck.HandCount >= N_BattleManager.instance.maxHandCount)
            return;
        StartCoroutine(DrawCoroutine(drawCount));
    }

    public void CostDraw()
    {
        boundCharacter.cost -= 1;
        DrawCard();
    }


    public void ReturnToInstant(GameObject gameObject)
    {
        cardInstant.Remove(gameObject);
        waitCardInstant.Enqueue(gameObject);
        gameObject.GetComponent<CardAnimation>().isdrawn = false;
        gameObject.transform.SetParent(emptyCards);
        gameObject.SetActive(false);
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
            boundDeck.AddCard(int.Parse(id.ToString()));
        }
        boundDeck.OrganizeDeck();
        deckDisplay.SetDeck(boundDeck);
        if (!boundCharacter.isMyturn)
            transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UnBindCharacter()
    {
        boundCharacter = null;
    }

    IEnumerator DrawCoroutine(int drawCount)
    {
        for (int i = 0; i < drawCount; i++)
        {
            if (boundDeck.DeckCount <= 0)
            {
                boundDeck.Refill();
                boundCharacter.Hp -= Mathf.Round(boundCharacter.maxHp / 3);
                if (boundCharacter.Hp <= 0)
                {
                    boundCharacter.GetComponent<UnitAnimationControl>().DeathAnimation();
                }
            }
            GetComponent<AudioSource>().Play();
            int cardID = boundDeck.DrawCard(Random.Range(0, boundDeck.DeckCount));
            GameObject cardParent = new GameObject("card" + (hand.childCount + 1));
            GameObject cardObject;
            cardParent.transform.SetParent(hand);
            if (waitCardInstant.Count > 0)
            {
                cardObject = waitCardInstant.Dequeue();
                cardObject.transform.SetParent(cardParent.transform);
            }
            else
            {
                cardObject = Instantiate(cardPrefab, cardParent.transform);
            }
            cardObject.GetComponent<CardUI>().defaultParent = cardParent.transform;
            cardInstant.Add(cardObject);
            cardObject.GetComponent<N_Card>().playerUI = this;
            cardObject.SetActive(true);
            cardObject.GetComponent<N_Card>().GetCardData(cardID);
            cardObject.GetComponent<CardUI>().DisplayOnUI();
            yield return new WaitUntil(() => cardObject.GetComponent<CardAnimation>().isdrawn);
        }
        if (hand.childCount > maxHandCount)
            N_BattleManager.instance.isHandOver = true;
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