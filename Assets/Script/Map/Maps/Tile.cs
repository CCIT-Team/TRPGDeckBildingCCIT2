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

    /// <summary>
    /// Sum of G and H.
    /// </summary>
    public int F => g + h;

    /// <summary>
    /// Cost from start tile to this tile.
    /// </summary>
    public int g;

    /// <summary>
    /// Estimated cost from this tile to destination tile.
    /// </summary>
    public int h;

    /// <summary>
    /// Tile's coordinates.
    /// </summary>
    public Vector3Int position;

    /// <summary>
    /// References to all adjacent tiles.
    /// </summary>
    public List<Tile> adjacentTiles = new List<Tile>(6);

    /// <summary>
    /// If true - Tile is an obstacle impossible to pass.
    /// </summary>
    /// 
    public bool isObstacle;

    public bool isSpawnTile = false;

    public bool isMonsterTile = false;

    public bool isBossTile = false;

    public bool isKingdomTile = false;

    public bool isVillageTile = false;

    public bool isSelect = false;

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
    [SerializeField] GameObject bossObject;

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
        if (tileState == TileState.SpawnTile)
        {
            isSpawnTile = true;
        }
        else if (tileState == TileState.MonsterTile)
        {
            isMonsterTile = true;
            monsterObject.SetActive(true);
            if(climate == Climate.GRASS)
            {
                //GameManager.instance.MonsterMapInstance(Map.instance.monsterIDList[UnityEngine.Random.Range(0,3)],monsterPosition.position);
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(0, 3)], monsterPosition.position,Quaternion.identity);
            }
            else if(climate == Climate.DESERT)
            {
                //GameManager.instance.MonsterMapInstance(Map.instance.monsterIDList[UnityEngine.Random.Range(2, 4)], monsterPosition.position);
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(2, 4)], monsterPosition.position, Quaternion.identity);
            }
            else
            {
                //GameManager.instance.MonsterMapInstance(Map.instance.monsterIDList[UnityEngine.Random.Range(3, 5)], monsterPosition.position);
                Instantiate(Map.instance.monsterList[UnityEngine.Random.Range(3, 5)], monsterPosition.position, Quaternion.identity);
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
        if (tiles.Count < 7)
        {
            if (col.CompareTag("Tile")) { tiles.Add(col.gameObject); }
            tiles = tiles.Distinct().ToList();
            foreach (GameObject tile in tiles)
            {
                Tile tiletile = tile.GetComponent<Tile>();
                adjacentTiles.Add(tiletile.GetComponent<Tile>());
                adjacentTiles = adjacentTiles.Distinct().ToList();
            }
        }

        if (col.CompareTag("Player"))
        {
            player = col.gameObject.GetComponent<Character>();

            if (Map.instance.startTile == null)
            {
                if (player.isMyturn)
                {
                    Map.instance.startTile = this;
                    isSelect = true;
                }
            }

            if (isSpawnTile)
            {

            }
            else if (isMonsterTile && !Map.instance.isBattle && !Map.instance.isPlayerMoving)
            {
                //GameManager.instance.LoadScenceName("New Battle");
                //Map.instance.isBattle = true;
                //Debug.Log("전투진입");
                tileUI.OnMonsterBattle();
                Map.instance.isOutofUI = true;
            }
            else if (isBossTile && !Map.instance.isPlayerMoving)
            {
                tileUI.OnMonsterBattle();
                Map.instance.isOutofUI = true;
            }
            else if (isKingdomTile && !Map.instance.isPlayerMoving)
            {
                tileUI.OnShopAndHospital();
                Map.instance.isOutofUI = true;
            }
            else if (isVillageTile)
            {

            }
            else
            {

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
                //Map.instance.wolrdTurn.currentPlayer.transform.position
                //tagPlayer.transform.LookAt(adjacentTiles[0].transform.position);
                //tagPlayer.transform.Translate(new Vector3(adjacentTiles[0].gameObject.transform.position.x,
                //    0, adjacentTiles[0].gameObject.transform.position.z) * Time.deltaTime * 0.1f, Space.Self);
                //if (Vector3.Distance(adjacentTiles[0].transform.position, tagPlayer.transform.position) <= 0.1f)
                //{
                //    Map.instance.wolrdTurn.currentPlayer.isMyturn = false;
                //    Map.instance.startTile = null;
                //    Map.instance.pathTileObjectList.Clear();
                //    Map.instance.isPlayerOnEndTile = true;
                //    Map.instance.isOutofUI = false;
                //}
            }
            if (isSpawnTile)
            {

            }
            else if (isMonsterTile && !Map.instance.isBattle && !Map.instance.isPlayerMoving)
            {
                //GameManager.instance.LoadScenceName("New Battle");
                //Map.instance.isBattle = true;
                //Debug.Log("전투진입");
                tileUI.OnMonsterBattle();
                Map.instance.isOutofUI = true;
            }
            else if (isKingdomTile && !Map.instance.isPlayerMoving)
            {
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
            Map.instance.startTile = null;
            Map.instance.pathTileObjectList.Clear();
            Map.instance.isPlayerOnEndTile = true;
            Map.instance.isOutofUI = false;
        }
    }
}
