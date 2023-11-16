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

    public Shop shop;

    void Awake()
    {
        shop_hospitalUI.enabled = false;
        shopUI.enabled = false;
        hospital.enabled = false;
        MonsterBattleUI.enabled = false;
    }
    #region 상점&병원 버튼
    public void OnShopAndHospital() => shop_hospitalUI.enabled = true;
    public void OffShopAndHospital() => shop_hospitalUI.enabled = false;
    public void OnShopUI() => OnShop();
    public void OnHospitalUI() => OnHospital();
    #endregion
    #region 상점 버튼
    public void OnShop() => shopUI.enabled = true;
    public void OffShop() => shopUI.enabled = false;

    public void ItemList()
    {
        
    }

    public void BuyItem()
    {
        shop.BuyingItem();
    }
    public void SellItem()
    {
        shop.SellingItem();
    }
    public void LeaveShop()
    {
        OffShopAndHospital();
        Map.instance.isOutofUI = true;
    }
    #endregion

    #region 병원 버튼
    public void OnHospital() => shopUI.enabled = true;
    public void OffHospital() => shopUI.enabled = false;

    public void Heal()
    {

    }
    public void Leave()
    {
        OffHospital();
        Map.instance.isOutofUI = true;
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
        Map.instance.isOutofUI = true;
    }
    #endregion

}
