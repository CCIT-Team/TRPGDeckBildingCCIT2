using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster :MonsterStat
{
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
                DropReward();
                N_BattleManager.instance.ExitBattle(this);
            }
        }
    }

    private void Awake()
    {
        deck = gameObject.AddComponent<Deck>();
        card = gameObject.AddComponent<MonsterCard>();
        GetComponent<UnitAnimationControl>().SetAnimator();
    }

    void AddActInDeck()
    {
        deck.deck.Add(action1);
        deck.deck.Add(action2);
        deck.deck.Add(action3);
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
        gameObject.name = _name;
        AddActInDeck();
        Hp = maxHp;
    }

    public void DropReward()
    {
        N_BattleManager.instance.rewardUI.AddReward(false, dropGold);
        //아이템1
        if(dropitem1 != 0 && dropitem1Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, dropitem1);
        //아이템2
        if (dropitem2 != 0 && dropitem2Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, dropitem2);
        //아이템3
        if (dropitem3 != 0 && dropitem3Percentage <= Random.Range(0, 100))
            N_BattleManager.instance.rewardUI.AddReward(true, dropitem3);
    }
}
