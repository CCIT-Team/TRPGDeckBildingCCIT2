using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int posionRegulerPrice = 18;
    public int posionLargePrice = 35;
    public int healPrise = 20;
    public int deleteCurse = 40;

    public GameObject noMoney;
    #region 상점
    public void BuyingRegulerPosion()
    {
        if (Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold >= posionRegulerPrice)
        {
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold -= posionRegulerPrice;
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().portionRegular += 1;
            if(Map.instance.wolrdMission.mainMissionNum == 7) 
            {
                Map.instance.currentMissionTile.isMissionOn = false;
                Map.instance.currentMissionTile.MainMissionMarkerOnOff();
                Map.instance.wolrdMission.mainMissionNum = 8; 
            }
        }
        else
        {
            StartCoroutine(Comebackwiththemoney());
        }
    }
    public void BuyingLargePosion()
    {
        if (Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold >= posionLargePrice)
        {
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold -= posionLargePrice;
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().portionLarge += 1;
            if (Map.instance.wolrdMission.mainMissionNum == 7)
            {
                Map.instance.currentMissionTile.isMissionOn = false;
                Map.instance.currentMissionTile.MainMissionMarkerOnOff();         
                Map.instance.wolrdMission.mainMissionNum = 8;
            }
        }
        else
        {
            StartCoroutine(Comebackwiththemoney());
        }
    }

    IEnumerator Comebackwiththemoney()
    {
        noMoney.SetActive(true);
        yield return new WaitForSeconds(1);
        noMoney.SetActive(false);
    }

    #endregion
    #region 치료소
    public void BuyingHeal()
    {
        Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().hp =
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().maxHp;
        Debug.Log("풀피");
    }
    public void BuyingDeleteCurse()
    {
        
    }
    #endregion
}
