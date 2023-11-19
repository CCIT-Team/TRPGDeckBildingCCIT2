using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    public GameObject rewardUIPrefab;
    int rewardGold = 0;
    List<int> rewardItem = new List<int>();
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

    public void GiveReward()
    {
        GameObject reward;
        for (int i = 0; i< rewardItem.Count; i++)
        {
            reward = Instantiate(rewardUIPrefab, this.transform);
            reward.GetComponent<RewardDisplay>().DisplayRewardInfo(rewardItem[i]);
        }
        reward = Instantiate(rewardUIPrefab, this.transform);
        reward.GetComponent<RewardDisplay>().DisplayRewardInfo(rewardGold, false);
    }
}
