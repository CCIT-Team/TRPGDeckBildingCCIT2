using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Tile : MonoBehaviour
{
    TileUI tileUI;

    public Material[] climateMaterials = new Material[3];

    public GameObject[] tileSelectImage = new GameObject[2];//0 = Select 1 = SelectBold
    public TMP_Text walkAbleNumText;
    public int F => g + h;

    public int g;

    public int h;

    public Vector3Int position;

    public Tile[] adjacentTilesObject = new Tile[6];

    public List<Tile> adjacentTiles = new List<Tile>(6);

    bool isfindNeiber = false;

    public bool isObstacle;

    public bool isSpawnTile = false;

    public bool isMonsterTile = false;

    public bool isBossTile = false;

    public bool isKingdomTile = false;

    public bool isVillageTile = false;

    public bool isMissionOn = false;

    Character player;
    GameObject tagPlayer;
    GameObject dragon;

    bool isMonsterSet = false;

    [SerializeField] MeshRenderer material;
    Color defaultColor;

    [SerializeField] GameObject kingdomObject;
    [SerializeField] GameObject burnkingdomObject;
    [SerializeField] GameObject vileageObject;
    [SerializeField] GameObject burnVileageObject;
    [SerializeField] GameObject monsterObject;
    [SerializeField] Transform monsterPosition;
    [SerializeField] int monsterNum;//스폰된 몬스터의 마릿수입니다
    [SerializeField] int[] monsterID;//스폰될 몬스터의 ID입니다
    public List<int> monsterGroup = new List<int>();
    [SerializeField] GameObject bossObject;
    [SerializeField] GameObject MainMissionMaker;
    [SerializeField] GameObject SubMissionMaker;

    public Climate climate;
    public TileState tileState;

    public enum Climate
    {
        GOLDENPLACE,
        FORGOTTENFOREST,
        TEADELOSDESERT,
        WITCHSSWAMPLAND,
        VOLANO
    };

    public enum TileState
    {
        Default,
        SpawnTile,
        MonsterTile,
        BossTile,
        KingdomTile,
        VillageTile
    };

    void Awake()
    {
        kingdomObject.SetActive(false);
        vileageObject.SetActive(false);
        monsterObject.SetActive(false);
        bossObject.SetActive(false);
        monsterID = new int[10]
   {
        30000001, 30000002, 30000003, 30000004, 30000005,
        30000006,30000007, 30000020,30000023,30000024

   };//스폰될 몬스터의 ID입니다
    }

    private void Start()
    {
        FindAbjectTileVer2();

        tileUI = Map.instance.tileUI;

        if (tileState == TileState.SpawnTile)
        {
            isSpawnTile = true;
        }
        else if (tileState == TileState.BossTile)
        {
            isBossTile = true;
            bossObject.SetActive(true);
            monsterNum = 1;
            monsterGroup.Add(monsterID[9]);
        }
        else if (tileState == TileState.KingdomTile)
        {
            isKingdomTile = true;
            kingdomObject.SetActive(true);
        }
        else if (tileState == TileState.VillageTile)
        {
            isVillageTile = true;
            vileageObject.SetActive(true);
        }
        else
        {
            isSpawnTile = false;
            isMonsterTile = false;
            isBossTile = false;
            isKingdomTile = false;
            isVillageTile = false;
        }


    }
    public void SetMonsterValue()
    {
        if (tileState == TileState.MonsterTile && !isMonsterSet)
        {
            isMonsterTile = true;
            monsterObject.SetActive(true);
            if (climate == Climate.GOLDENPLACE)
            {
                monsterNum = UnityEngine.Random.Range(1, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[2]);
                }
                Instantiate(Map.instance.monsterList[2], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                isMonsterSet = true;
            }
            else if (climate == Climate.FORGOTTENFOREST)
            {
                monsterNum = UnityEngine.Random.Range(1, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[5]);
                }
                Instantiate(Map.instance.monsterList[5], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                isMonsterSet = true;
            }
            else if (climate == Climate.TEADELOSDESERT)
            {
                monsterNum = UnityEngine.Random.Range(2, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[6]);
                }
                Instantiate(Map.instance.monsterList[6], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                isMonsterSet = true;
            }
            else if (climate == Climate.WITCHSSWAMPLAND)
            {
                monsterNum = UnityEngine.Random.Range(2, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[7]);
                }
                Instantiate(Map.instance.monsterList[7], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                isMonsterSet = true;
            }
            else if (climate == Climate.VOLANO)
            {
                monsterNum = UnityEngine.Random.Range(2, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[8]);
                }
                Instantiate(Map.instance.monsterList[8], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                isMonsterSet = true;
            }
            else { isMonsterSet = true; }
        }
    }
    private void Update()
    {
        if (isMonsterTile && Map.instance.currentInteracteUITile == this && GameManager.instance.isVictory)
        {
            DestroyMonsterTile();
            GameManager.instance.isVictory = false;
        }
    }

    void FindAbjectTileVer2()
    {
        for (int tile = 0; tile < Map.instance.totalTileObjectList.Count; tile++)
        {
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x, 0, transform.position.z + 1.732051f))//상
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>());
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x + 1.5f, 0, transform.position.z + 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>());
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x + 1.5f, 0, transform.position.z - 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>());
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x, 0, transform.position.z - 1.732051f))//하
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>());
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
               new Vector3(transform.position.x - 1.5f, 0, transform.position.z - 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>());
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x - 1.5f, 0, transform.position.z + 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>());
            }
        }
    }

    #region 타일 선택
    public void InitializeSelect()
    {
        tileSelectImage[0].SetActive(false);
        tileSelectImage[1].SetActive(false);
    }
    public void TemporarySelection()
    {
        tileSelectImage[0].SetActive(true);
        tileSelectImage[1].SetActive(false);
    }
    public void ConfirmSelection()
    {
        tileSelectImage[0].SetActive(false);
        tileSelectImage[1].SetActive(true);
    }

    //List<Tile> RangeOfTileEvent(Tile tile)
    //{
    //    List<Tile> eventRange = new List<Tile>();
    //    for (int i = 0; i < tile.adjacentTiles.Count; i++)
    //    {
    //        eventRange.Add(tile.adjacentTiles[i]);
    //    }
    //    //eventRange.Add(tile.adjacentTiles[0].adjacentTiles[]);

    //    return eventRange;

    //}

    #endregion

    #region Maker
    public void MainMissionMarkerOnOff()
    {
        if (isMissionOn) { MainMissionMaker.SetActive(true); }
        else { MainMissionMaker.SetActive(false); }
    }

    public void SubMissionMarkerOnOff()
    {
        if (isMissionOn) { SubMissionMaker.SetActive(true); }
        else { SubMissionMaker.SetActive(false); }
    }
    #endregion

    public void SelectClimate(int ClimateNum)
    {
        switch (ClimateNum)
        {
            case 1:
                climate = Climate.GOLDENPLACE;
                material.material = climateMaterials[0];
                break;
            case 2:
                climate = Climate.FORGOTTENFOREST;
                material.material = climateMaterials[1];
                break;
            case 3:
                climate = Climate.TEADELOSDESERT;
                material.material = climateMaterials[2];
                break;
            case 4:
                climate = Climate.WITCHSSWAMPLAND;
                material.material = climateMaterials[3];
                break;
            case 5:
                climate = Climate.VOLANO;
                material.material = climateMaterials[4];
                break;
        }
    }

    public void MakeVilege()
    {
        vileageObject.SetActive(true);
    }

    public void DestroyKingdom()
    {
        isKingdomTile = false;
        Map.instance.isOutofUI = false;
        kingdomObject.SetActive(false);
        burnkingdomObject.SetActive(true);
        Map.instance.kingdomTile.Remove(this);
    }
    public void DestroyVilege()
    {
        isVillageTile = false;
        vileageObject.SetActive(false);
        burnVileageObject.SetActive(true);
    }
    public void DestroyMonsterTile()
    {
        isMonsterTile = false;
        monsterObject.SetActive(false);
        for (int i = 0; i < adjacentTiles.Count; i++)
        {
            adjacentTiles[i].tag = "Tile";
        }
        Map.instance.isOutofUI = false;
        //tileUI.OffMonsterBattle();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            player = col.gameObject.GetComponent<Character>();
            if (tileUI == null)
            {
                tileUI = Map.instance.tileUI;
            }
            //if (climate == Climate.GOLDENPLACE)
            //{
            //    Map.instance.mapUI.climateName.text = "황금 들판";
            //}
            //else if (climate == Climate.FORGOTTENFOREST)
            //{
            //    Map.instance.mapUI.climateName.text = "잊혀진 숲";
            //}
            //else if (climate == Climate.TEADELOSDESERT)
            //{
            //    Map.instance.mapUI.climateName.text = "타델로스 사막";
            //}
            //else if (climate == Climate.WITCHSSWAMPLAND)
            //{
            //    Map.instance.mapUI.climateName.text = "마녀의 늪지대";
            //}
            //else
            //{
            //    Map.instance.mapUI.climateName.text = "화산 기슭";
            //}

            if (Map.instance.startTile == null)
            {
                if (player.isMyturn)
                {
                    Map.instance.startTile = this;
                }
            }
            #region 미션용
            if (isKingdomTile && !Map.instance.isOutofUI && isMissionOn)
            {
                if (Map.instance.missionNum == 2)
                {
                    Map.instance.wolrdMission.mission.SetActive(true);
                    Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                }
                if (Map.instance.missionNum == 8)
                {
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    tileUI.Oncastle();
                    Map.instance.isOutofUI = true;
                }
                if (Map.instance.missionNum == 15)
                {
                    Map.instance.wolrdMission.mission.SetActive(true);
                    Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                }
                if (Map.instance.missionNum == 17)
                {
                    Map.instance.wolrdMission.mission.SetActive(true);
                    Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                }
                if (Map.instance.missionNum == 25)
                {
                    Map.instance.wolrdMission.mission.SetActive(true);
                    Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                }
            }
            if (isVillageTile && !Map.instance.isOutofUI && isMissionOn)
            {
                if (Map.instance.missionNum == 7)
                {
                    Map.instance.wolrdMission.mission.SetActive(true);
                    Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                }
            }
            if (Map.instance.missionNum == 9 && isMissionOn)
            {
                Map.instance.OnUIPlayerStop();
                Map.instance.wolrdMission.NextMission();
            }
            if (Map.instance.missionNum == 12 && isMissionOn)
            {
                Map.instance.wolrdMission.mission.SetActive(true);
                Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                Map.instance.isOutofUI = true;
            }
            if (Map.instance.missionNum == 13 && isMissionOn)
            {
                Map.instance.wolrdMission.mission.SetActive(true);
                Map.instance.wolrdMission.mission.GetComponent<Mission>().OnStartMission();
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                Map.instance.isOutofUI = true;
            }
            #endregion
        }
        if (col.CompareTag("Dragon"))
        {
            Map.instance.dragonStartTile = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Character>();
            tagPlayer = other.gameObject;
            if (Map.instance.startTile == null)
            {
                if (player.isMyturn)
                {
                    Map.instance.startTile = this;
                }
            }

            //if (gameObject.CompareTag("BattleRange"))//전투에 들어가는 범위
            //{
            //    if (tileUI.players[0] == null) { tileUI.players[0] = other.gameObject; tileUI.playerNumber = 1; }
            //    else if (tileUI.players[1] == null
            //        && tileUI.players[0].gameObject.name != other.gameObject.name)
            //    { tileUI.players[1] = other.gameObject; tileUI.playerNumber = 2; }
            //    else if (tileUI.players[2] == null
            //        && tileUI.players[0].gameObject.name != other.gameObject.name
            //        && tileUI.players[1].gameObject.name != other.gameObject.name)
            //    { tileUI.players[2] = other.gameObject; tileUI.playerNumber = 3; }
            //}

            if (Map.instance.isOutofUI && isKingdomTile || Map.instance.isOutofUI && isMonsterTile)
            {
                StartCoroutine(WaitExitUI());
            }
            if (isMonsterTile && !Map.instance.isOutofUI && !Map.instance.isPlayerMoving)
            {
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                if (Map.instance.currentMissionTile == this) { tileUI.missionMark.enabled = true; }
                else { tileUI.missionMark.enabled = false; }
                tileUI.OnMonsterBattle();
                tileUI.monsterName.text = Map.instance.GetMonsterName(monsterGroup[0]);
                Map.instance.isOutofUI = true;
                tileUI.monsterNumtext.text = monsterNum.ToString();
                if (monsterGroup[0] == monsterID[2]) { tileUI.monsterImage.sprite = tileUI.monsterSprites[0]; }
                else if (monsterGroup[0] == monsterID[5]) { tileUI.monsterImage.sprite = tileUI.monsterSprites[2]; }
                else if (monsterGroup[0] == monsterID[6]) { tileUI.monsterImage.sprite = tileUI.monsterSprites[3]; }
                else if (monsterGroup[0] == monsterID[7]) { tileUI.monsterImage.sprite = tileUI.monsterSprites[4]; }
                else if (monsterGroup[0] == monsterID[8]) { tileUI.monsterImage.sprite = tileUI.monsterSprites[5]; }


            }
            else if (isBossTile && !Map.instance.isOutofUI && !Map.instance.isPlayerMoving)
            {
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                if (Map.instance.currentMissionTile == this) { tileUI.missionMark.enabled = true; }
                else { tileUI.missionMark.enabled = false; }
                tileUI.OnMonsterBattle();
                tileUI.monsterName.text = Map.instance.GetMonsterName(monsterGroup[0]);
                Map.instance.isOutofUI = true;
                tileUI.monsterNumtext.text = monsterNum.ToString();
                tileUI.monsterImage.sprite = tileUI.monsterSprites[6];
            }
            else if (isKingdomTile && !Map.instance.isOutofUI && !isMissionOn)
            {
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                tileUI.Oncastle();
                Map.instance.isOutofUI = true;
            }
        }
        if (other.CompareTag("Dragon"))
        {
            Map.instance.dragonStartTile = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    IEnumerator WaitExitUI()
    {
        yield return new WaitUntil(() => !Map.instance.isOutofUI);
        tagPlayer.transform.rotation =
            Quaternion.LookRotation(new Vector3(0, 0, adjacentTiles[0].transform.position.z) -
            new Vector3(0, 0, tagPlayer.transform.position.z)).normalized;
        tagPlayer.transform.position = Vector3.MoveTowards(tagPlayer.transform.position, new Vector3(
             adjacentTiles[0].gameObject.transform.position.x,
            adjacentTiles[0].gameObject.transform.position.y,
             adjacentTiles[0].gameObject.transform.position.z), 0.05f);
        if (Vector3.Distance(adjacentTiles[0].transform.position, tagPlayer.transform.position) <= 0.05f)
        {
            Map.instance.startTile = adjacentTiles[0];
            Map.instance.pathTileObjectList.Clear();
            Map.instance.isPlayerOnEndTile = true;
            Map.instance.currentInteracteUITile = null;
            Map.instance.isOutofUI = false;
        }
    }
}
