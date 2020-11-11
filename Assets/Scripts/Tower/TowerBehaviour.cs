using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviour : MonoBehaviour, ITower {
    protected TowerBehaviour src;
    public Vector2Int mapIndex { get; private set; }
    public int cost;
    int ITower.cost => cost;

    public abstract void GameplayUpdate();
    public abstract void WaitUpdate();
    public virtual void Init(Vector2Int index, TowerBehaviour src) { mapIndex = index; this.src = src; }

    protected void FaceTarget(CreepBehaviour cb) {
        var dir = cb.transform.position - transform.position;
        // set forward axis to point to creep
        transform.up = dir;
    }

    void ITower.DestroyTower() {
        Destroy(gameObject);
    }
}
