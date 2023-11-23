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
    public Canvas MonsterBattleUI;
    public GameObject[] MonsterNum = new GameObject[5];//몬스터 마릿수

    public Shop shop;

    void Awake()
    {
        shop_hospitalUI.enabled = false;
        shopUI.enabled = false;
        hospital.enabled = false;
        MonsterBattleUI.enabled = false;
    }

    #region 상점&병원 버튼
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
    #region 상점 버튼
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

    #region 병원 버튼
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

    #region 몬스터 전투 버튼
    public void OnMonsterBattle() => MonsterBattleUI.enabled = true;
    public void OffMonsterBattle() => MonsterBattleUI.enabled = false;
    public void Fight()
    {
        GameManager.instance.LoadScenceName("New Battle");
        Map.instance.isBattle = true;
        Debug.Log("전투진입");
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
