﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviour : MonoBehaviour, ITower
{
    public Vector2Int mapIndex { get; private set; }
    public int cost;
    int ITower.cost => cost;

    public abstract void GameplayUpdate();
    public abstract void WaitUpdate();
    public virtual void Init(Vector2Int index) { mapIndex = index; }
}

// To add functionality, it must be done in both ITower and TowerBehaviour