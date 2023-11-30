using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterStatUI : MonoBehaviour
{
    public Monster boundMonsterStat;

    public TMP_Text nickName;
    public TMP_Text level;

    public TMP_Text guard;
    public TMP_Text magicGuard;

    public Slider hpbar;
    public TMP_Text hpbarText;

    public GameObject costs;

    public void InitUI()
    {
        nickName.text = boundMonsterStat.name;
        level.text = boundMonsterStat.level.ToString();
        hpbar.value = boundMonsterStat.hp / boundMonsterStat.maxHp;
        hpbarText.text = boundMonsterStat.hp.ToString() + "/" + boundMonsterStat.maxHp.ToString();
      
        guard.text = "0";
        magicGuard.text = "0";
        //for (int i = 0; i < boundMonsterStat.cost; i++)
        //{
        //    costs.transform.GetChild(i).gameObject.SetActive(true);
        //}
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
        //UpdateGuardUI();
        //UpdateCostUI();
    }

    public void UpdateSelectUI()
    {
        if (boundMonsterStat.IsMyturn)
        {
            GameObject selectUI = transform.parent.gameObject.GetComponent<MonsterUIManager>().selectUI;
            transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            selectUI.transform.localPosition = new Vector3(selectUI.transform.localPosition.x, gameObject.transform.localPosition.y, selectUI.transform.localPosition.z);
            //selectUI.SetActive(true);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    public void UpdateHpUI()
    {
        hpbar.value = boundMonsterStat.hp / boundMonsterStat.maxHp;
        hpbarText.text = boundMonsterStat.hp.ToString() + "/" + boundMonsterStat.maxHp.ToString();
    }


    /*public void UpdateGuardUI()
    {
        guard.text = boundMonsterStat.attackGuard.ToString();
        magicGuard.text = boundMonsterStat.magicGuard.ToString();
    }*/

    /*public void UpdateCostUI()
    {
        int activeCost = 0;
        for(int i = 0; i < costs.transform.childCount; i++)
        {
            if(costs.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                activeCost++;
            }
        }

        for (int i = 0; i < activeCost; i++)
        {
            costs.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }

        for (int i = 0; i < boundMonsterStat.cost; i++)
        {
            costs.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
        }
    }*/
}
