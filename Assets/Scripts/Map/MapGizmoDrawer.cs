using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGizmoDrawer : MonoBehaviour {
    private void OnDrawGizmos() {
        if (!MapBehaviour.isAvailable) {
            return;
        }

        for (int x = 0; x < MapBehaviour.width; x++) {
            for (int y = 0; y < MapBehaviour.height; y++) {
                switch (MapBehaviour.GetTileType(x,y)) {
                    case TileType.creepPath:
                        Gizmos.color = Color.gray;
                        break;
                    case TileType.towerSpot:
                        Gizmos.color = Color.black;
                        break;
                    default:
                        break;
                }

                Gizmos.DrawCube(new Vector3(x, y), Vector3.one);
            }
        }
        var path = MapBehaviour.GetCreepPath();
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(path[0].x, path[0].y), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(path[path.Count - 1].x, path[path.Count - 1].y), Vector3.one);
    }


}
