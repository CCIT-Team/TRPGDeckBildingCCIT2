using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBottomUI : MonoBehaviour
{
    public bool[] isWhoturn;
    public Character linkedPlayerStat;
    public Character_type likedPlayerType;
    public GameObject[] renderTexture;

    public Image hpBar;
    public TMP_Text hpBarText;

    public TMP_Text guard;
    public TMP_Text magicGuard;

    public GameObject buffIcon;
    public GameObject deBuffIcon;

    public TMP_Text level;
    public TMP_Text nickName;

    public GameObject cost;

    public TMP_Text deckNum;

    public GameObject draw;

    private void Update()
    {
        if(GameManager.instance.players[0].GetComponent<Character>().isMyturn)
        {
            LinkingPlayer(GameManager.instance.players[0]);
            UpdateRender(0);
            InitUI();
        }
        else if(GameManager.instance.players[1].GetComponent<Character>().isMyturn)
        {
            LinkingPlayer(GameManager.instance.players[1]);
            UpdateRender(1);
            InitUI();
        }
        else if(GameManager.instance.players[2].GetComponent<Character>().isMyturn)
        {
            LinkingPlayer(GameManager.instance.players[2]);
            UpdateRender(2);
            InitUI();
        }
    }
    public void LinkingPlayer(GameObject player)
    {
        linkedPlayerStat = player.GetComponent<Character>();
        likedPlayerType = player.GetComponent<Character_type>();
        //linkedPlayerStat.simpleUI = GetComponent<PlayerStatUI>();
    }

    public void UpdatePlayerUI()
    {

    }

    private void UpdateRender(int index)
    {
        for(int i =0; i < renderTexture.Length; i++)
        {
            renderTexture[i].SetActive(false);
        }
        renderTexture[index].SetActive(true);
    }

    public void InitUI()
    {
        hpBar.fillAmount = linkedPlayerStat.hp / linkedPlayerStat.maxHp;
        hpBarText.text = linkedPlayerStat.hp.ToString();
        level.text = linkedPlayerStat.level.ToString();
        nickName.text = likedPlayerType.nickname;
        InitBuffDeBuff();
        deckNum.text = linkedPlayerStat.gameObject.GetComponent<Character_Card>().GetHaveCard().ToString();
        for(int i = 0; i < linkedPlayerStat.cost; i++)
        {
            cost.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void UpdateCostUI()
    {
        int activeCost = 0;
        for(int i =0; i < cost.transform.childCount; i++)
        {
            if(cost.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                activeCost++;
            }
        }

        for(int i = 0; i < activeCost; i++)
        {
            cost.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(false);
        }
        for (int i = 0; i < linkedPlayerStat.cost; i++)
        {
            cost.transform.GetChild(i).transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void InitBuffDeBuff()
    {
        if (GameManager.instance.currentScene == GameManager.SceneType.Battle)
        {
            guard.transform.parent.gameObject.SetActive(true);
            magicGuard.transform.parent.gameObject.SetActive(true);
            draw.SetActive(true);
        }
        else if (GameManager.instance.currentScene == GameManager.SceneType.Wolrd)
        {
            guard.transform.parent.gameObject.SetActive(false);
            magicGuard.transform.parent.gameObject.SetActive(false);
            draw.SetActive(false);
        }
        else
        {
            guard.transform.parent.gameObject.SetActive(false);
            magicGuard.transform.parent.gameObject.SetActive(false);
            draw.SetActive(false);
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
