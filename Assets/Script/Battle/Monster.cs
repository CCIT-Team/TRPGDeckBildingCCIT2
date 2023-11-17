using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { None = 0 ,Undead };

public class Monster : Unit
{
    public MonsterStat monsterStat;
    MonsterCard card;
    Deck deck;

    bool ismyturn;
    public bool IsMyturn
    {
        get { return ismyturn; }
        set
        {
            ismyturn = value;
            if(ismyturn)
            {
                SelectAction();
            }
            
        }
    }

    public override float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                battleState = BattleState.Death;
                DropReward();
                N_BattleManager.instance.ExitBattle(this);
            }
        }
    }

    private void Awake()
    {
        monsterStat = GetComponent<MonsterStat>();
        deck = gameObject.AddComponent<Deck>();
        card = gameObject.AddComponent<MonsterCard>();
    }

    void AddActInDeck()
    {
        deck.deck.Add(monsterStat.action1);
        deck.deck.Add(monsterStat.action2);
        deck.deck.Add(monsterStat.action3);
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
    }

    public void SetMonster()
    {
        gameObject.name = monsterStat._name;
        AddActInDeck();
        maxHp = monsterStat.maxHp;
        Hp = maxHp;
    }

    public void DropReward()
    {
        N_BattleManager.instance.rewardUI.AddReward(false, monsterStat.dropGold);
        //아이템1
        if(monsterStat.dropitem1 != 0 && monsterStat.dropitem1Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, monsterStat.dropitem1);
        //아이템2
        if (monsterStat.dropitem2 != 0 && monsterStat.dropitem2Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, monsterStat.dropitem2);
        //아이템3
        if (monsterStat.dropitem3 != 0 && monsterStat.dropitem3Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, monsterStat.dropitem3);
    }
}
