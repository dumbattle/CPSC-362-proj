using UnityEngine;
using System.Collections.Generic;

public abstract class WaypointBehaviour : MonoBehaviour {
    static WaypointBehaviour _main;

    public static IReadOnlyList<Vector2> GetWayPoints() {
        return _main.GetWayPoints_();
    }

    protected virtual void Awake() {
        _main = this;
    }

    protected abstract IReadOnlyList<Vector2> GetWayPoints_();



}
