using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach(Unit unit in N_BattleManager.instance.units)
        {
            if(unit.CompareTag("Player"))
            {
                unit.GetComponent<Character>().SetExp(rewardExp);
            }
        }
        GameObject reward;
        int playercount = 0;
        for (int i = 0; i< rewardItem.Count; i++)
        {
            reward = Instantiate(rewardUIPrefab, this.transform);
            reward.GetComponent<RewardDisplay>().DisplayRewardInfo(rewardItem[i]);
            foreach (Unit unit in N_BattleManager.instance.units)
            {
                if (unit.CompareTag("Player"))
                {
                    reward.GetComponent<RewardDisplay>().ButtonTexts[playercount].text = unit.gameObject.name+ " È¹µæ";
                    playercount++;
                }
            }
            rewardCount++;
        }
        playercount = 0;
        reward = Instantiate(rewardUIPrefab, this.transform);
        reward.GetComponent<RewardDisplay>().DisplayRewardInfo(rewardGold, false);
        foreach (Unit unit in N_BattleManager.instance.units)
        {
            if (unit.CompareTag("Player"))
            {
                reward.GetComponent<RewardDisplay>().ButtonTexts[playercount].text = unit.gameObject.name + " È¹µæ";
                playercount++;
            }
        }
        rewardCount++;
        playercount = 0;
        StartCoroutine(WaitGetAllReward());
    }

    IEnumerator WaitGetAllReward()
    {
        yield return new WaitUntil(() => rewardCount <= 0);
        N_BattleManager.instance.EndBattle();
    }
}
