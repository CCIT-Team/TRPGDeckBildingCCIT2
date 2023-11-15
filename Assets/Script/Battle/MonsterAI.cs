using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    Monster monster;
    MonsterCard card;
    private void Awake()
    {
        monster = GetComponent<Monster>();
        card = GetComponent<MonsterCard>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAction()
    {
        int i = Random.Range(0, 3);
        card.SetCardAction();
        card.UseCard();
        Debug.Log(gameObject.name + "ÀÇ " + i + "°ø°Ý");
    }
}
