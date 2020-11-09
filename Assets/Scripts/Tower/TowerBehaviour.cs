﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviour : MonoBehaviour, ITower
{
    protected TowerBehaviour src;
    public Vector2Int mapIndex { get; private set; }
    public int cost;
    int ITower.cost => cost;

    public abstract void GameplayUpdate();
    public abstract void WaitUpdate();
    public virtual void Init(Vector2Int index, TowerBehaviour src) { mapIndex = index; this.src = src; }

    void ITower.DestroyTower()
    {
        Destroy(gameObject);
    }
}

// To add functionality, it must be done in both ITower and TowerBehaviour