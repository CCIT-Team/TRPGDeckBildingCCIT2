using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    public Character linkedPlayerStat;

    public TMP_Text guard;
    public TMP_Text magicGuard;

    public GameObject buffIcon;
    public GameObject deBuffIcon;

    public Image hpbar;
    public TMP_Text hpbarText;

    private bool hpDirector = false;
    public void InitUI()
    {
        hpbar.fillAmount = linkedPlayerStat.hp / linkedPlayerStat.maxHp;
        hpbarText.text = linkedPlayerStat.hp.ToString();

        guard.text = "0";
        magicGuard.text = "0";

        InitBuffDeBuff();
    }

    public void LinkingPlayer(GameObject player)
    {
        linkedPlayerStat = player.GetComponent<Character>();
        linkedPlayerStat.simpleUI = GetComponent<PlayerStatUI>();
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
    private void Update()
    {
        UpdateGuardUI();
        hpbar.fillAmount = linkedPlayerStat.hp / linkedPlayerStat.maxHp;
        hpbarText.text = linkedPlayerStat.hp.ToString();
    }

    public void UpdateHpUI(float value)
    {
        float currentHP = linkedPlayerStat.hp;
        float updateHP = linkedPlayerStat.hp + value;

        if (updateHP > linkedPlayerStat.maxHp)
        {
            updateHP = linkedPlayerStat.maxHp;
            hpbar.fillAmount = updateHP / linkedPlayerStat.maxHp;
        }
        else if(updateHP < 0)
        {
            updateHP = 0.0f;
            hpbar.fillAmount = updateHP / linkedPlayerStat.maxHp;
        }
        hpbarText.text = linkedPlayerStat.hp.ToString();
        //코루틴
        //StartCoroutine(HpLerp(currentHP, updateHP));
    }

    public void UpdateGuardUI()
    {
        guard.text = linkedPlayerStat.attackGuard.ToString();
        magicGuard.text = linkedPlayerStat.magicGuard.ToString();
    }

    private IEnumerator HpLerp(float currentHp, float updateHp)
    {
        hpDirector = true;
        float timer = 0.0f;
        float durtion = 1.0f;
        float t = 0.0f;
        while (timer <= durtion)
        {
            timer += Time.deltaTime;
            t = timer / durtion;
            Debug.Log(t.ToString() + "값");
            //hpbar.value = Mathf.Lerp(currentHp / linkedPlayerStat.maxHp, updateHp / linkedPlayerStat.maxHp, t);
            //hpbarText.text = (hpbar.value * linkedPlayerStat.maxHp).ToString("#.#") + "/" + linkedPlayerStat.maxHp.ToString();
            yield return null;
        }

        linkedPlayerStat.hp = updateHp;
        hpbarText.text = linkedPlayerStat.hp.ToString("#.#") + "/" + linkedPlayerStat.maxHp.ToString();
        hpDirector = false;
        yield return null;
    }
}
