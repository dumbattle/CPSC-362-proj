using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehaviour : MonoBehaviour {
    CreepHealth health;
    CreepMovement movement;
    CreepCommponent[] components;

    public void Init() {
        health = GetComponent<CreepHealth>();
        movement = GetComponent<CreepMovement>();

        components = GetComponents<CreepCommponent>();
        // TODO - check health and movement are not null. If they are, throw an exception.
    }

    //this script does nothing on its own, the components will are what dictates what this creep will do
    public void GameplayUpdate() {
        // health has no GameplayUpdate
        movement.GameplayUpdate();

        foreach (var c in components) {
            c.GameplayUpdate();
        }
    }
}
