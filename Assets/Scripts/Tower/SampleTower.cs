using System;
using System.Collections;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;


public class SampleTower : TowerBehaviour
{
     [Min(.001f)]
     public float coolDown;
     private float cooldownTimer;
     [Range(.1f, 3f)]
     
     public float LaserLife=1;
     public LineRenderer line;
     public float range = 2f;                // the shooting range of the towers

     [Min(1)] public int damageDone = 1;
     // curently unused variable; included as a precaution in case there is a need for 
     // rotations in the future

     // Start is called before the first frame update
     
        

     public override void GameplayUpdate()
     {
          FadeLaser();
          cooldownTimer -= Time.deltaTime;
          if (cooldownTimer > 0)
          {
               return;
          }
          // Finds GameObjects that have been tagged as enemies and stores them
          // var creeps = creepManager.AllCreeps();
          var enemies = CreepManager.main.AllCreeps();

          float shortestDistance = Mathf.Infinity;
          CreepBehaviour nearestEnemy = null;                     // the closest enemy is null by default

          
          // searches through all enemies to find the nearest one
          foreach (var enemy in enemies)
          {
               float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

               if (distanceToEnemy < shortestDistance)
               {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
               }
          }

          // If the closest enemy is in range, that enemy becomes the target
          if (nearestEnemy != null && shortestDistance <= range)
          {
               cooldownTimer = coolDown;
               // the line renderer is enabled when a target is detected within range
               line = GetComponent<LineRenderer>();
               line.enabled = true;
               line.SetPosition(0, transform.position);
               line.SetPosition(1, nearestEnemy.transform.position);
               var creepyBar = nearestEnemy.GetComponent<CreepHealth>();
               creepyBar.damage(damageDone);
               // prints "Targeting Enemy" to the console
               StartLaser();
          }
        
     }

     public override void  WaitUpdate()
     {
          FadeLaser();
     }

     private void FadeLaser()
     {
          var c = line.startColor;
          var f = 1 / LaserLife;
          c.a = c.a - f * Time.deltaTime;
          line.startColor = c;
          line.endColor = c;
     }


     private void StartLaser()
     {
          var c = line.startColor;
          c.a = 1;
          line.startColor = c;
          line.endColor = c;
     }
}