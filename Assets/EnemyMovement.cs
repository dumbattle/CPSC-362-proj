using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;

    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        wavepointIndex++;

        if(wavepointIndex >= Waypoints.points.Length)
        {
            Destroy(gameObject);
            return;
        }

        

        target = Waypoints.points[wavepointIndex];
    }
}
