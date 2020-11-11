/* Reference: 
 *   https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/ 
 *   
 *   I used the answers on this site as a base for this script
 */


using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreepPath)), CanEditMultipleObjects]
public class CreepPathDrawer : Editor {
    enum End {
        front, back
    }
    int fid;
    int bid;
    int id;
    End end = End.back;

    private void OnSceneGUI() {
        InitTarget();
        BackHandle();
        FrontHandle();

        if (GUIUtility.hotControl == fid) {
            end = End.front;
        }
        else if (GUIUtility.hotControl == bid) {
            end = End.back;
        }

        KeypadInput();
    }

    private void InitTarget() {
        CreepPath cp = (CreepPath)target;
        if (cp.grid == null) {
            cp.grid = FindObjectOfType<Grid>();
        }

        if (cp.path.Count == 0) {
            cp.path.Add(new Vector2Int(0, 0));
            cp.path.Add(new Vector2Int(0, 1));
        }
    }

    private void KeypadInput() {
        CreepPath cp = (CreepPath)target;
        var endT = cp.path[cp.path.Count - 1].SetZ(0);
 

        if (Event.current.type == EventType.KeyDown) {
            if (Event.current.keyCode == KeyCode.W || Event.current.keyCode == KeyCode.UpArrow) {
                Move(new Vector2Int(0, 1));
                Event.current.Use();
            }
            else if (Event.current.keyCode == KeyCode.A || Event.current.keyCode == KeyCode.LeftArrow) {
                Move(new Vector2Int(-1, 0));
                Event.current.Use();

            }
            else if (Event.current.keyCode == KeyCode.S || Event.current.keyCode == KeyCode.DownArrow) {
                Move(new Vector2Int(0, -1));
                Event.current.Use();
            }
            else if (Event.current.keyCode == KeyCode.D || Event.current.keyCode == KeyCode.RightArrow) {
                Move(new Vector2Int(1, 0));
                Event.current.Use();
            }
        }



        void Move(Vector2Int dir) {
            switch (end) {
                case End.front:
                    FrontEnd(dir);
                    break;
                case End.back:
                    BackEnd(dir);
                    break;
                default:
                    break;
            }
        }
        void BackEnd(Vector2Int dir) {
            Undo.RecordObject(cp, "Update Creep Path");
            var t = cp.path[cp.path.Count - 1] + dir;
            int i = cp.path.IndexOf(t);
            if (i < 0) {
                cp.path.Add(t);
            }
            else {
                cp.path.RemoveRange(i + 1, cp.path.Count - i - 1);
            }
        }
        void FrontEnd(Vector2Int dir) {
            Undo.RecordObject(cp, "Update Creep Path Front");
            var t = cp.path[0] + dir;
            int i = cp.path.IndexOf(t);

            if (i < 0) {
                cp.path.Insert(0, t);
            }
            else {
                cp.path.RemoveRange(0, i);
            }
        }
    }

  

    void FrontHandle() {
        CreepPath cp = (CreepPath)target;
        var frontT = cp.path[0].SetZ(0);
        Handles.color = cp.color;
        EditorGUI.BeginChangeCheck();

        Handles.CapFunction hcf = (controlID, position, rotation, size, et) => {
            fid = controlID;
            Handles.RectangleHandleCap(controlID, position, rotation, size,et);
        };

        Vector3 world = cp.grid.CellToWorld(frontT);
        Vector3 newTargetPosition =
            Handles.FreeMoveHandle(
               world,
               Quaternion.identity,
               cp.grid.cellSize.x / 2,
               Vector3.zero,
               hcf);

        Vector2Int newfrontT = (Vector2Int)cp.grid.WorldToCell(newTargetPosition);

        if (EditorGUI.EndChangeCheck()) {
            end = End.front;
            Undo.RecordObject(cp, "Update Creep Path");
            if (frontT != newfrontT.SetZ(0)) {


                if ((frontT - newfrontT.SetZ(0)).sqrMagnitude == 1) {
                int i = cp.path.IndexOf(newfrontT);
                    if (i >= 0) {
                        cp.path.RemoveRange(0, i);
                    }
                    else {
                        cp.path.Insert(0,newfrontT);
                    }
                }
            }
        }
    }
    void BackHandle() {
        CreepPath cp = (CreepPath)target;
        var endT = cp.path[cp.path.Count - 1].SetZ(0);
        EditorGUI.BeginChangeCheck();

        Handles.color = cp.color;

        Handles.CapFunction hcf = (controlID, position, rotation, size, et) => {
            bid = controlID;
            Handles.RectangleHandleCap(controlID, position, rotation, size, et);
        };
        Vector3 newTargetPosition =
            Handles.FreeMoveHandle(
               cp.grid.CellToWorld(endT),
               Quaternion.identity,
               cp.grid.cellSize.x / 2,
               Vector3.zero,
               hcf);

        Vector2Int newEndT = (Vector2Int)cp.grid.WorldToCell(newTargetPosition);


        if (EditorGUI.EndChangeCheck()) {
            end = End.back;
            Undo.RecordObject(cp, "Update Creep Path");
            if (endT != newEndT.SetZ(0)) {
                int i = cp.path.IndexOf(newEndT);


                if ((endT - newEndT.SetZ(0)).sqrMagnitude == 1) {
                    if (i >= 0) {
                        cp.path.RemoveRange(i + 1, cp.path.Count - i - 1);
                    }
                    else {
                        cp.path.Add(newEndT);
                    }
                }
            }
        }
    }
}