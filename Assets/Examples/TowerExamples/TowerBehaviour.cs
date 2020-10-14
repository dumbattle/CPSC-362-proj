using UnityEngine;

namespace AustinsExamples.Tower {
    public abstract class TowerBehaviour : MonoBehaviour, ITower {
        public Vector2Int mapIndex { get; private set; }
        public abstract void GameplayUpdate();
    }
}
