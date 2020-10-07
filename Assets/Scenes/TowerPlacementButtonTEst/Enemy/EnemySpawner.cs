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
     // Start is called before the first frame update
    void Start()
    {
        
    }

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
                    Instantiate(enemy, whereToSpawn, Quaternion.identity);
                    enemyCounter++;
               }
          }

    }
}
