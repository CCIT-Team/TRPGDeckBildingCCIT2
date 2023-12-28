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
        Map.instance.currentMissionTile = Map.instance.kingdomTile[0].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 2;
    }

    void ThirdMainMission()
    {
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
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        mainMissionNum = 7;
    }

    void EighthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.kingdomTile[3].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        mainMissionNum = 8;
    }

    void NinthMainMission()
    {
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
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[216].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 12;
    }

    void ThirdthMainMission()
    {
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Fourteenth()
    {
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Fifteenth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Sixteenth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Seventeenth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Eighteenth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Nineteenth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twentieth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthfirst()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthsecond()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenththird()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthfourth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthfifth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthsixth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthseventh()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twentheighth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void Twenthninth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    void thirtieth()
    {
        mainMissionText.text = "소식을 전달하세요";
        Map.instance.currentMissionTile = Map.instance.totalTileObjectList[78].GetComponent<Tile>();
        Map.instance.currentMissionTile.isMissionOn = true;
        Map.instance.currentMissionTile.MainMissionMarkerOnOff();
        Map.instance.missionNum = 13;
    }
    #endregion
}
