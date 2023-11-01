using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //�� ������ �����ϰ� �÷��̾��� ��񿡼� ī�带 Ȯ���� �����ϴ� ����
{
    GameObject DeckUI;


    public List<int> deck;
    public List<int> hand;
    public List<int> grave;

    int deckCounter;
    public int DeckCounter
    {
        get { return deckCounter; }
        set
        {
            deckCounter = value;
            if (deckCounter == 0)
            {
                deck.AddRange(grave);
                grave.Clear();
            }
        }
    }

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name + " On");
        for (int i = 0; i < deck.Count; i++)//�ӽ��ڵ�
        {
            Debug.Log(gameObject.name+":"+CardDataBase.instance.cards.Count);
            deck[i] = Random.Range(0, CardDataBase.instance.cards.Count);
        }
    }

    private void OnDisable()
    {
        if(DeckUI != null)
            DeckUI.SetActive(false);
    }

    void Start()
    {
        
    }

    public void BIndUI(GameObject ui)
    {
        DeckUI = ui;
    }

    public void LoadDeck()
    {
        for (int i = 0; i < deck.Count; i++)//�ӽ��ڵ�
        {
            deck[i] = Random.Range(0, CardDataBase.instance.cards.Count);
        }
        deckCounter = deck.Count;
    }
}
