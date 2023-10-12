using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDummy : Character
{
    void Start()
    {
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            BattleManager.instance.ExitBattle(this);
            this.gameObject.SetActive(false);
        }
    }
}
