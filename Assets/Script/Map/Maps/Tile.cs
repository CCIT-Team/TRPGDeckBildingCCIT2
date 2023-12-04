using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Tile : MonoBehaviour
{
    public TileUI tileUI;

    public Material[] climateMaterials = new Material[3];

    public GameObject[] tileSelectImage = new GameObject[2];//0 = Select 1 = SelectBold
    public TMP_Text walkAbleNumText;
    public int F => g + h;

    public int g;

    public int h;

    public int rayDistance;

    public Vector3Int position;

    public List<GameObject> adjacentTilesObject = new List<GameObject>(6);

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

    [SerializeField] MeshRenderer material;
    Color defaultColor;

    [SerializeField] GameObject kingdomObject;
    [SerializeField] GameObject burnkingdomObject;
    [SerializeField] GameObject vileageObject;
    [SerializeField] GameObject burnVileageObject;
    [SerializeField] GameObject monsterObject;
    [SerializeField] Transform monsterPosition;
    [SerializeField] int monsterNum;//스폰된 몬스터의 마릿수입니다
    [SerializeField] int[] monsterID = new int[] { 30000001, 30000002, 30000003, 30000004, 30000005 };//스폰될 몬스터의 ID입니다
    [SerializeField] List<int> monsterGroup = new List<int>();
    [SerializeField] GameObject bossObject;
    [SerializeField] GameObject MainMissionMaker;
    [SerializeField] GameObject SubMissionMaker;

    public Climate climate;
    public TileState tileState;

    public enum Climate
    {
        GRASS,
        DESERT,
        JUNGLE
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
        defaultColor = GetComponent<MeshRenderer>().material.color;
        kingdomObject.SetActive(false);
        vileageObject.SetActive(false);
        monsterObject.SetActive(false);
        bossObject.SetActive(false);
    }

    private void Start()
    {
        FindAbjectTileVer2();
        if (tileState == TileState.SpawnTile)
        {
            isSpawnTile = true;
        }
        else if (tileState == TileState.MonsterTile)
        {
            isMonsterTile = true;
            monsterObject.SetActive(true);
            if (climate == Climate.GRASS)
            {
                monsterNum = UnityEngine.Random.Range(1, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[UnityEngine.Random.Range(0, 3)]);
                    tileUI.monsterNum[i].SetActive(true);
                }
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(0, 3)], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                GameManager.instance.SetBattleMonsterSetting(monsterGroup);
            }
            else if (climate == Climate.DESERT)
            {
                monsterNum = UnityEngine.Random.Range(2, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[UnityEngine.Random.Range(2, 4)]);
                    tileUI.monsterNum[i].SetActive(true);
                }
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(2, 4)], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                GameManager.instance.SetBattleMonsterSetting(monsterGroup);
            }
            else
            {
                monsterNum = UnityEngine.Random.Range(2, 6);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[UnityEngine.Random.Range(3, 5)]);
                    tileUI.monsterNum[i].SetActive(true);
                }
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(3, 5)], monsterPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)), monsterPosition);
                GameManager.instance.SetBattleMonsterSetting(monsterGroup);
            }
        }
        else if (tileState == TileState.BossTile)
        {
            isBossTile = true;
            bossObject.SetActive(true);
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

    private void Update()
    {

    }

    void FindAbjectTileVer2()
    {
        for (int tile = 0; tile < Map.instance.totalTileObjectList.Count; tile++)
        {
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x, 0, transform.position.z + 1.732051f))//상
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>()); ;
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x + 1.5f, 0, transform.position.z + 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>()); ;
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x + 1.5f, 0, transform.position.z - 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>()); ;
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x, 0, transform.position.z - 1.732051f))//하
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>()); ;
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
               new Vector3(transform.position.x - 1.5f, 0, transform.position.z - 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>()); ;
            }
            if (Map.instance.totalTileObjectList[tile].gameObject.transform.position ==
                new Vector3(transform.position.x - 1.5f, 0, transform.position.z + 0.8660254f))
            {
                adjacentTiles.Add(Map.instance.totalTileObjectList[tile].gameObject.GetComponent<Tile>()); ;
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
                climate = Climate.GRASS;
                material.material = climateMaterials[0];
                break;
            case 2:
                climate = Climate.DESERT;
                material.material = climateMaterials[1];
                break;
            case 3:
                climate = Climate.JUNGLE;
                material.material = climateMaterials[2];
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
        kingdomObject.SetActive(false);
        burnkingdomObject.SetActive(true);
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
        tileUI.OffMonsterBattle();
        monsterObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            player = col.gameObject.GetComponent<Character>();

            if (climate == Climate.GRASS)
            {
                Map.instance.mapUI.climateName.text = "초원";
            }
            else if (climate == Climate.DESERT)
            {
                Map.instance.mapUI.climateName.text = "사막";
            }
            else
            {
                Map.instance.mapUI.climateName.text = "정글";
            }

            if (Map.instance.startTile == null)
            {
                if (player.isMyturn)
                {
                    Map.instance.startTile = this;
                }
            }
            //미션 관련
            if (isKingdomTile && !Map.instance.isOutofUI && isMissionOn)
            {
                if (Map.instance.wolrdMission.mainMissionNum == 1)
                {
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                    Map.instance.wolrdMission.secondMainMission.SetActive(true);
                }
                if (Map.instance.wolrdMission.mainMissionNum == 7)
                {
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    tileUI.OnShopAndHospital();
                    Map.instance.isOutofUI = true;
                }
            }
            if (isVillageTile && !Map.instance.isOutofUI && isMissionOn)
            {
                if (Map.instance.wolrdMission.mainMissionNum == 6)
                {
                    Map.instance.currentInteracteUITile = this;
                    Map.instance.OnUIPlayerStop();
                    Map.instance.isOutofUI = true;
                    Map.instance.wolrdMission.seventhdMainMission.SetActive(true);
                }
            }
            if (Map.instance.wolrdMission.mainMissionNum == 8 && isMissionOn)
            {
                Map.instance.OnUIPlayerStop();
                Map.instance.wolrdMission.mainMissionNum = 9;
            }
            //
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
            }
            else if (isBossTile && !Map.instance.isOutofUI && !Map.instance.isPlayerMoving)
            {
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                tileUI.OnMonsterBattle();
                Map.instance.isOutofUI = true;
            }
            else if (isKingdomTile && !Map.instance.isOutofUI && !isMissionOn)
            {
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                tileUI.OnShopAndHospital();
                Map.instance.isOutofUI = true;
            }
        }
        if (other.CompareTag("Dragon"))
        {
            Map.instance.dragonStartTile = this;
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
            0.5f,
             adjacentTiles[0].gameObject.transform.position.z), 0.05f);
        if (Vector3.Distance(adjacentTiles[0].transform.position, tagPlayer.transform.position) <= 0.1f)
        {
            Map.instance.startTile = adjacentTiles[0];
            Map.instance.pathTileObjectList.Clear();
            Map.instance.isPlayerOnEndTile = true;
            Map.instance.currentInteracteUITile = null;
            Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
            Map.instance.isOutofUI = false;
        }
    }
}
