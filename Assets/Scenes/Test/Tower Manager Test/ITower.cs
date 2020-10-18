using UnityEngine;

public interface ITower
{
    Vector2Int mapIndex { get; }
    void GameplayUpdate();
}
