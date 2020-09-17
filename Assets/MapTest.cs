using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour
{
    public int seed;
    private void OnDrawGizmos() {
        var (map, path) = GetMap();

        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                Gizmos.color = map[x, y] ? Color.white : Color.black;

                Gizmos.DrawCube(new Vector3(x, y), Vector3.one);
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(path[0].x, path[0].y), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(path[path.Count - 1].x, path[path.Count - 1].y), Vector3.one);
    }


    (bool[,], List<Vector2Int>) GetMap() {
        Random.InitState(seed);
        var result = new bool[10,10];

        Vector2Int start = new Vector2Int(Random.Range(0, 10), Random.Range(0, 10));
        List<Vector2Int> path =  new List<Vector2Int>();
        path.Add(start);
        result[start.x, start.y] = true;

        for (int i = 0; i < 10; i++) {
            var r = Random.value;

            var dir =
                r < .25f ? Vector2Int.up :
                r < .5f ? Vector2Int.right :
                r < .75f ? Vector2Int.down :
                Vector2Int.left;

            var next = start + dir;

            if (next.x >= 0 && next.x < 10 && next.y >= 0 && next.y < 10 && !result[next.x, next.y]) {
                result[next.x, next.y] = true;
                start = next;
                path .Add( next);
            }
        }
        return (result, path);
    }
}
