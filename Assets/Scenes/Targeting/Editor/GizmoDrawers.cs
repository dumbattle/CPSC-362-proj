/* Reference: 
 *   https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/ 
 *   
 *   I used the answers on this site as a base for this script
 */


using UnityEngine;
using UnityEditor;

public static class GizmoDrawers {
    static Mesh arrowMesh;

    static GizmoDrawers() {
        arrowMesh = new Mesh();

        arrowMesh.vertices = new[] {
            new Vector3(0,.5f,0),
            new Vector3(.25f,0,0),
            new Vector3(-.25f,0,0)
        };

        arrowMesh.triangles = new[] { 0, 1, 2 };
        arrowMesh.RecalculateNormals();
    }


    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    public static void DrawGizmo(CreepPath cp, GizmoType type) {
        if ((type & GizmoType.Selected) == 0) {
            return;
        } 
        if (cp.grid == null) {
            return;
        }
        Gizmos.color = cp.color * 2;
        Vector2 dir = new Vector2();

        for (int i = 0; i < cp.path.Count - 1; i++) {
            Vector3 p = cp.grid.CellToWorld((Vector3Int)cp.path[i]);
            dir = cp.grid.CellToWorld((Vector3Int)(cp.path[i + 1])) - p;

            float time = 0;
            float time2 = .5f;

            var p1 = Vector2.Lerp(p, cp.grid.CellToWorld((Vector3Int)cp.path[i + 1]), time);
            var p2 = Vector2.Lerp(p, cp.grid.CellToWorld((Vector3Int)cp.path[i + 1]), time2);


            if (dir == new Vector2(0, -1)) {
                Gizmos.DrawMesh(arrowMesh, 0, p1, Quaternion.Euler(0,0,180), Vector3.one);
                Gizmos.DrawMesh(arrowMesh, 0, p2, Quaternion.Euler(0,0,180), Vector3.one);
            }
            else {
                Gizmos.DrawMesh(arrowMesh, 0, p1, Quaternion.FromToRotation(Vector3.up, dir), Vector3.one);
                Gizmos.DrawMesh(arrowMesh, 0, p2, Quaternion.FromToRotation(Vector3.up, dir), Vector3.one);
            }
        }
    }
}
