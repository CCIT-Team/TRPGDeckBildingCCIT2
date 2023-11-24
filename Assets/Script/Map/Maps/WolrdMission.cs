using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WolrdMission : MonoBehaviour
{
    public Animator missionButtonAni;
    bool isWindowOn = false;
    public TMP_Text mainMissionText;
    public TMP_Text subMissionText;

    public Transform mainMissionUITransform;
    public Transform subMissionUITransform;

    public GameObject firstMainMission;
    public GameObject subMissionUIObject;

    public List<GameObject> missions = new List<GameObject>();
    public bool missionCleard = false;

    public int mainMissionNum = 0;

    void Start()
    {
        Map.instance.wolrdMission = this;
        mainMissionNum = Map.instance.missionNum;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMissionNum();
    }

    public void OnOffMissionWindow()
    {
        if (!isWindowOn)
        {
            missionButtonAni.SetBool("OnOff", true);
            isWindowOn = true;
        }
        else
        {
            missionButtonAni.SetBool("OnOff", false);
            isWindowOn = false;
        }
    }

    public void CheckMissionNum()
    {
        switch (mainMissionNum)
        {
            case 0:
                FirstMainMission();
                break;
            case 1:
                SecondMainMission();
                break;
            case 2:
                ThirdMainMission();
                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
    }

    void FirstMainMission()
    {
        mainMissionText.text = "�ձ����� ������";
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.kingdomTile[0].GetComponent<Tile>().isMissionOn = true;
        Map.instance.kingdomTile[0].GetComponent<Tile>().MainMissionMarkerOnOff();
        Map.instance.missionNum = 0;
    }

    void SecondMainMission()
    {
        mainMissionText.text = "���͸� ����ϼ���";
        Map.instance.currentMissionTile = Map.instance.monsterTile[0];
        Map.instance.monsterTile[0].GetComponent<Tile>().isMissionOn = true;
        Map.instance.monsterTile[0].GetComponent<Tile>().MainMissionMarkerOnOff();
        Map.instance.missionNum = 1;
        if (GameManager.instance.isVictory)
        {
            Map.instance.currentMissionTile.GetComponent<Tile>().isMissionOn = false;
            Map.instance.currentMissionTile.GetComponent<Tile>().MainMissionMarkerOnOff();
            Map.instance.currentMissionTile.GetComponent<Tile>().DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            mainMissionNum = 2;
        }
    }
    void ThirdMainMission()
    {
        mainMissionText.text = "???�� ������";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[0].GetComponent<Tile>();
        Map.instance.monsterTile[0].GetComponent<Tile>().isMissionOn = true;
        Map.instance.monsterTile[0].GetComponent<Tile>().MainMissionMarkerOnOff();
        Map.instance.missionNum = 2;
    }
}
