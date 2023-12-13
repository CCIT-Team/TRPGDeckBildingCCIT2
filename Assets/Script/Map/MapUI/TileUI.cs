using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileUI : MonoBehaviour
{
    public GameObject shop_hospitalUI;
    public GameObject shopUI;
    public GameObject hospital;
    public GameObject monsterBattleUI;
    public Image missionMark;
    public GameObject[] monsterNum = new GameObject[5];//몬스터 마릿수
    public TMP_Text monsterName;

    public Shop shop;

    void Awake()
    {
        shop_hospitalUI.SetActive(false);
        shopUI.SetActive(false);
        hospital.SetActive(false);
        monsterBattleUI.SetActive(false);
    }

    void InitializedPlayerTurn()
    {
        Map.instance.startTile = null;
        Map.instance.pathTileObjectList.Clear();
        Map.instance.isPlayerOnEndTile = true;
        Map.instance.currentInteracteUITile = null;
        Map.instance.isOutofUI = false;
    }

    #region 상점&병원 버튼
    public void OnShopAndHospital() { shop_hospitalUI.SetActive(true); ; Map.instance.isOutofUI = true; }
    public void OffShopAndHospital()
    {
        SoundManager.instance.PlayUICilckSound();
        shop_hospitalUI.SetActive(false);
        InitializedPlayerTurn();
    }
    public void OnShopUI()
    {
        SoundManager.instance.PlayUICilckSound();
        OffShopAndHospital();
        OnShop();
    }
    public void OnHospitalUI()
    {
        SoundManager.instance.PlayUICilckSound();
        OffShopAndHospital();
        OnHospital();
    }
    #endregion
    #region 상점 버튼
    public void OnShop() { shopUI.SetActive(true); Map.instance.isOutofUI = true; }
    public void OffShop()
    {
        SoundManager.instance.PlayUICilckSound();
        shopUI.SetActive(false);
        InitializedPlayerTurn();
    }
    #endregion

    #region 병원 버튼
    public void OnHospital() { hospital.SetActive(true); Map.instance.isOutofUI = true; }
    public void OffHospital() 
    {
        SoundManager.instance.PlayUICilckSound();
        hospital.SetActive(false);
        InitializedPlayerTurn();

    }
    #endregion

    #region 몬스터 전투 버튼
    public void OnMonsterBattle() => monsterBattleUI.SetActive(true);
    public void OffMonsterBattle() => monsterBattleUI.SetActive(false);
    public void Fight()
    {
        SoundManager.instance.PlayUICilckSound();
        GameManager.instance.SetBattleMonsterSetting(Map.instance.currentInteracteUITile.monsterGroup);
        GameManager.instance.LoadScenceName("New Battle");
        Map.instance.isBattle = true;
        Debug.Log("전투진입");
    }
    public void Run()
    {
        SoundManager.instance.PlayUICilckSound();
        OffMonsterBattle();
        InitializedPlayerTurn();
    }
    #endregion
}
