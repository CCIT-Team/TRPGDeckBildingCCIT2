using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts.Grid.Scriptables {
    [CreateAssetMenu(fileName = "New Scriptable Iso Grid")]
    public class ScriptableIsoGrid : ScriptableGrid
    {
        [SerializeField,Range(3,50)] private int _gridWidth = 16;
        [SerializeField,Range(3,50)] private int _gridHeight = 9;
        
        public override Dictionary<Vector3, NodeBase> GenerateGrid() {
            var tiles = new Dictionary<Vector3, NodeBase>();
            var grid = new GameObject {
                name = "Grid"
            };
            
            for (var x = 0; x < _gridWidth; x++) {
                for (var y = 0; y < _gridHeight; y++) {
                    var tile = Instantiate(nodeBasePrefab,grid.transform);
                    var pos = new Vector2((x - y) * 0.5f, (x + y) * 0.25f) * 2;
                    tiles.Add(pos,tile);
                }
            }

            return tiles;
        }
    }
}

