using UnityEngine;

namespace AustinsExamples.Tower {
    public interface ITower {
        Vector2Int mapIndex { get; }
        void GameplayUpdate();
    }
}
