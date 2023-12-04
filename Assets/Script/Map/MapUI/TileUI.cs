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
    public Image missionMark;
    public GameObject[] monsterNum = new GameObject[5];//몬스터 마릿수
    public TMP_Text monsterName;

    public Shop shop;

    void Awake()
    {
        shop_hospitalUI.enabled = false;
        shopUI.enabled = false;
        hospital.enabled = false;
        monsterBattleUI.enabled = false;
        for (int i = 0; i < monsterNum.Length; i++)
        {
            monsterNum[i].SetActive(false);
        }
    }

    void InitializedPlayerTurn()
    {
        Map.instance.startTile = null;
        Map.instance.pathTileObjectList.Clear();
        Map.instance.isPlayerOnEndTile = true;
        Map.instance.currentInteracteUITile = null;
        Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
        Map.instance.isOutofUI = false;
    }

    #region 상점&병원 버튼
    public void OnShopAndHospital() { shop_hospitalUI.enabled = true; Map.instance.isOutofUI = true; }
    public void OffShopAndHospital()
    {
        SoundManager.instance.PlayUICilckSound();
        shop_hospitalUI.enabled = false;
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
    public void OnShop() { shopUI.enabled = true; Map.instance.isOutofUI = true; }
    public void OffShop()
    {
        SoundManager.instance.PlayUICilckSound();
        shopUI.enabled = false;
        InitializedPlayerTurn();
    }
    #endregion

    #region 병원 버튼
    public void OnHospital() { hospital.enabled = true; Map.instance.isOutofUI = true; }
    public void OffHospital() 
    {
        SoundManager.instance.PlayUICilckSound();
        hospital.enabled = false;
        InitializedPlayerTurn();

    }
    #endregion

    #region 몬스터 전투 버튼
    public void OnMonsterBattle() => monsterBattleUI.enabled = true;
    public void OffMonsterBattle() => monsterBattleUI.enabled = false;
    public void Fight()
    {
        SoundManager.instance.PlayUICilckSound();
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
