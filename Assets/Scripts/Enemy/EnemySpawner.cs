using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     private float timer = 0;
     public GameObject enemy;
     Vector2 whereToSpawn;
     int spawnMax = 5;
     int enemyCounter = 0;
     List<CreepBehaviour> creeps = new List<CreepBehaviour>();
     // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
          timer += Time.deltaTime; 
          if (timer > 5f)
          {
               if (enemyCounter < spawnMax)
               {
                    timer = 0;
                    whereToSpawn = new Vector2(transform.position.x, transform.position.y);
                    var go = Instantiate(enemy, whereToSpawn, Quaternion.identity);
                    enemyCounter++;
                    var cb = go.GetComponent<CreepBehaviour>();
                    creeps.Add(cb);
                    cb.Init();
               }

          }

                 List<CreepBehaviour> toRemove = new List<CreepBehaviour>();

          foreach (var creep in creeps)
          {
               if (creep == null)
               {
                    toRemove.Add(creep);
               }
               else
               {
                    creep.GameplayUpdate();
               }
          }

          foreach (var c in toRemove)
          {
               creeps.Remove(c);
          }


          }
}
