using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    #region ����
    public void BuyingRegulerPosion()
    {
        //���� ���� 1��
        Debug.Log("���� ���� 1��");
    }
    public void BuyingLargePosion()
    {
        //ū ���� 1��
        Debug.Log("ū ���� 1��");
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
