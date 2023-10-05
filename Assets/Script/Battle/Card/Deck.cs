using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //덱 정보를 소지하고 플레이어의 장비에서 카드를 확인해 저장하는 역할
{
    public List<int> deck;
    public List<int> hand;
    public List<int> grave;

    public int counter;

    private void Awake()
    {
        
    }

    void Start()
    {
        for(int i = 0; i < deck.Count;i++)//임시코드
        {
            deck[i] = Random.Range(0, CardDataBase.instance.cards.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
