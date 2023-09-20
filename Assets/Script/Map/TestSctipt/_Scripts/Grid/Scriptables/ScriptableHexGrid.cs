using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts.Grid.Scriptables {
    [CreateAssetMenu(fileName = "New Scriptable Hex Grid")]
    public class ScriptableHexGrid : ScriptableGrid {

        [SerializeField,Range(1,50)] private int _gridWidth = 16;
        [SerializeField,Range(1,50)] private int _gridDepth = 9;
        
        public override Dictionary<Vector3, NodeBase> GenerateGrid() 
        {
            var tiles = new Dictionary<Vector3, NodeBase>();
            GameObject grid = new GameObject 
            {
                name = "Grid"
            };
            for (var r = 0; r < _gridDepth ; r++) {
                var rOffset = r >> 1;
                for (var q = -rOffset; q < _gridWidth - rOffset; q++) {
                    var tile = Instantiate(nodeBasePrefab,grid.transform);
                    tiles.Add(tile.Coords.Pos,tile);
                }
            }

            return tiles;
        }
    }
}