using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour   //�� ������ �����ϰ� �÷��̾��� ��񿡼� ī�带 Ȯ���� �����ϴ� ����
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
        for(int i = 0; i < deck.Count;i++)//�ӽ��ڵ�
        {
            deck[i] = Random.Range(0, CardDataBase.instance.cards.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
