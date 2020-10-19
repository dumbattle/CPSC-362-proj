using UnityEngine;

public interface ITower
{
    Vector2Int mapIndex { get; }
    GameObject reference { get; }

    void GameplayUpdate();
}
