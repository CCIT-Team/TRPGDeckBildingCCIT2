using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDisplay : MonoBehaviour
{
    public Image image;
    public Text rewardName;


    public void DisplayRewardInfo(int rewardID, bool isitem = true)
    {
        if(!isitem)
        {
            rewardName.text = rewardID + "°ñµå";
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
        }   
    }

    public void GetReward()
    {
        Debug.Log(rewardName.text + "¸¦ ¹Þ¾Ò´Ù");
        gameObject.SetActive(false);
    }

    public void DumpReward()
    {
        Debug.Log(rewardName.text + "¸¦ ¹ö·È´Ù");
        gameObject.SetActive(false);
    }
}
