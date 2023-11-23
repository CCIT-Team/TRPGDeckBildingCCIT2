using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileUI : MonoBehaviour
{
    public Canvas shop_hospitalUI;
    public Canvas shopUI;
    public Canvas hospital;
    public Canvas monsterBattleUI;
    public GameObject[] monsterNum = new GameObject[5];//���� ������
    public TMP_Text monsterName;

    public Shop shop;

    void Awake()
    {
        shop_hospitalUI.enabled = false;
        shopUI.enabled = false;
        hospital.enabled = false;
        monsterBattleUI.enabled = false;
    }

    #region ����&���� ��ư
    public void OnShopAndHospital() { shop_hospitalUI.enabled = true; }
    public void OffShopAndHospital()
    {
        shop_hospitalUI.enabled = false;
        Map.instance.startTile = null;
        Map.instance.pathTileObjectList.Clear();
        Map.instance.isPlayerOnEndTile = true;
        Map.instance.currentInteracteUITile = null;
        Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
        Map.instance.isOutofUI = false;
    }
    public void OnShopUI()
    {
        OffShopAndHospital();
        OnShop();
    }
    public void OnHospitalUI()
    {
        OffShopAndHospital();
        OnHospital();
    }
    #endregion
    #region ���� ��ư
    public void OnShop() { shopUI.enabled = true; }
    public void OffShop()
    {
        shopUI.enabled = false;
        Map.instance.startTile = null;
        Map.instance.pathTileObjectList.Clear();
        Map.instance.isPlayerOnEndTile = true;
        Map.instance.currentInteracteUITile = null;
        Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
        Map.instance.isOutofUI = false;
    }
    #endregion

    #region ���� ��ư
    public void OnHospital() { hospital.enabled = true; }
    public void OffHospital() 
    {
        hospital.enabled = false;
        Map.instance.startTile = null;
        Map.instance.pathTileObjectList.Clear();
        Map.instance.isPlayerOnEndTile = true;
        Map.instance.currentInteracteUITile = null;
        Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
        Map.instance.isOutofUI = false;

    }
    #endregion

    #region ���� ���� ��ư
    public void OnMonsterBattle() => monsterBattleUI.enabled = true;
    public void OffMonsterBattle() => monsterBattleUI.enabled = false;
    public void Fight()
    {
        GameManager.instance.LoadScenceName("New Battle");
        Map.instance.isBattle = true;
        Debug.Log("��������");
    }
    public void Run()
    {
        OffMonsterBattle();
        Map.instance.startTile = null;
        Map.instance.pathTileObjectList.Clear();
        Map.instance.isPlayerOnEndTile = true;
        Map.instance.currentInteracteUITile = null;
        Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
        Map.instance.isOutofUI = false;
    }
    #endregion
}
