using UnityEngine;
using System.Collections.Generic;

public abstract class MapBehaviour : MonoBehaviour {
    static MapBehaviour _main;

    public static bool isAvailable => _main != null;
    public static TileType GetTileType(int x, int y) => _main._tileTypes[x, y];
    public static int width => _main._width;
    public static int height => _main._height;

    public static IReadOnlyList<Vector2Int> GetCreepPath() => _main.GetPathForCreep();



    TileType[,] _tileTypes;
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
        if (!Application.isPlaying) {
            _main = null;
            Start();
        }
    }

    protected abstract TileType[,] BuildMap();
    protected abstract IReadOnlyList<Vector2Int> GetPathForCreep();

}
