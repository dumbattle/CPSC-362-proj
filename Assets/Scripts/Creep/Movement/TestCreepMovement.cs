using UnityEngine;
using UnityEngine.Serialization;

public class TestCreepMovement : CreepMovement {
    private Transform target;
    private int wavepointIndex = 0;

    [FormerlySerializedAs("speed")] // renamed this variable
    [SerializeField]
    float _baseSpeed = 2f;

    float speedMod = 1;
    public float baseSpeed => _baseSpeed;

    public float speed => _baseSpeed * speedMod;

    public override void Init() {
        target = Waypoints.points[0];
        transform.position = target.position;
    }

    public override void GameplayUpdate() {
        Vector3 dir = target.position - transform.position;

        if (dir.sqrMagnitude <= 0.01f * 0.01f) {
            transform.position = target.position;
            GetNextWaypoint();
            return;
        }
        dir = dir.normalized * speed * Time.deltaTime;

        transform.Translate(dir, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.01f) {
            GetNextWaypoint();
        }

    }

    public override bool ModifySpeed(float amnt) {
        if (amnt < 0) {
            return false;
        }
        
        speedMod *= amnt;
        return true;
    }

    void GetNextWaypoint() {
        wavepointIndex++;

        if (wavepointIndex >= Waypoints.points.Length) {
            CallOnReachedEnd();
            return;
        }



        target = Waypoints.points[wavepointIndex];
    }

}