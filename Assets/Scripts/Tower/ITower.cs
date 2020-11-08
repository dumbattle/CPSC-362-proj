using UnityEngine;

public interface ITower {
    Vector2Int mapIndex { get; }
    int cost { get; }
    void Init(Vector2Int index, TowerBehaviour src);

    void GameplayUpdate();

    void WaitUpdate();
}

