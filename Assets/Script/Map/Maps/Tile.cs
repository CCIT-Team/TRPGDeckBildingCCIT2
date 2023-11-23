using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Tile : MonoBehaviour
{
    public TileUI tileUI;
    public List<GameObject> tiles = new List<GameObject>(6);
    public Material[] climateMaterials = new Material[3];
    public TMP_Text walkAbleNumText;
    public int F => g + h;

    public int g;

    public int h;

    public int rayDistance;

    public Vector3Int position;

    public Vector3Int[] adjacentTilesPos;

    public List<Tile> adjacentTiles = new List<Tile>(6);

    bool isfindNeiber = false;

    public bool isObstacle;

    public bool isSpawnTile = false;

    public bool isMonsterTile = false;

    public bool isBossTile = false;

    public bool isKingdomTile = false;

    public bool isVillageTile = false;

    public bool isSelect = false;

    public bool isMissionOn = false;

    Character player;
    GameObject tagPlayer;
    GameObject dragon;

    [SerializeField] MeshRenderer material;
    Color defaultColor;

    [SerializeField] GameObject kingdomObject;
    [SerializeField] GameObject burnkingdomObject;
    [SerializeField] GameObject vileageObject;
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
        adjacentTilesPos = new Vector3Int[]
    {
        new Vector3Int(0,0,1),//상0
        new Vector3Int(1,0,0),//우상1
        new Vector3Int(1,0,-1),//우하2
        new Vector3Int(0,0,-1),//하3
        new Vector3Int(-1,0,-1),//좌하4
        new Vector3Int(-1,0,0),//좌상5
        new Vector3Int(0,0,1),//상 보정6
        new Vector3Int(1,0,1),//우상 보정7
        new Vector3Int(1,0,0),//우하 보정8
        new Vector3Int(0,0,-1),//하 보정9
        new Vector3Int(-1,0,0),//좌하 보정10
        new Vector3Int(-1,0,1)//좌상 보정11
    };
    }

    private void Start()
    {
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
                }
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(0, 3)], monsterPosition);
                GameManager.instance.SetBattleMonsterSetting(monsterGroup);
            }
            else if (climate == Climate.DESERT)
            {
                monsterNum = UnityEngine.Random.Range(2, 4);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[UnityEngine.Random.Range(2, 4)]);
                }
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(2, 4)], monsterPosition);
                GameManager.instance.SetBattleMonsterSetting(monsterGroup);
            }
            else
            {
                monsterNum = UnityEngine.Random.Range(2, 6);
                for (int i = 0; i < monsterNum; i++)
                {
                    monsterGroup.Add(monsterID[UnityEngine.Random.Range(3, 5)]);
                }
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(3, 5)], monsterPosition);
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            adjacentTiles.Clear();
            FindAbjectTile();
        }
        if(!isfindNeiber)
        {
            FindAbjectTile();
            adjacentTiles.Clear();
            FindAbjectTile();
            isfindNeiber = true;
        }
    }

    void FindAbjectTile()//인접타일 확인
    {
        RaycastHit ray0;

        Debug.DrawRay(transform.position, transform.forward * 1, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out ray0, rayDistance))//상
        {
            if (ray0.transform.CompareTag("Tile"))
            {
                if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[0] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
                else if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[6] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
            }
            else
            {
                Debug.Log("Nothing Here" + " My Name is" + gameObject.name);
            }
        }
        Debug.DrawRay(transform.position, transform.right + transform.forward, Color.red);
        if (Physics.Raycast(transform.position, transform.right + transform.forward, out ray0, rayDistance))//우상
        {
            if (ray0.transform.CompareTag("Tile"))
            {
                if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[1] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
                else if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[7] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
            }
            else
            {
                Debug.Log("Nothing Here" + " My Name is" + gameObject.name);
            }
        }
        Debug.DrawRay(transform.position, transform.right + (transform.forward * -1), Color.red);
        if (Physics.Raycast(transform.position, transform.right + (transform.forward * -1), out ray0, rayDistance))//우하
        {
            if (ray0.transform.CompareTag("Tile"))
            {
                if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[2] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
                else if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[8] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
            }
            else
            {
                Debug.Log("Nothing Here" + " My Name is" + gameObject.name);
            }
        }
        Debug.DrawRay(transform.position, transform.forward * -1, Color.red);
        if (Physics.Raycast(transform.position, transform.forward * -1, out ray0, rayDistance))//하
        {
            if (ray0.transform.CompareTag("Tile"))
            {
                if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[3] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
                else if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[9] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
            }
            else
            {
                Debug.Log("Nothing Here" + " My Name is" + gameObject.name);
            }
        }
        Debug.DrawRay(transform.position, (transform.right * -1) + (transform.forward * -1), Color.red);
        if (Physics.Raycast(transform.position, (transform.right * -1) + (transform.forward * -1), out ray0, rayDistance))//좌하
        {
            if (ray0.transform.CompareTag("Tile"))
            {
                if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[4] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
                else if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[10] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
            }
            else
            {
                Debug.Log("Nothing Here" + " My Name is" + gameObject.name);
            }
        }
        Debug.DrawRay(transform.position, (transform.right * -1) + transform.forward, Color.red);
        if (Physics.Raycast(transform.position, (transform.right * -1) + transform.forward, out ray0, rayDistance))//좌상
        {
            if (ray0.transform.CompareTag("Tile"))
            {
                if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[5] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
                else if (ray0.transform.GetComponent<Tile>().position == adjacentTilesPos[11] + position)
                {
                    adjacentTiles.Add(ray0.transform.GetComponent<Tile>());
                }
            }
            else
            {
                Debug.Log("Nothing Here" + " My Name is" + gameObject.name);
            }
        }
    }

    public void IsSelect(Color color)
    {
        if (isSelect)
        {
            material.material = gameObject.GetComponent<MeshRenderer>().material;
            material.material.color = color;
        }
        else
        {
            material.material = gameObject.GetComponent<MeshRenderer>().material;
            material.material.color = defaultColor;
        }
    }

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

    public void DestroyKingdom()
    {
        isKingdomTile = false;
        kingdomObject.SetActive(false);
        burnkingdomObject.SetActive(true);
    }

    public void DestroyMonsterTile()
    {
        isMonsterTile = false;
        monsterObject.SetActive(false);
    }

    //public void MakeKingdom()
    //{
    //    if (isKingdomTile)
    //    {
    //        tileState = TileState.KingdomTile;
    //        kingdomObject.SetActive(true);
    //        for (int i = 0; i < adjacentTiles.Count; i++)
    //        {
    //            adjacentTiles[i].climate = climate;
    //            adjacentTiles[i].material.material = material.material;
    //            if (adjacentTiles[i].climate != climate)
    //            {
    //                for (int j = 0; j < adjacentTiles.Count; j++)
    //                {
    //                    Debug.Log(adjacentTiles.Count + name);
    //                    adjacentTiles[j].climate = climate;
    //                    adjacentTiles[j].material.material = material.material;
    //                }
    //            }
    //        }
    //    }
    //}

    //public void MakeClimate()
    //{
    //    for (int i = 0; i < adjacentTiles.Count; i++)
    //    {
    //        Debug.Log(adjacentTiles.Count + name);
    //        adjacentTiles[i].climate = climate;
    //        adjacentTiles[i].material.material = material.material;
    //    }
    //}

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
                    isSelect = true;
                }
            }
            if (isKingdomTile && !Map.instance.isOutofUI && isMissionOn)
            {
                //Map.instance.wolrdMission.missionCleard = true;
                //isMissionOn = false;
                //MissionMarkerOnOff();
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                Map.instance.isOutofUI = true;
                Map.instance.wolrdMission.firstMainMission.SetActive(true);
            }
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
                    isSelect = true;
                }
            }
            if (Map.instance.isOutofUI && isKingdomTile || Map.instance.isOutofUI && isMonsterTile)
            {
                StartCoroutine(WaitExitUI());
            }
            else if (isMonsterTile && !Map.instance.isOutofUI && !Map.instance.isPlayerMoving)
            {
                Map.instance.currentInteracteUITile = this;
                Map.instance.OnUIPlayerStop();
                tileUI.OnMonsterBattle();
                for (int i = 0; i < monsterGroup.Count; i++)
                {
                    tileUI.monsterNum[i].SetActive(true);
                }
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
            else if (isKingdomTile && !Map.instance.isOutofUI && !isMissionOn && !Map.instance.isPlayerMoving)
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
            Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
            Map.instance.pathTileObjectList.Clear();
            Map.instance.startTile = adjacentTiles[0];
            isSelect = true;
            Map.instance.isPlayerOnEndTile = true;
            Map.instance.isOutofUI = false;
        }
    }
}
