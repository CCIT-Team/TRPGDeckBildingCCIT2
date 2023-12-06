using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileSelector : MonoBehaviour
{
    Tile startPoint;
    Tile endPoint;
    List<Tile> tilePath;


    bool isEndTileSelect = false;

    [SerializeField] private Camera m_camera;
    public AStarPathfinding astar = new AStarPathfinding();
    public Map map;

    private void Start()
    {
        map = Map.instance;
    }

    enum MouseButton
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    private void Update()
    {
        var mouseButton = Input.GetMouseButtonDown(0) ? MouseButton.Left : Input.GetMouseButtonDown(1) ? MouseButton.Right : MouseButton.None;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        Tile tile = Raycast(ray);
        if (mouseButton == MouseButton.None)
        {
            if (tile != null)
            {
                Tile tiles = tile;
                if (startPoint == null && map.startTile != null)
                {
                    startPoint = map.startTile;
                }
                if (!isEndTileSelect && !Map.instance.isOutofUI && Map.instance.wolrdTurn.currentPlayer.isMyturn && !Map.instance.dragonScript.isdragonTurn)
                {
                    if (endPoint == null)
                    {
                        endPoint = tiles;
                        tilePath = astar.FindPath(startPoint, endPoint);

                        tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
                        int walkNum = 0;
                        //foreach (Tile game in tilePath)
                        //{
                        if (Map.instance.wolrdTurn.currentPlayer.cost + 1 < tilePath.Count)
                        {
                            for (int i = 0; i < Map.instance.wolrdTurn.currentPlayer.cost + 1; i++)
                            {
                                //tilePath[i].gameObject.GetComponent<MeshRenderer>().material;
                                tilePath[i].TemporarySelection();
                                tilePath[i].walkAbleNumText.text = walkNum.ToString();
                                tilePath[0].walkAbleNumText.text = "";
                                walkNum += 1;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < tilePath.Count; i++)
                            {
                                tilePath[i].TemporarySelection();
                                tilePath[i].walkAbleNumText.text = walkNum.ToString();
                                tilePath[0].walkAbleNumText.text = "";
                                walkNum += 1;
                            }
                        }
                    }
                    if (endPoint != tiles && tilePath != null)
                    {
                        foreach (Tile game in tilePath)
                        {
                            game.InitializeSelect();
                            game.walkAbleNumText.text = "";
                        }
                        tilePath.Clear();
                        endPoint = null;
                    }
                }
            }
        }
        if (mouseButton == MouseButton.Left && !isEndTileSelect && !Map.instance.isOutofUI && Map.instance.wolrdTurn.currentPlayer.isMyturn && !Map.instance.dragonScript.isdragonTurn)
        {
            if (endPoint != null && startPoint != endPoint)
            {
                isEndTileSelect = true;
                tilePath.Clear();
                tilePath = astar.FindPath(startPoint, endPoint);
                tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
                if (Map.instance.wolrdTurn.currentPlayer.cost + 1 < tilePath.Count)
                {
                    endPoint = tilePath[Map.instance.wolrdTurn.currentPlayer.cost];
                    for (int i = 0; i < Map.instance.wolrdTurn.currentPlayer.cost + 1; i++)
                    {
                        tilePath[i].ConfirmSelection();
                        map.PlayerMovePath(tilePath[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < tilePath.Count; i++)
                    {
                        tilePath[i].ConfirmSelection();
                        map.PlayerMovePath(tilePath[i]);
                    }
                }
            }
        }
        if (map.isPlayerOnEndTile && tilePath != null)
        {
            foreach (Tile game in tilePath)
            {
                game.InitializeSelect();
                game.walkAbleNumText.text = "";
            }
            Debug.Log("Clear Path List");
            tilePath.Clear();
            startPoint = null;
            endPoint = null;
            map.isPlayerOnEndTile = false;
            isEndTileSelect = false;
        }
    }

    private Tile Raycast(Ray ray)
    {
        Tile result = default(Tile);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            result = hit.transform.GetComponent<Tile>();
        }
        return result;
    }
}


