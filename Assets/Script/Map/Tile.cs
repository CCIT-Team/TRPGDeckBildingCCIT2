using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tile : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>(6);
    int i = 0;
    Material material;
    Color defaultColor;

     void Awake()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
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
            Maps.instance.startTile = this;
            if (Maps.instance.startTile = this) { Debug.Log("Find Player"); }
        }
    }
}
