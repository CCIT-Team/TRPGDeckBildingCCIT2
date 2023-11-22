using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region 상점
    public void BuyingRegulerPosion()
    {
        //작은 포션 1업
        Debug.Log("작은 포션 1업");
    }
    public void BuyingLargePosion()
    {
        //큰 포션 1업
        Debug.Log("큰 포션 1업");
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
        //큰 포션 1업
    }
    #endregion
}
