using UnityEngine;

    public class CellView : MonoBehaviour
    {
        private Vector2 _point;

        public void SetPoint(Vector2 point)
        {
            _point = point;
        }

        public Vector2 GetPoint() => _point;
    }
