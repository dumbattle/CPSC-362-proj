using UnityEngine;

public interface ITower {
    Vector2Int mapIndex { get; }
    int type { get; }
    int cost { get; }
    int level { get; }
    void Init(Vector2Int index, TowerBehaviour src);

    void GameplayUpdate();

    void WaitUpdate();

    void DestroyTower();
}

