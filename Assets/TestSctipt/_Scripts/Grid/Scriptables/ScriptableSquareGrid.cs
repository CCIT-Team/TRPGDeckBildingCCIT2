using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts.Grid.Scriptables {
    [CreateAssetMenu(fileName = "New Scriptable Square Grid")]
    public class ScriptableSquareGrid : ScriptableGrid
    {
        [SerializeField,Range(3,50)] private int _gridWidth = 16;
        [SerializeField,Range(3,50)] private int _gridHeight = 9;
        
        public override Dictionary<Vector3, NodeBase> GenerateGrid() {
            var tiles = new Dictionary<Vector3, NodeBase>();
            var grid = new GameObject {
                name = "Grid"
            };
            for (int x = 0; x < _gridWidth; x++) {
                for (int y = 0; y < _gridHeight; y++) {
                    var tile = Instantiate(nodeBasePrefab,grid.transform);
                    tiles.Add(new Vector2(x,y),tile);
                }
            }

            return tiles;
        }
    }
}
