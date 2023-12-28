using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WolrdMission : MonoBehaviour
{
    [SerializeField] Animator missionButtonAni;
    bool isWindowOn = false;
    public TMP_Text mainMissionText;
    public TMP_Text subMissionText;

    public Image missionCharacter;
    public TMP_Text chracterName;

    public Transform mainMissionUITransform;
    public Transform subMissionUITransform;

    public Sprite[] missionChraterImage = new Sprite[5];

    public  GameObject mission;

    public bool missionCleard = false;

    public int mainMissionNum = 1;

  // public  List<Dictionary<string, object>> data_Dialog;
    void Start()
    {
        Map.instance.wolrdMission = this;
        mainMissionNum = Map.instance.missionNum;
        //data_Dialog = CSVReader.Read("MissionCSV/MissionDialog");
    }

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
            case 1:
                FirstMainMission();
                break;
            case 2:
                SecondMainMission();
                break;
            case 3:
                ThirdMainMission();
                break;
            case 4:
                FourthMainMission();
                break;
            case 5:
                FifthMainMission();
                break;
            case 6:
                SixthMainMission();
                break;
            case 7:
                SeventhMainMission();
                break;
            case 8:
                EighthMainMission();
                break;
            case 9:
                NinthMainMission();
                break;
            case 10:
                //용 날라가기
                break;
            case 11:
                //용 돌아가기
                break;
            case 12:
                TwelfthMainMission();
                break;
            case 13:
                ThirdthMainMission();
                break;
            case 14:
                mainMissionText.text = "플레이해주셔서 감사합니다!!";
                break;
        }
    }

    #region 미션
    public void NextMission()
    {
            mission.GetComponent<Mission>().SCVDataReadAndSet();
            mainMissionNum += 1;
            Map.instance.missionChatNum += 1;
            if (Map.instance.currentMissionTile != null)
            {
                Map.instance.currentMissionTile.isMissionOn = false;
                Map.instance.currentMissionTile.MainMissionMarkerOnOff();
            }
            mission.GetComponent<Mission>().isEndScript = false;
            gameObject.SetActive(true);
    }

    void FirstMainMission()
    {
        mainMissionUITransform.gameObject.SetActive(false);
        gameObject.SetActive(true);
        Map.instance.missionNum = 1;
    }


    void SecondMainMission()
    {
        mainMissionUITransform.gameObject.SetActive(true);
        mainMissionText.text = "왕국으로 가세요";
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 2;
    }

    void ThirdMainMission()
    {
        mainMissionText.text = "공동묘지로 가세요";
        Map.instance.currentMissionTile = Map.instance.monsterTile[0];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 3;
        if (GameManager.instance.isVictory)
        {
            Map.instance.monsterTile[0].isMissionOn = false;
            Map.instance.monsterTile[0].MainMissionMarkerOnOff();
            Map.instance.monsterTile[0].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void FourthMainMission()
    {
        GameManager.instance.isVictory = false;
        Map.instance.wolrdMission.mission.SetActive(true);
        Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 4;
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
        Map.instance.missionNum = 5;
        if (GameManager.instance.isVictory)
        {
            Map.instance.monsterTile[1].isMissionOn = false;
            Map.instance.monsterTile[1].MainMissionMarkerOnOff();
            Map.instance.monsterTile[1].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }

    void SixthMainMission()
    {
        GameManager.instance.isVictory = false;
        Map.instance.wolrdMission.mission.SetActive(true);
        Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        mainMissionNum = 6;
    }

    void SeventhMainMission()
    {
        mainMissionText.text = "마을로 이동하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        mainMissionNum = 7;
    }

    void EighthMainMission()
    {
        mainMissionText.text = "성으로 이동하세요";
        Map.instance.currentMissionTile = Map.instance.kingdomTile[3].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        mainMissionNum = 8;
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
        Map.instance.missionNum = 9;

    }

    void TwelfthMainMission()
    {
        mainMissionText.text = "마을을 확인하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[216].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 12;
    }

    void ThirdthMainMission()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    #endregion
}
