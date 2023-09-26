using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public IList<Cell> FindPathOnMap(Cell cellStart, Cell cellEnd, Map1 map)
    {
        List<Cell> findPath = new List<Cell>();
        if (cellStart == null || cellEnd == null) return findPath;

        Vector2Int mapSize = map.GetMapSize();
        IDictionary<Vector2, Cell> mapDic = map.GetCells();

        HashSet<Cell> opened = new HashSet<Cell>();
        HashSet<Cell> closed = new HashSet<Cell>();

        opened.Add(cellStart);

        List<Vector2> moveList = new List<Vector2>(8);
        bool isFindEnd = false;

        Rect mapRect = new Rect(0, 0, mapSize.x, mapSize.y);

        while (opened.Count > 0)
        {
            var minOpened = opened.OrderBy(x => x.Summ).First();
            closed.Add(minOpened);
            opened.Remove(minOpened);

            if (minOpened.Point.y % 2 != 0)//Y가 홀수일때
            {
                //Vector2 n1 = new Vector2(minOpened.Point.x, minOpened.Point.y - ((Mathf.Sqrt(3)/2)/2));
                //Vector2 n2 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y - (Mathf.Sqrt(3)/2));
                //Vector2 n3 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y);
                //Vector2 n4 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y);
                //Vector2 n5 = new Vector2(minOpened.Point.x, minOpened.Point.y + (Mathf.Sqrt(3)/2));
                //Vector2 n6 = new(minOpened.Point.x + 1, minOpened.Point.y + (Mathf.Sqrt(3)/2));
                Vector2 n1 = new Vector2(minOpened.Point.x, minOpened.Point.y - 1);
                Vector2 n2 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y - 1);
                Vector2 n3 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y);
                Vector2 n4 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y);
                Vector2 n5 = new Vector2(minOpened.Point.x, minOpened.Point.y + 1);
                Vector2 n6 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y + 1);
                moveList.Add(n1);
                moveList.Add(n2);
                moveList.Add(n3);
                moveList.Add(n4);
                moveList.Add(n5);
                moveList.Add(n6);
            }
            else//Y가 짝수일때
            {
                //Vector2 n1 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y - (Mathf.Sqrt(3)/2));
                //Vector2 n2 = new Vector2(minOpened.Point.x, minOpened.Point.y - (Mathf.Sqrt(3)/2));
                //Vector2 n3 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y);
                //Vector2 n4 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y);
                //Vector2 n5 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y + (Mathf.Sqrt(3)/2));
                //Vector2 n6 = new Vector2(minOpened.Point.x, minOpened.Point.y + (Mathf.Sqrt(3)/2));
                Vector2 n1 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y - 1);
                Vector2 n2 = new Vector2(minOpened.Point.x, minOpened.Point.y - 1);
                Vector2 n3 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y);
                Vector2 n4 = new Vector2(minOpened.Point.x + 1, minOpened.Point.y);
                Vector2 n5 = new Vector2(minOpened.Point.x - 1, minOpened.Point.y + 1);
                Vector2 n6 = new Vector2(minOpened.Point.x, minOpened.Point.y + 1);
                moveList.Add(n1);
                moveList.Add(n2);
                moveList.Add(n3);
                moveList.Add(n4);
                moveList.Add(n5);
                moveList.Add(n6);
            }

            for (int i = 0; i < moveList.Count; i++)
            {
                var movePosition = moveList[i];
                if (mapRect.Contains(movePosition))
                {
                    Cell element = mapDic[movePosition];
                    if (closed.Contains(element) == false && element.IsWall == false)
                    {
                        var isOpened = opened.Contains(element);
                        var addDistance = 10;
                        var distance = minOpened.Distance + addDistance;

                        if (isOpened)
                        {
                            if (element.Distance > minOpened.Distance + addDistance)
                            {
                                element.SetDistance(distance);
                                element.SetParent(minOpened);
                            }
                        }
                        else
                        {
                            opened.Add(element);
                            element.SetDistance(distance);
                            element.SetParent(minOpened);
                        }

                        var HeurX = element.Point.x > cellEnd.Point.x ? element.Point.x - cellEnd.Point.x : cellEnd.Point.x - element.Point.x;
                        var HeurY = element.Point.y > cellEnd.Point.y ? element.Point.y - cellEnd.Point.y : cellEnd.Point.y - element.Point.y;
                        element.SetHeuristic((int)Math.Sqrt(HeurX * HeurX + HeurY * HeurY) * addDistance);

                        if (element == cellEnd)
                        {
                            isFindEnd = true;
                        }
                        else
                        {
                            if (!opened.Contains(element))
                                opened.Add(element);
                        }
                    }
                }
            }
            moveList.Clear();
            if (isFindEnd)
            {
                break;
            }
        }

        if (isFindEnd)
        {
            var current = cellEnd;
            findPath.Add(current);
            while (current != cellStart)
            {
                current = current.Parent;
                findPath.Add(current);
            }
        }

        //Reset
        foreach (var cellPair in mapDic)
        {
            var cell = cellPair.Value;
            cell.SetParent(null);
            cell.SetDistance(0);
            cell.SetWeight(0);
            cell.SetHeuristic(0);
        }

        return findPath;
    }
}

