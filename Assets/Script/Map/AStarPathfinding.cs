using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathfinding
{
    public List<Tile> finalPathTiles = new List<Tile>();
    public List<Tile> FindPath(Tile startPoint, Tile endPoint)
    {
        List<Tile> openPathTiles = new List<Tile>();
        List<Tile> closedPathTiles = new List<Tile>();

        // Prepare the start tile.
        Tile currentTile = startPoint;

        currentTile.g = 0;
        currentTile.h = GetEstimatedPathCost(startPoint.position, endPoint.position);

        // Add the start tile to the open list.
        openPathTiles.Add(currentTile);

        while (openPathTiles.Count != 0)
        {
            // Sorting the open list to get the tile with the lowest F.
            openPathTiles = openPathTiles.OrderBy(x => x.F).ThenByDescending(x => x.g).ToList();
            currentTile = openPathTiles[0];

            // Removing the current tile from the open list and adding it to the closed list.
            openPathTiles.Remove(currentTile);
            closedPathTiles.Add(currentTile);

            int g = currentTile.g + 1;

            // If there is a target tile in the closed list, we have found a path.
            if (closedPathTiles.Contains(endPoint))
            {
                break;
            }

            // Investigating each adjacent tile of the current tile.
            foreach (Tile adjacentTile in currentTile.adjacentTiles)
            {
                // Ignore not walkable adjacent tiles.
                if (adjacentTile.isObstacle)
                {
                    continue;
                }

                // Ignore the tile if it's already in the closed list.
                if (closedPathTiles.Contains(adjacentTile))
                {
                    continue;
                }

                // If it's not in the open list - add it and compute G and H.
                if (!(openPathTiles.Contains(adjacentTile)))
                {
                    adjacentTile.g = g;
                    adjacentTile.h = GetEstimatedPathCost(adjacentTile.position, endPoint.position);
                    openPathTiles.Add(adjacentTile);
                }
                // Otherwise check if using current G we can get a lower value of F, if so update it's value.
                else if (adjacentTile.F > g + adjacentTile.h)
                {
                    adjacentTile.g = g;
                }
            }
        }

        //List<Tile> finalPathTiles = new List<Tile>();

        // Backtracking - setting the final path.
        if (closedPathTiles.Contains(endPoint))
        {
            currentTile = endPoint;
            finalPathTiles.Add(currentTile);

            for (int i = endPoint.g - 1; i >= 0; i--)
            {
                currentTile = closedPathTiles.Find(x => x.g == i && currentTile.adjacentTiles.Contains(x));
                finalPathTiles.Add(currentTile);
            }

            finalPathTiles.Reverse();
            Debug.Log("Done!");
        }
        else
        {
            Debug.Log("I Can`t Find Path!");
        }
       // Debug.Log("Done!");
        return finalPathTiles;
    }

    /// <summary>
    /// Returns estimated path cost from given start position to target position of hex tile using Manhattan distance.
    /// </summary>
    /// <param name="startPosition">Start position.</param>
    /// <param name="targetPosition">Destination position.</param>
    protected static int GetEstimatedPathCost(Vector3Int startPosition, Vector3Int targetPosition)
    {
        return Mathf.Max(Mathf.Abs(startPosition.z - targetPosition.z), Mathf.Max(Mathf.Abs(startPosition.x - targetPosition.x), Mathf.Abs(startPosition.y - targetPosition.y)));
    }
}
