using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{
    public Image image;
    public Text rewardName;
    int itemID = -1;
    int gold = -1;


    public void DisplayRewardInfo(int rewardID, bool isitem = true)
    {
        if(!isitem)
        {
            rewardName.text = rewardID + "°ñµå";
            gold = rewardID;
        }
        else
        {
            int itemType = int.Parse(rewardID.ToString().Substring(0, 5));
            switch (itemType)
            {
                case 12000:
                    foreach (ItemData item in DataBase.instance.itemData)
                    {
                        if (item.no == rewardID)
                            rewardName.text = item.name;
                    }
                    break;
                case 12001:
                    foreach (WeaponData weapon in DataBase.instance.weaponData)
                    {
                        if (weapon.no == rewardID)
                            rewardName.text = weapon.name;
                    }
                    break;
                case 22000:
                    foreach (ArmorData armor in DataBase.instance.armorData)
                    {
                        if (armor.no == rewardID)
                            rewardName.text = armor.name;
                    }
                    break;
            }
            itemID = rewardID;
        }   
    }

    public void GetReward()
    {
        if(itemID == -1)
            N_BattleManager.instance.units[Random.Range(0, N_BattleManager.instance.units.Count)].GetComponent<Character>().gold += gold;
        N_BattleManager.instance.rewardUI.rewardCount--;
        gameObject.SetActive(false);
    }

    public void DumpReward()
    {
        N_BattleManager.instance.rewardUI.rewardCount--;
        gameObject.SetActive(false);
    }
}
