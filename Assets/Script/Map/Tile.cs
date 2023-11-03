using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Tile : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>(6);
    public Material[] climateMaterials = new Material[3];
    public TMP_Text walkAbleNumText;


    Character player;
    Material material;
    Color defaultColor;

    [SerializeField] GameObject kingdomObject;
    [SerializeField] GameObject vileageObject;
    [SerializeField] GameObject monsterObject;
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
    public bool isObstacle;

    public bool isSpawnTile = false;

    public bool isMonsterTile = false;

    public bool isBossTile = false;

    public bool isKingdomTile = false;

    public bool isVillageTile = false;

    public bool isSelect = false;

    public void IsSelect(Color color)
    {
        if (isSelect)
        {
            material = gameObject.GetComponent<MeshRenderer>().material;
            material.color = color;
        }
        else
        {
            material = gameObject.GetComponent<MeshRenderer>().material;
            material.color = defaultColor;
        }
    }

    public void SelectClimate(int ClimateNum)
    {
        switch (ClimateNum)
        {
            case 0:
                climate = Climate.GRASS;
                material = climateMaterials[ClimateNum];
                break;
            case 1:
                climate = Climate.DESERT;
                material = climateMaterials[ClimateNum];
                break;
            case 2:
                climate = Climate.JUNGLE;
                material = climateMaterials[ClimateNum];
                break;
        }
    }

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
            else if (isMonsterTile && !Map.instance.isBattle)
            {
                //Map.instance.ChangeScene(4);
                GameManager.instance.LoadScenceName("New Battle");
                Map.instance.isBattle = true;
                Debug.Log("전투진입");
            }
            else if (isBossTile)
            {

            }
            else if (isKingdomTile)
            {

            }
            else if (isVillageTile)
            {

            }
            else
            {

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Character>();

            if (Map.instance.startTile == null)
            {
                if (player.isMyturn)
                {
                    Map.instance.startTile = this;
                    isSelect = true;
                }
            }
        }
    }
}
