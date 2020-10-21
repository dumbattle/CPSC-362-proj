using UnityEngine;

public abstract class CreepCommponent : MonoBehaviour {
    protected CreepBehaviour creep; // creep this component belongs to

    protected virtual void Awake() {
        creep = GetComponent<CreepBehaviour>();
    }

    public virtual void GameplayUpdate() { } // empty by default
}
