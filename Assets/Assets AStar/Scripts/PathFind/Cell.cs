using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell Parent { get; private set; }
    public Vector2 Point { get; private set; }
    public bool IsWall { get; private set; }
    public int Weight { get; private set; }
    public int Heuristic { get; private set; }
    public int Distance { get; private set; }
    public int Summ => (Distance + Heuristic) * Weight;

    public void Cells(Vector2 point)
    {
        Point = point;
        Weight = 1;
    }

    public void SetParent(Cell parent)
    {
        Parent = parent;
    }

    public void SetIsWall(bool isWall)
    {
        IsWall = isWall;
    }

    public void SetWeight(int weight)
    {
        Weight = 1 + weight;
    }

    public void SetHeuristic(int heuristic)
    {
        Heuristic = heuristic;
    }

    public void SetDistance(int distance)
    {
        Distance = distance;
    }
}

