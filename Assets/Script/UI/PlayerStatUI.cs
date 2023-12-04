using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    public Character linkedPlayerStat;
    public Character_type linkedPlayerType;
    public GameObject selectUI;

    public TMP_Text nickName;
    public TMP_Text level;

    public TMP_Text strength;
    public TMP_Text intel;
    public TMP_Text luck;
    public TMP_Text speed;
    public TMP_Text gold;
    public TMP_Text guard;
    public TMP_Text magicGuard;

    public Slider hpbar;
    public Slider expbar;
    public TMP_Text hpbarText;
    public TMP_Text expbarText;

    public GameObject costs;
    public GameObject viewportContent;

    private bool isPortionR = false;
    private bool isPortionL = false;

    private bool hpDirector = false;
    public void InitUI()
    {
        nickName.text = linkedPlayerType.nickname;
        level.text = linkedPlayerStat.level.ToString();
        strength.text = linkedPlayerStat.strength.ToString();
        intel.text = linkedPlayerStat.intelligence.ToString();
        luck.text = linkedPlayerStat.luck.ToString();
        speed.text = linkedPlayerStat.speed.ToString();
        gold.text = linkedPlayerStat.gold.ToString();
        hpbar.value = linkedPlayerStat.hp / linkedPlayerStat.maxHp;
        expbar.value = Convert.ToSingle(linkedPlayerStat.exp) / Convert.ToSingle(linkedPlayerStat.maxExp);
        hpbarText.text = linkedPlayerStat.hp.ToString() + "/" + linkedPlayerStat.maxHp.ToString();
        expbarText.text = linkedPlayerStat.exp.ToString() + "/" + linkedPlayerStat.maxExp.ToString();
        guard.text = "0";
        magicGuard.text = "0";
        for (int i = 0; i < linkedPlayerStat.cost; i++)
        {
            costs.transform.GetChild(i).gameObject.SetActive(true);
        }

        if (linkedPlayerStat.portionRegular > 0)
        {
            CreatePortion();
            isPortionR = true;
        }
        else
            isPortionR = false;

        if (linkedPlayerStat.portionLarge > 0)
        {
            CreatePortion();
            isPortionL = true;
        }
        else
            isPortionL = false;

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
    }

    public void LinkingPlayer(GameObject player)
    {
        linkedPlayerStat = player.GetComponent<Character>();
        linkedPlayerType = player.GetComponent<Character_type>();
    }

    private void Update()
    {
        UpdateSelectUI();
        UpdateLevelUpUI();
        UpdateGuardUI();
        UpdateCostUI();
        UpdatePortionUI();
        if(!hpDirector)
        {
            hpbar.value = Convert.ToSingle(linkedPlayerStat.hp) / Convert.ToSingle(linkedPlayerStat.maxHp);
            hpbarText.text = linkedPlayerStat.hp.ToString() + "/" + linkedPlayerStat.maxHp.ToString();
        }
    }

    public void UpdateSelectUI()
    {
        if (linkedPlayerStat.isMyturn)
        {
            selectUI.SetActive(true);
            transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            //selectUI.transform.localPosition = new Vector3(selectUI.transform.localPosition.x, gameObject.transform.localPosition.y, selectUI.transform.localPosition.z);            
        }
        else
        {
            //GameObject selectUI = transform.parent.gameObject.GetComponent<PlayerUIManager>().selectUI;
            selectUI.SetActive(false);
            transform.localScale = Vector3.one;
        }
    }

    public void UpdateLevelUpUI()
    {
        level.text = linkedPlayerStat.level.ToString();
        strength.text = linkedPlayerStat.strength.ToString();
        intel.text = linkedPlayerStat.intelligence.ToString();
        luck.text = linkedPlayerStat.luck.ToString();
        speed.text = linkedPlayerStat.speed.ToString();
        gold.text = linkedPlayerStat.gold.ToString();
        expbar.value = Convert.ToSingle(linkedPlayerStat.exp) / Convert.ToSingle(linkedPlayerStat.maxExp);
        expbarText.text = linkedPlayerStat.exp.ToString() + "/" + linkedPlayerStat.maxExp.ToString();
    }
    public void UpdateHpUI(float value)
    {
        float currentHP = linkedPlayerStat.hp;
        float updateHP = linkedPlayerStat.hp + value;

        if (updateHP > linkedPlayerStat.maxHp)
        {
            updateHP = linkedPlayerStat.maxHp;
        }
        else if(updateHP < 0)
        {
            updateHP = 0.0f;
        }

        //코루틴
        StartCoroutine(HpLerp(currentHP, updateHP));
    }
    public void UpdateExpUI()
    {
        expbar.value = linkedPlayerStat.exp / linkedPlayerStat.maxExp;
        expbarText.text = linkedPlayerStat.exp.ToString() + "/" + linkedPlayerStat.maxExp.ToString();
    }

    public void UpdateStatUI()
    {
        strength.text = linkedPlayerStat.strength.ToString();
        intel.text = linkedPlayerStat.intelligence.ToString();
        luck.text = linkedPlayerStat.luck.ToString();
        speed.text = linkedPlayerStat.speed.ToString();
    }

    public void UpdateGuardUI()
    {
        guard.text = linkedPlayerStat.attackGuard.ToString();
        magicGuard.text = linkedPlayerStat.magicGuard.ToString();
    }

    public void UpdateCostUI()
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

        for (int i = 0; i < linkedPlayerStat.cost; i++)
        {
            costs.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void UpdatePortionUI()
    {
        CreatePortion();
    }

    private void CreatePortion()
    {
        if (!isPortionR)
        {
            InstanPortionR();
            PortionTexting();
        }

        if (!isPortionL)
        {
            InstanPortionL();
            PortionTexting();
        }

        if (isPortionR || isPortionL)
        {
            PortionTexting();
        }
    }

    private void InstanPortionR()
    {
        if (linkedPlayerStat.portionRegular > 0)
        {
            GameObject portionR = Instantiate(Resources.Load("Prefabs/UI/Player/portionRegular_Item", typeof(GameObject))) as GameObject;
            portionR.transform.SetParent(viewportContent.transform);
            portionR.GetComponent<Button>().onClick.AddListener((PortionRegularHeal));
            isPortionR = true;
        }
    }

    private void InstanPortionL()
    {
        if(linkedPlayerStat.portionLarge > 0)
        {
            GameObject portionL = Instantiate(Resources.Load("Prefabs/UI/Player/portionLarge_Item", typeof(GameObject))) as GameObject;
            portionL.transform.SetParent(viewportContent.transform);
            portionL.GetComponent<Button>().onClick.AddListener((PortionLargeHeal));
            isPortionL = true;
        }
    }

    private void PortionTexting()
    {
        for (int i = 0; i < viewportContent.transform.childCount; i++)
        {
            if (viewportContent.transform.GetChild(i).tag == "PortionR")
            {
                if(linkedPlayerStat.portionRegular == 0)
                {
                    Destroy(viewportContent.transform.GetChild(i).gameObject);
                    isPortionR = false;
                    break;
                }

                if(linkedPlayerStat.portionRegular == 1)
                {
                    viewportContent.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    viewportContent.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                }
                viewportContent.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = linkedPlayerStat.portionRegular.ToString();
            }

            if (viewportContent.transform.GetChild(i).tag == "PortionL")
            {
                if (linkedPlayerStat.portionLarge == 0)
                {
                    Destroy(viewportContent.transform.GetChild(i).gameObject);
                    isPortionL = false;
                    break;
                }

                if (linkedPlayerStat.portionLarge == 1)
                {
                    viewportContent.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    viewportContent.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
                }
                viewportContent.transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = linkedPlayerStat.portionLarge.ToString();
            }
        }
    }

    public void PortionRegularHeal()
    {
        float healValue = linkedPlayerStat.maxHp * 0.3f;
        if (linkedPlayerStat.isMyturn)
        {
            linkedPlayerStat.portionRegular--;
            PortionTexting();
            UpdateHpUI(healValue);
        }
    }

    public void PortionLargeHeal()
    {
        float healValue = linkedPlayerStat.maxHp * 0.4f;
        if (linkedPlayerStat.isMyturn)
        {
            linkedPlayerStat.portionLarge--;
            PortionTexting();
            UpdateHpUI(healValue);
        }
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
            hpbar.value = Mathf.Lerp(currentHp / linkedPlayerStat.maxHp, updateHp / linkedPlayerStat.maxHp, t);
            hpbarText.text = (hpbar.value * linkedPlayerStat.maxHp).ToString("#.#") + "/" + linkedPlayerStat.maxHp.ToString();
            yield return null;
        }

        linkedPlayerStat.hp = updateHp;
        hpbarText.text = linkedPlayerStat.hp.ToString("#.#") + "/" + linkedPlayerStat.maxHp.ToString();
        hpDirector = false;
        yield return null;
    }
}
