using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardDisplay : MonoBehaviour
{
    public GameObject[] image;
    public TMP_Text rewardName;
    public TMP_Text rewardText;
    public List<TMP_Text> ButtonTexts;
    int itemID = -1;
    int gold = -1;


    public void DisplayRewardInfo(int rewardID, bool isitem = true)
    {
        if(!isitem)
        {
            image[0].SetActive(true);
            rewardName.text = rewardID + "골드";
            rewardText.text = rewardID + "골드를 얻었다";
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
                        {
                            image[3].SetActive(true);
                            rewardName.text = item.name;
                            rewardText.text = item.name + "을(를) 얻었다";
                        }
                            
                    }
                    break;
                case 12001:
                    foreach (WeaponData weapon in DataBase.instance.weaponData)
                    {
                        if (weapon.no == rewardID)
                        {
                            image[2].SetActive(true);
                            rewardName.text = weapon.name;
                            rewardText.text = weapon.name + "을(를) 얻었다";
                        }
                            
                    }
                    break;
                case 22000:
                    foreach (ArmorData armor in DataBase.instance.armorData)
                    {
                        if (armor.no == rewardID)
                        {
                            image[1].SetActive(true);
                            rewardName.text = armor.name;
                            rewardText.text = armor.name + "을(를) 얻었다";
                        }     
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
