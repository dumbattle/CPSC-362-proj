using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    // TODO - Use this instead
    public static Vector2[] GetWayPoints() {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        points = new Transform[transform.childCount];

        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
