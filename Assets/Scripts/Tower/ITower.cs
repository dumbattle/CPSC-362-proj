using UnityEngine;

public interface ITower
{
    Vector2Int mapIndex { get; }
    int cost { get; }
    void Init(Vector2Int name);

    void GameplayUpdate();
}

// To add functionality, it must be done in both ITower and TowerBehaviour