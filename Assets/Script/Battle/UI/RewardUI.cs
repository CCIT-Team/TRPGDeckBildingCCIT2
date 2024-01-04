using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardUI : MonoBehaviour
{
    public GameObject rewardUIPrefab;
    int rewardGold = 0;
    int rewardExp = 0;
    List<int> rewardItem = new List<int>();
    [HideInInspector]
    public int rewardCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddReward(bool isItem,int no)
    {
        if(isItem)
        {
            rewardItem.Add(no);
            
        }
        else
        {
            rewardGold += no;
        }
    }

    public void GainExp(int exp)
    {
        rewardExp += exp;
    }

    public void GiveReward()
    {
        if(N_BattleManager.instance.currentUnit.CompareTag("Player"))
        {
            if (!N_BattleManager.instance.units.Contains(N_BattleManager.instance.currentUnit))
                N_BattleManager.instance.units.Add(N_BattleManager.instance.currentUnit);
        }

        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if(unit.CompareTag("Player"))
            {
                unit.GetComponent<Character>().SetExp(rewardExp);
            }
        }
        GameObject reward;
        for (int i = 0; i< rewardItem.Count; i++)
        {
            reward = Instantiate(rewardUIPrefab, this.transform);
            reward.GetComponent<RewardDisplay>().DisplayRewardInfo(rewardItem[i]);
            for (int j = 0; j < N_BattleManager.instance.units.Count; j++)
            {
                reward.GetComponent<RewardDisplay>().ButtonTexts[j].text = "Deleted";
                reward.GetComponent<RewardDisplay>().ButtonTexts[j].text = N_BattleManager.instance.units[j].gameObject.name + " È¹µæ";
            }
            foreach ( TMP_Text text in reward.GetComponent<RewardDisplay>().ButtonTexts)
            {
                if (text.text == "Deleted")
                    text.transform.parent.gameObject.SetActive(false);
            }
            rewardCount++;
        }
        reward = Instantiate(rewardUIPrefab, this.transform);
        reward.GetComponent<RewardDisplay>().DisplayRewardInfo(rewardGold, false);
        for (int j = 0; j < N_BattleManager.instance.units.Count; j++)
        {
            reward.GetComponent<RewardDisplay>().ButtonTexts[j].text = "Deleted";
            reward.GetComponent<RewardDisplay>().ButtonTexts[j].text = N_BattleManager.instance.units[j].gameObject.name + " È¹µæ";
        }
        foreach (TMP_Text text in reward.GetComponent<RewardDisplay>().ButtonTexts)
        {
            if (text.text == "Deleted")
                text.transform.parent.gameObject.SetActive(false);
        }
        rewardCount++;
        StartCoroutine(WaitGetAllReward());
    }

    IEnumerator WaitGetAllReward()
    {
        yield return new WaitUntil(() => rewardCount <= 0);
        N_BattleManager.instance.EndBattle();
    }
}
