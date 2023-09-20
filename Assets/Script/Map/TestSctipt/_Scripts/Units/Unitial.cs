using UnityEngine;

namespace Tarodev_Pathfinding._Scripts.Units {
    public class Unitial : MonoBehaviour {
        [SerializeField] private SpriteRenderer _renderer;

        public void Init(Sprite sprite) {
            _renderer.sprite = sprite;
        }
    }
}
