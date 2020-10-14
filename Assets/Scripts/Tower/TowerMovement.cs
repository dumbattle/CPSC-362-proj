using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMovement : MonoBehaviour
{
    public LineRenderer line;
    public Transform target;
    public float range = 2f;                // the shooting range of the towers

    public string enemyTag = "Enemy";       // towers will target objects tagged "Enemy"

    // curently unused variable; included as a precaution in case there is a need for 
    // rotations in the future
    public Transform partToRotate;

    // Start is called before the first frame update
    void Start()
    {
        // Continuously checks for targets in range
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget()
    {
          // Finds GameObjects that have been tagged as enemies and stores them
         // var creeps = creepManager.AllCreeps();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;                     // the closest enemy is null by default

        // searches through all enemies to find the nearest one
        foreach (GameObject enemy in enemies)
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
            target = nearestEnemy.transform;

            // the line renderer is enabled when a target is detected within range
            line = GetComponent<LineRenderer>();
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, target.position);

            // prints "Targeting Enemy" to the console
            Debug.Log("Targeting Enemy");
        }
        else
        {
            target = null;      // no target
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // the line renderer is disabled when the there is no target in range
            line.enabled = false;
            return;
        }
        
        /* For tower rotation; Not yet sure if necessary
         
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
         */
    }
}