using UnityEngine;
using System.Collections.Generic;


public class EnemyMovementUsingWaypointBehaviour : MonoBehaviour {
    public float speed = 2f;

    private IReadOnlyList<Vector2> _path;
    private int wavepointIndex;

    void Start() {
        _path = WaypointBehaviour.GetWayPoints();
        wavepointIndex = 0;
    }

    void Update() {
        Vector3 dir = (Vector3)_path[wavepointIndex] - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _path[wavepointIndex]) <= 0.01f) {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {
        wavepointIndex++;

        if (wavepointIndex >= Waypoints.points.Length) {
            Destroy(gameObject);
            return;
        }
    }
}
