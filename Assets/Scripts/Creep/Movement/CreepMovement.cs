using System;
using UnityEngine;

public abstract class CreepMovement : MonoBehaviour {
    public event Action OnReachedEnd;

    public virtual void Init() { }
    public abstract void GameplayUpdate();

    protected void CallOnReachedEnd() {
        OnReachedEnd?.Invoke();
    }

    public abstract bool ModifySpeed(float amnt);

}
