using System.Collections.Generic;
using UnityEngine;


public interface IMap
{
    Vector2Int GetMapSize();
    void SetCell(Cell cell);
    ICell GetCell(Vector2Int point);
    IDictionary<Vector2Int, ICell> GetCells();
}
