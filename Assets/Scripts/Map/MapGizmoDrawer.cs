using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGizmoDrawer : MonoBehaviour {
    private void OnDrawGizmos() {
        if (!MapBehaviour.isAvailable) {
            return;
        }

        // draw tiles.
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

<<<<<<< HEAD
                Gizmos.DrawCube(new Vector3(x, y), Vector3.one/2);
=======
                Gizmos.DrawCube(new Vector3(x, y), Vector3.one);
>>>>>>> 12ae5fc49d469d14db2c4e5a748d86c2c7fb17c0
            }
        }

        // color start and end tiles
        var path = MapBehaviour.GetCreepPath();
        Gizmos.color = Color.green;
<<<<<<< HEAD
        Gizmos.DrawCube(new Vector3(path[0].x, path[0].y), Vector3.one/2);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(path[path.Count - 1].x, path[path.Count - 1].y), Vector3.one/2);
=======
        Gizmos.DrawCube(new Vector3(path[0].x, path[0].y), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(path[path.Count - 1].x, path[path.Count - 1].y), Vector3.one);
>>>>>>> 12ae5fc49d469d14db2c4e5a748d86c2c7fb17c0
    }


}
