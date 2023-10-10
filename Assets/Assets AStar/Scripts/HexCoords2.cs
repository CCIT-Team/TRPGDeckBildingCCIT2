using UnityEngine;


    public static class HexCoords2
    {
        public const float CellHeight = 0.88f;

        public static Vector3 GetHexVisualCoords(Vector2 point, Vector2Int mapSize)
        {

            var shift = point.y % 2 == 0 ? 0 : 0.5f;
            var x = point.x + shift - ((float)mapSize.x / 2) + 0.25f;
            var y = point.y * CellHeight - (mapSize.y * CellHeight / 2f);
            return new Vector3(x, 0, y);
        }
    }
