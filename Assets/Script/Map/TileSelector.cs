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
                if (startPoint == null)
                {
                    startPoint = map.startTile;
                    startPoint.isSelect = true;
                    startPoint.IsSelect(Color.red);
                }
                if (!isEndTileSelect)
                {
                    if (endPoint == null)
                    {
                        endPoint = tiles;
                        tilePath = astar.FindPath(startPoint, endPoint);

                        tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
                        int walkNum = 0;
                        //foreach (Tile game in tilePath)
                        //{
                        if(Map.instance.wolrdTurn.currentPlayer.cost + 1 < tilePath.Count)
                        {
                            for (int i = 0; i < Map.instance.wolrdTurn.currentPlayer.cost + 1; i++)
                            {
                                Material material = tilePath[i].gameObject.GetComponent<MeshRenderer>().material;
                                material.color = Color.red;
                                tilePath[i].GetComponent<Tile>().walkAbleNumText.text = walkNum.ToString();
                                tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
                                walkNum += 1;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < tilePath.Count; i++)
                            {
                                Material material = tilePath[i].gameObject.GetComponent<MeshRenderer>().material;
                                material.color = Color.red;
                                tilePath[i].GetComponent<Tile>().walkAbleNumText.text = walkNum.ToString();
                                tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
                                walkNum += 1;
                            }
                        }

                        //Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                        //material.color = Color.red;
                        //game.GetComponent<Tile>().walkAbleNumText.text = walkNum.ToString();
                        //tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
                        //walkNum += 1;
                        //}
                    }
                    if (endPoint != tiles)
                    {
                        foreach (Tile game in tilePath)
                        {
                            Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                            material.color = Color.white;

                            game.GetComponent<Tile>().walkAbleNumText.text = "";
                        }
                        tilePath.Clear();
                        endPoint = null;
                    }
                }
            }
        }
        if (mouseButton == MouseButton.Right)
        {
            isEndTileSelect = true;
            endPoint.isSelect = true;
            tilePath.Clear();
            tilePath = astar.FindPath(startPoint, endPoint);
            tilePath[0].GetComponent<Tile>().walkAbleNumText.text = "";
            if (Map.instance.wolrdTurn.currentPlayer.cost + 1 < tilePath.Count)
            {
                endPoint = tilePath[Map.instance.wolrdTurn.currentPlayer.cost];
                endPoint.IsSelect(Color.blue);
                for (int i = 0; i < Map.instance.wolrdTurn.currentPlayer.cost + 1; i++)
                {
                    Material material = tilePath[i].gameObject.GetComponent<MeshRenderer>().material;
                    material.color = Color.cyan;
                    map.PlayerMovePath(tilePath[i]);
                }
            }
            else
            {
                endPoint.IsSelect(Color.blue);
                for (int i = 0; i < tilePath.Count; i++)
                {
                    Material material = tilePath[i].gameObject.GetComponent<MeshRenderer>().material;
                    material.color = Color.cyan;
                    map.PlayerMovePath(tilePath[i]);
                }
            }
        }
        if (map.isPlayerOnEndTile)
        {
            foreach (Tile game in tilePath)
            {
                Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                game.GetComponent<Tile>().walkAbleNumText.text = "";
                material.color = Color.white;
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


