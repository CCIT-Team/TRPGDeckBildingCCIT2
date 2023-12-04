using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterStatUI : MonoBehaviour
{
    public Monster boundMonsterStat;
    public GameObject selectUI;

    public TMP_Text nickName;
    public TMP_Text level;

    public TMP_Text guard;
    public TMP_Text magicGuard;

    public Slider hpbar;
    public TMP_Text hpbarText;

    public void InitUI()
    {
        nickName.text = boundMonsterStat.name;
        level.text = boundMonsterStat.level.ToString();

        hpbar.value = boundMonsterStat.hp / boundMonsterStat.maxHp;
        hpbarText.text = boundMonsterStat.hp.ToString() + "/" + boundMonsterStat.maxHp.ToString();
      
        guard.text = "0";
        magicGuard.text = "0";
        guard.transform.parent.gameObject.SetActive(true);
        magicGuard.transform.parent.gameObject.SetActive(true);
    }

    public void LinkingMonster(GameObject monster)
    {
        boundMonsterStat = monster.GetComponent<Monster>();
    }

    private void Update()
    {
        UpdateSelectUI();
        UpdateHpUI();
        UpdateGuardUI();
    }

    public void UpdateSelectUI()
    {
        if (boundMonsterStat.IsMyturn)
        {
            selectUI.SetActive(true);
            transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        }
        else
        {
            selectUI.SetActive(false);
            transform.localScale = Vector3.one;
        }
    }

    public void UpdateHpUI()
    {
        hpbar.value = boundMonsterStat.hp / boundMonsterStat.maxHp;
        hpbarText.text = boundMonsterStat.hp.ToString() + "/" + boundMonsterStat.maxHp.ToString();
    }


    public void UpdateGuardUI()
    {
        guard.text = boundMonsterStat.attackGuard.ToString();
        magicGuard.text = boundMonsterStat.magicGuard.ToString();
    }
}
