using System;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehaviour : MonoBehaviour {
    public CreepHealth health;
    public CreepMovement movement;

    public event Action OnKilled;
    public event Action OnReachedEnd {
        add { movement.OnReachedEnd += value; }
        remove { movement.OnReachedEnd -= value; }
    }
    CreepCommponent[] components;

    public void Init() {
        health = GetComponent<CreepHealth>();
        movement = GetComponent<CreepMovement>();

        components = GetComponents<CreepCommponent>();
        movement.Init();
        health.Init();

        OnReachedEnd += () => {
            Destroy(gameObject);
        };
        foreach (var c in components) {
            c.Init(this);
        }
    }

    private void Die() {
        OnKilled?.Invoke();
        Destroy(gameObject);
    }

    //this script does nothing on its own, the components will are what dictates what this creep will do
    public void GameplayUpdate() {
        // health has no GameplayUpdate
        movement.GameplayUpdate();

        foreach (var c in components) {
            c.GameplayUpdate();
        }
        if (health.current <= 0) {
            Die();
        }
    }
}
