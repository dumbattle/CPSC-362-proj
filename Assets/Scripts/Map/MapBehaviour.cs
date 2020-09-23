using UnityEngine;
using System.Collections.Generic;

public abstract class MapBehaviour : MonoBehaviour {
    static MapBehaviour _main;

    public static bool isAvailable => _main != null;
    public static TileType GetTileType(int x, int y) => _main._tileTypes[x, y];  // I do not like this b/c it returns an enum
    public static int width => _main._width;
    public static int height => _main._height;

    public static IReadOnlyList<Vector2Int> GetCreepPath() => _main.GetPathForCreep();


    // TODO - These 2 functions are opposites of each other
    public static Vector3 GetWorldPosition(int x, int y) {
        throw new System.NotImplementedException();
    }

    public static Vector2Int GetTileIndex(Vector2 worldPosition) {
        throw new System.NotImplementedException();
    }


    TileType[,] _tileTypes; // I do not like this. This is highly specific and likely to change, therefore should not be in base class
    int _width;
    int _height;


    protected virtual void Start() {
        if (_main != null) {
            Debug.LogWarning("Multiple map components found. Only 1 should exist");
        }
        _main = this;
        _tileTypes = BuildMap();
        _width = _tileTypes.GetLength(0);
        _height = _tileTypes.GetLength(1);
    }

    private void Update() {
        // update map when in editor, but not during gameplay
        // allows for changing values and seeing results immediately
        // make sure derived class has ExecuteInEditModeAttribute
        if (!Application.isPlaying) {
            _main = null;
            Start();
        }
    }

    private void OnDestroy() {
        if (_main == this) {
            // allows another map to be loaded later
            _main = null;
        }
    }


    protected abstract TileType[,] BuildMap();
    protected abstract IReadOnlyList<Vector2Int> GetPathForCreep();
}
