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
    public GameObject secondMainMission;
    public GameObject fourthdMainMission;
    public GameObject sixthdMainMission;
    public GameObject seventhdMainMission;
    public GameObject eleventhdMainMission;
    public GameObject twelfthdMainMission;
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
                FourthMainMission();
                break;
            case 4:
                FifthMainMission();
                break;
            case 5:
                SixthMainMission();
                break;
            case 6:
                SeventhMainMission();
                break;
            case 7:
                EighthMainMission();
                break;
            case 8:
                NinthMainMission();
                break;
            case 9:
                //용 날라가기
                break;
            case 10:
                //용 돌아가기
                break;
            case 11:
                EleventhMainMission();
                break;
            case 12:
                TwelfthMainMission();
                break;
            case 13:
                mainMissionText.text = "플레이해주셔서 감사합니다!!";
                break;
        }
    }

    void FirstMainMission()
    {
        mainMissionUITransform.gameObject.SetActive(false);
        firstMainMission.SetActive(true);
        Map.instance.missionNum = 0;
    }


    void SecondMainMission()
    {
        mainMissionUITransform.gameObject.SetActive(true);
        mainMissionText.text = "왕국으로 가세요";
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 1;
    }

    void ThirdMainMission()
    {
        mainMissionText.text = "공동묘지로 가세요";
        Map.instance.currentMissionTile = Map.instance.monsterTile[0];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 2;
        if (GameManager.instance.isVictory)
        {
            Map.instance.currentMissionTile.isMissionOn = false;
            Map.instance.currentMissionTile.MainMissionMarkerOnOff();
            Map.instance.currentMissionTile.DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            mainMissionNum = 3;
        }
    }
    void FourthMainMission()
    {
        GameManager.instance.isVictory = false;
        fourthdMainMission.SetActive(true);
        Map.instance.missionNum = 3;
    }

    void FifthMainMission()
    {
        mainMissionText.text = "마을 근처 몬스터를 토벌하세요";
        Map.instance.currentMissionTile = Map.instance.monsterTile[1];
        Map.instance.totalTileObjectList[78].GetComponent<Tile>().tileState = Tile.TileState.VillageTile;
        Map.instance.totalTileObjectList[78].GetComponent<Tile>().isVillageTile = true;
        Map.instance.totalTileObjectList[78].GetComponent<Tile>().MakeVilege();
        Map.instance.monsterTile[1].isMissionOn = true;
        Map.instance.monsterTile[1].MainMissionMarkerOnOff();
        Map.instance.missionNum = 4;
        if (GameManager.instance.isVictory)
        {
            Map.instance.currentMissionTile.isMissionOn = false;
            Map.instance.currentMissionTile.MainMissionMarkerOnOff();
            Map.instance.currentMissionTile.DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            mainMissionNum = 5;
        }
    }

    void SixthMainMission()
    {
        GameManager.instance.isVictory = false;
        sixthdMainMission.SetActive(true);
        mainMissionNum = 5;
    }

    void SeventhMainMission()
    {
        mainMissionText.text = "마을로 이동하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        mainMissionNum = 6;
    }

    void EighthMainMission()
    {
        mainMissionText.text = "성으로 이동하세요";
        Map.instance.currentMissionTile = Map.instance.kingdomTile[3].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        mainMissionNum = 7;
    }

    void NinthMainMission()
    {
        mainMissionText.text = "포션을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[216].GetComponent<Tile>();
        Map.instance.currentMissionTile.GetComponent<Tile>().tileState = Tile.TileState.VillageTile;
        Map.instance.currentMissionTile.GetComponent<Tile>().isVillageTile = true;
        Map.instance.currentMissionTile.GetComponent<Tile>().MakeVilege();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();

        for (int i = 0; i < 6; i++)
        {
            Map.instance.currentMissionTile.adjacentTiles[i].isMissionOn = true;
        } 
        Map.instance.missionNum = 8;
    }

    void EleventhMainMission()
    {
        mainMissionText.text = "마을을 확인하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[216].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 11;
    }

    void TwelfthMainMission()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 12;
    }
}
