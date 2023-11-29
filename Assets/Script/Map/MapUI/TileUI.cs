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
    public GameObject[] monsterNum = new GameObject[5];//���� ������
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

    #region ����&���� ��ư
    public void OnShopAndHospital() { shop_hospitalUI.enabled = true; Map.instance.isOutofUI = true; }
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
    #region ���� ��ư
    public void OnShop() { shopUI.enabled = true; Map.instance.isOutofUI = true; }
    public void OffShop()
    {
        SoundManager.instance.PlayUICilckSound();
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
    public void OnHospital() { hospital.enabled = true; Map.instance.isOutofUI = true; }
    public void OffHospital() 
    {
        SoundManager.instance.PlayUICilckSound();
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
        SoundManager.instance.PlayUICilckSound();
        GameManager.instance.LoadScenceName("New Battle");
        Map.instance.isBattle = true;
        Debug.Log("��������");
    }
    public void Run()
    {
        SoundManager.instance.PlayUICilckSound();
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
