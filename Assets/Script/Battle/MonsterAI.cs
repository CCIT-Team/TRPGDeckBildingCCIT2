using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    Monster monster;
    MonsterCard card;
    Deck deck;
    private void Awake()
    {
        monster = GetComponent<Monster>();
        deck = GetComponent<Deck>();
        card = GetComponent<MonsterCard>();
    }

    void Start()
    {
        AddActInDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddActInDeck()
    {
        deck.deck.Add(monster.action1);
        deck.deck.Add(monster.action2);
        deck.deck.Add(monster.action3);
        deck.deck.RemoveAll(x => x == 0);
        deck.DeckCounter = deck.deck.Count;
    }

    public void SelectAction()
    {
        int i = Random.Range(0, deck.DeckCounter);
        card.cardID = deck.deck[i];
        card.cardTarget = N_BattleManager.instance.units[Random.Range(0, N_BattleManager.instance.units.Count)].gameObject;
        card.SetCardAction();
       
        card.UseCard();
        Debug.Log(gameObject.name + "ÀÇ " + i + "°ø°Ý");
    }
}
