using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileUI : MonoBehaviour
{
    public GameObject castle;
    public GameObject shopUI;
    public GameObject hospital;
    public GameObject monsterBattleUI;
    public Image missionMark;

    public int playerNumber = 0;
    public TMP_Text monsterName;
    public TMP_Text playerNumtext;
    public TMP_Text monsterNumtext;

    public Shop shop;

    void Awake()
    {
        castle.SetActive(false);
        shopUI.SetActive(false);
        hospital.SetActive(false);
        monsterBattleUI.SetActive(false);

        if (Map.instance != null && Map.instance.tileUI == null)
        {
            Debug.Log("ReConnectTileUI");
            Map.instance.tileUI = this;
        }
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
    public void Oncastle() { castle.SetActive(true); ; Map.instance.isOutofUI = true; }
    public void Offcastle()
    {
        SoundManager.instance.PlayUICilckSound();
        castle.SetActive(false);
        InitializedPlayerTurn();
    }
    public void OnShopUI()
    {
        SoundManager.instance.PlayUICilckSound();
        Offcastle();
        OnShop();
    }
    public void OnHospitalUI()
    {
        SoundManager.instance.PlayUICilckSound();
        Offcastle();
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
    public void OnMonsterBattle()
    {
        monsterBattleUI.SetActive(true);
    }
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
