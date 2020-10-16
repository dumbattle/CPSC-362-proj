using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBehaviour : MonoBehaviour {
    public CreepHealth health;
    TestCreepMovement movement;
    CreepCommponent[] components;

    public void Init() {
      
        health = GetComponent<CreepHealth>();
        movement = GetComponent<TestCreepMovement>();
       
        components = GetComponents<CreepCommponent>();
        movement.Init();
        health.Init(100);
        // TODO - check health and movement are not null. If they are, throw an exception.
    }

     private void Die()
     {
          Destroy(gameObject);
     }
    //this script does nothing on its own, the components will are what dictates what this creep will do
    public void GameplayUpdate() {
        // health has no GameplayUpdate
        movement.GameplayUpdate();
          
        foreach (var c in components) {
            c.GameplayUpdate();
        }
          if (health.current <= 0)
          {
               Die();
          }
     }
}
