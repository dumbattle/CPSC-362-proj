using UnityEngine;

public class TestCreepMovement : CreepMovement {

    private Transform target;
    private int wavepointIndex = 0;
    public float speed = 2f;

    public override void Init() {
        target = Waypoints.points[0];
        transform.position = target.position;
    }

    public override void GameplayUpdate() {
        Vector3 dir = target.position - transform.position;
        dir = dir.normalized * speed * Time.deltaTime;

        if (dir.sqrMagnitude <= 0.01f * 0.01f) {
            transform.position = target.position;
            GetNextWaypoint();
            return;
        }

        transform.Translate(dir, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.01f) {
            GetNextWaypoint();
        }

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