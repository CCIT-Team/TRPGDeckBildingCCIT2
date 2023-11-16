using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { None = 0 ,Undead };

public class Monster : Unit
{

    public MonsterData monsterdData;
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

                N_BattleManager.instance.ExitBattle(this);
            }
        }
    }

    private void Awake()
    {
        deck = GetComponent<Deck>();
        card = GetComponent<MonsterCard>();
    }

    void AddActInDeck()
    {
        deck.deck.Add(monsterdData.action1);
        deck.deck.Add(monsterdData.action2);
        deck.deck.Add(monsterdData.action3);
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

    public void GetMonsterData(int indexNo)
    {
        monsterdData =  DataBase.instance.monsterData[int.Parse(indexNo.ToString().Substring(1))];
        gameObject.name = monsterdData.name;
        AddActInDeck();
        maxHp = monsterdData.hp;
        Hp = maxHp;
    }

    public void DropReward()
    {
        N_BattleManager.instance.rewardUI.AddReward(false, monsterdData.dropGold);
        //아이템1
        if(monsterdData.dropitem1 != 0 && monsterdData.dropitem1Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, monsterdData.dropitem1);
        //아이템2
        if (monsterdData.dropitem2 != 0 && monsterdData.dropitem2Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, monsterdData.dropitem2);
        //아이템3
        if (monsterdData.dropitem3 != 0 && monsterdData.dropitem3Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, monsterdData.dropitem3);
    }
}
