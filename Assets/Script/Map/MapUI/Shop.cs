using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int posionRegulerPrice = 18;
    public int posionLargePrice = 35;

    public GameObject noMoney;
    #region ����
    public void BuyingRegulerPosion()
    {
        if (Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold >= posionRegulerPrice)
        {
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().gold -= posionRegulerPrice;
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().portionRegular += 1;
            if(Map.instance.wolrdMission.mainMissionNum == 7) { Map.instance.wolrdMission.mainMissionNum = 8; }
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
    #region ġ���
    public void BuyingHeal()
    {
        Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().hp =
            Map.instance.wolrdTurn.currentPlayer.GetComponent<Character>().maxHp;
        Debug.Log("Ǯ��");
    }
    public void BuyingDeleteCurse()
    {
        //ū ���� 1��
    }
    #endregion
}
