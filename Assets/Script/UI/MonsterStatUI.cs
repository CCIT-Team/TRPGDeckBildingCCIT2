using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterStatUI : MonoBehaviour
{
    public Monster boundMonsterStat;

    public TMP_Text guard;
    public TMP_Text magicGuard;

    public GameObject buffIcon;
    public GameObject deBuffIcon;

    public Image hpbar;
    public TMP_Text hpbarText;

    public void InitUI()
    {
        hpbar.fillAmount = boundMonsterStat.hp / boundMonsterStat.maxHp;
        hpbarText.text = boundMonsterStat.hp.ToString();
      
        guard.text = "0";
        magicGuard.text = "0";
        guard.transform.parent.gameObject.SetActive(true);
        magicGuard.transform.parent.gameObject.SetActive(true);

        InitBuffDeBuff();
        }

    public void LinkingMonster(GameObject monster)
    {
        boundMonsterStat = monster.GetComponent<Monster>();
    }

    private void Update()
    {
        UpdateHpUI();
        UpdateGuardUI();
    }


    public void UpdateHpUI()
    {
        hpbar.fillAmount = boundMonsterStat.hp / boundMonsterStat.maxHp;
        hpbarText.text = boundMonsterStat.hp.ToString("#.#");
    }


    public void UpdateGuardUI()
    {
        guard.text = boundMonsterStat.attackGuard.ToString();
        magicGuard.text = boundMonsterStat.magicGuard.ToString();
    }

    private void InitBuffDeBuff()
    {
        if (GameManager.instance.currentScene == GameManager.SceneType.Battle)
        {
            guard.transform.parent.gameObject.SetActive(true);
            magicGuard.transform.parent.gameObject.SetActive(true);
        }
        else if (GameManager.instance.currentScene == GameManager.SceneType.Wolrd)
        {
            guard.transform.parent.gameObject.SetActive(false);
            magicGuard.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            guard.transform.parent.gameObject.SetActive(false);
            magicGuard.transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < buffIcon.transform.childCount; i++)
        {
            buffIcon.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < deBuffIcon.transform.childCount; i++)
        {
            deBuffIcon.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
