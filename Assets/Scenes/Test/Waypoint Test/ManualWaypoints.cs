using UnityEngine;
using System.Collections.Generic;


[AddComponentMenu("Waypoints/Manual Waypoints")]
public class ManualWaypoints : WaypointBehaviour {
    Vector2[] points;

    protected override void Awake() {
        base.Awake();
        points = new Vector2[transform.childCount];

        for (int i = 0; i < points.Length; i++) {
            points[i] = transform.GetChild(i).position;
        }
    }

    protected override IReadOnlyList<Vector2> GetWayPoints_() {
        return points;
    }
}
