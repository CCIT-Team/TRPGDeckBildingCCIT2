using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    //작업 순서
    //포션 아이템 소지 확인, 갯수 판단

    //public PlayerStat character;
    //public PlayerType character_Type;

    public Character linkedPlayerStat;
    public Character_type linkedPlayerType;

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
        expbar.value = linkedPlayerStat.exp / linkedPlayerStat.maxExp;
        hpbarText.text = linkedPlayerStat.hp.ToString() + "/" + linkedPlayerStat.maxHp.ToString();
        expbarText.text = linkedPlayerStat.exp.ToString() + "/" + linkedPlayerStat.maxExp.ToString();
        guard.text = "0";
        magicGuard.text = "0";
        for (int i = 0; i < linkedPlayerStat.cost; i++)
        {
            costs.transform.GetChild(i).gameObject.SetActive(true);
        }

        if(GameManager.instance.currentScene == GameManager.SceneType.Battle)
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

    public void UpdateLevelUpUI()
    {
        level.text = linkedPlayerStat.level.ToString();
        strength.text = linkedPlayerStat.strength.ToString();
        intel.text = linkedPlayerStat.intelligence.ToString();
        luck.text = linkedPlayerStat.luck.ToString();
        speed.text = linkedPlayerStat.speed.ToString();
        gold.text = linkedPlayerStat.gold.ToString();
        hpbar.value = linkedPlayerStat.hp / linkedPlayerStat.maxHp;
        expbar.value = linkedPlayerStat.exp / linkedPlayerStat.maxExp;
        hpbarText.text = linkedPlayerStat.hp.ToString() + "/" + linkedPlayerStat.maxHp.ToString();
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
        if(linkedPlayerStat.portionRegular > 0)
        {
            GameObject portionR = Instantiate(Resources.Load("Prefabs/UI/Player/portionRegular_Item", typeof(GameObject))) as GameObject;
            portionR.transform.SetParent(viewportContent.transform);
            if(linkedPlayerStat.portionRegular == 1)
            {
                portionR.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if(linkedPlayerStat.portionRegular > 1)
            {
                portionR.transform.GetChild(0).gameObject.SetActive(true);
                portionR.transform.GetChild(0).GetComponent<TMP_Text>().text = linkedPlayerStat.portionRegular.ToString();
            }
        }
        else if(linkedPlayerStat.portionRegular == 0)
        {
            //물약 삭제
        }
        
    }
}
