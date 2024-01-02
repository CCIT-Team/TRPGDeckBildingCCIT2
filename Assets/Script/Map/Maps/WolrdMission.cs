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

    public GameObject mission;

    public bool missionCleard = false;

    // public  List<Dictionary<string, object>> data_Dialog;
    void Start()
    {
        Map.instance.wolrdMission = this;
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
        switch (Map.instance.missionNum)
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
                for (int i = 0; i < Map.instance.currentMissionTile.adjacentTiles.Count; i++)
                {
                    Map.instance.currentMissionTile.adjacentTiles[i].isMissionOn = false;
                }
                //용 돌아가기
                break;
            case 12:
                TwelfthMainMission();
                break;
            case 13:
                ThirdthMainMission();
                break;
            case 14:
                Fourteenth();
                break;
            case 15:
                Fifteenth();
                break;
            case 16:
                Sixteenth();
                break;
            case 17:
                Seventeenth();
                break;
            case 18:
                Eighteenth();
                break;
            case 19:
                Nineteenth();
                break;
            case 20:
                Twentieth();
                break;
            case 21:
                Twenthfirst();
                break;
            case 22:
                Twenthsecond();
                break;
            case 23:
                Twenththird();
                break;
            case 24:
                Twenthfourth();
                break;
            case 25:
                Twenthfifth();
                break;
            case 26:
                Twenthsixth();
                break;
            case 27:
                Twenthseventh();
                break;
            case 28:
                Twentheighth();
                break;
            case 29:
                Twenthninth();
                break;
            case 30:
                thirtieth();
                break;
        }
    }

    #region 미션
    public void NextMission()
    {
        Map.instance.missionNum += 1;
        Map.instance.missionChatNum += 1;
        mission.GetComponent<Mission>().SCVDataReadAndSet();
        if (Map.instance.currentMissionTile != null)
        {
            Map.instance.currentMissionTile.isMissionOn = false;
            Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        }
        mission.GetComponent<Mission>().isEndScript = false;
        mainMissionUITransform.gameObject.SetActive(true);
        //mission.SetActive(true);
    }

    void TurnOnMissionPanel()
    {
        if(mission.activeSelf == false)
        {
            mission.SetActive(true);
        }
    }

    void FirstMainMission()
    {
        mainMissionUITransform.gameObject.SetActive(false);
        gameObject.SetActive(true);
        Map.instance.missionNum = 1;
    }


    void SecondMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 2;
    }

    void ThirdMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[0];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 3;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[0].isMissionOn = false;
            Map.instance.missionMonsterTile[0].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[0].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void FourthMainMission()
    {
        GameManager.instance.isVictory = false;
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 4;
    }

    void FifthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[1];
        Map.instance.totalTileObjectList[119].GetComponent<Tile>().tileState = Tile.TileState.VillageTile;
        Map.instance.totalTileObjectList[119].GetComponent<Tile>().isVillageTile = true;
        Map.instance.totalTileObjectList[119].GetComponent<Tile>().MakeVilege();
        Map.instance.missionMonsterTile[1].isMissionOn = true;
        Map.instance.missionMonsterTile[1].MainMissionMarkerOnOff();
        Map.instance.missionNum = 5;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[1].isMissionOn = false;
            Map.instance.missionMonsterTile[1].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[1].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }

    void SixthMainMission()
    {
        GameManager.instance.isVictory = false;
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 6;
    }

    void SeventhMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[119].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 7;
    }

    void EighthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.kingdomTile[1].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 8;
    }

    void NinthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[248].GetComponent<Tile>();
        Map.instance.currentMissionTile.tileState = Tile.TileState.VillageTile;
        Map.instance.currentMissionTile.isVillageTile = true;
        Map.instance.currentMissionTile.MakeVilege();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();

        for (int i = 0; i < Map.instance.currentMissionTile.adjacentTiles.Count; i++)
        {
            Map.instance.currentMissionTile.adjacentTiles[i].isMissionOn = true;
        }
        Map.instance.missionNum = 9;

    }

    void TwelfthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[248].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 12;
    }

    void ThirdthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[119].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Fourteenth()
    {
        TurnOnMissionPanel();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 14;
    }
    void Fifteenth()
    {
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 15;
    }
    void Sixteenth()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[2];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 16;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[2].isMissionOn = false;
            Map.instance.missionMonsterTile[2].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[2].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void Seventeenth()
    {
        GameManager.instance.isVictory = false;
        Map.instance.currentMissionTile = Map.instance.kingdomTile[3].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 17;
    }
    void Eighteenth()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[3];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 18;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[3].isMissionOn = false;
            Map.instance.missionMonsterTile[3].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[3].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void Nineteenth()
    {
        GameManager.instance.isVictory = false;
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 19;
    }
    void Twentieth()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[4];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 20;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[4].isMissionOn = false;
            Map.instance.missionMonsterTile[4].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[4].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void Twenthfirst()
    {
        GameManager.instance.isVictory = false;
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 21;
    }
    void Twenthsecond()
    {
        TurnOnMissionPanel();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 22;
    }
    void Twenththird()
    {
        TurnOnMissionPanel();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 23;
    }
    void Twenthfourth()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[5];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 24;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[5].isMissionOn = false;
            Map.instance.missionMonsterTile[5].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[5].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void Twenthfifth()
    {
        GameManager.instance.isVictory = false;
        Map.instance.currentMissionTile = Map.instance.kingdomTile[7].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 25;
    }
    void Twenthsixth()
    {
        TurnOnMissionPanel();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 26;
    }
    void Twenthseventh()
    {
        TurnOnMissionPanel();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 27;
    }
    void Twentheighth()
    {
        TurnOnMissionPanel();
        Map.instance.OnUIPlayerStop();
        Map.instance.isOutofUI = true;
        Map.instance.missionNum = 28;
    }
    void Twenthninth()
    {
        Map.instance.currentMissionTile = Map.instance.missionMonsterTile[6];
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 29;
        if (GameManager.instance.isVictory)
        {
            Map.instance.missionMonsterTile[6].isMissionOn = false;
            Map.instance.missionMonsterTile[6].MainMissionMarkerOnOff();
            Map.instance.missionMonsterTile[6].DestroyMonsterTile();
            Map.instance.isOutofUI = false;
            NextMission();
        }
    }
    void thirtieth()
    {
        GameManager.instance.isVictory = false;
        Map.instance.OnUIPlayerStop();
        Map.instance.missionNum = 30;
    }
    #endregion
}
