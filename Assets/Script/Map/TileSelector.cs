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
    public AstarPath astarPath;
    public Maps map;

    enum MouseButton
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    private void Update()
    {
        var mouseButton = Input.GetMouseButtonDown(0) ? MouseButton.Left : Input.GetMouseButtonDown(1) ? MouseButton.Right : MouseButton.None;
        if (mouseButton == MouseButton.None)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            Tile tile = Raycast(ray);
            if (tile != null)
            {
                Tile tiles = tile;
                //if (mouseButton == MouseButton.Left)
                //{
                //    if (startPoint == null)
                //    {
                //        startPoint = tiles;
                //        startPoint.isSelect = true;
                //        startPoint.IsSelect(Color.red);
                //        Debug.Log("StartTile!" + startPoint.gameObject.name);
                //        astarPath.OnStartCellSelect(startPoint);
                //    }
                //    // List<Tile> tilePath = AStarPathfinding.FindPath(startPoint, endPoint);
                //}
                if (startPoint == null)
                {
                    startPoint = map.startTile;
                    startPoint.isSelect = true;
                    startPoint.IsSelect(Color.red);
                    Debug.Log("StartTile!" + startPoint.gameObject.name);
                    astarPath.OnStartCellSelect(startPoint);
                }
                if (startPoint != null && !isEndTileSelect)
                {
                    if (endPoint == null)
                    {
                        endPoint = tiles;
                        //endPoint.isSelect = true;
                        //endPoint.IsSelect(Color.blue);
                        Debug.Log("EndTile!" + endPoint.gameObject.name);
                        astarPath.OnEndCellSelect(endPoint);
                        tilePath = astar.FindPath(startPoint, endPoint);
                        foreach (Tile game in tilePath)
                        {
                            Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                            material.color = Color.red;
                        }
                        astarPath.OnFindPath(tilePath);
                    }
                    if (endPoint != tiles)
                    {
                        foreach (Tile game in tilePath)
                        {
                            Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                            material.color = Color.white;
                        }
                        tilePath.Clear();
                        if (!isEndTileSelect) { endPoint = null; }
                    }
                }
                //if (mouseButton == MouseButton.Right)
                //{
                //    isEndTileSelect = true;
                //    endPoint = tiles;
                //    endPoint.isSelect = true;
                //    endPoint.IsSelect(Color.blue);
                //    Debug.Log("EndTile!" + endPoint.gameObject.name);
                //    astarPath.OnEndCellSelect(endPoint);

                //    if (startPoint != null)
                //    {
                //        tilePath = astar.FindPath(startPoint, endPoint);
                //        foreach (Tile game in tilePath)
                //        {
                //            Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                //            material.color = Color.cyan;
                //            map.PlayerMovePath(game);
                //        }
                //        astarPath.OnFindPath(tilePath);
                //    }
                //}
            }
        }
        if (mouseButton == MouseButton.Right)
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            Tile tile = Raycast(ray);
            Tile tiles = tile;
            isEndTileSelect = true;
            endPoint = tiles;
            endPoint.isSelect = true;
            endPoint.IsSelect(Color.blue);
            Debug.Log("EndTile!" + endPoint.gameObject.name);
            astarPath.OnEndCellSelect(endPoint);
            startPoint = null;
            if (startPoint == null)
            {
                startPoint = map.startTile;
                startPoint.isSelect = true;
                startPoint.IsSelect(Color.red);
                Debug.Log("StartTile!" + startPoint.gameObject.name);
                astarPath.OnStartCellSelect(startPoint);
            }
            if (startPoint != null)
            {
                tilePath = astar.FindPath(startPoint, endPoint);
                foreach (Tile game in tilePath)
                {
                    Material material = game.gameObject.GetComponent<MeshRenderer>().material;
                    material.color = Color.cyan;
                    map.PlayerMovePath(game);
                }
                astarPath.OnFindPath(tilePath);
            }
        }
        if (map.isPlayerOnEndTile)
        {
            foreach (Tile game in tilePath)
            {
                Material material = game.gameObject.GetComponent<MeshRenderer>().material;
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


