/*
 * Example of a randomly generated map
 * This algorithm does generate nice looking maps, so it shouldn't be used in final product
 */


using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
[AddComponentMenu("Map/Example/Map Example Random")]
public class ExampleMapRandom : MapBehaviour {
    public int seed;
    [Range(1, 100)]
    public int _mapWidth = 5;
    [Range(1, 100)]
    public int _mapHeight = 5;
    [Range(2, 1000)]
    public int length;

    // path not fixed
    List<Vector2Int> _path;


    protected override TileType[,] BuildMap() {
        // save state
        var rState = Random.state;
        //set seed
        Random.InitState(seed);

        // initialize
        var result = new TileType[_mapWidth, _mapHeight];

        for (int x = 0; x < _mapWidth; x++) {
            for (int y = 0; y < _mapHeight; y++) {
                result[x, y] = TileType.towerSpot;
            }
        }

        //construct path
        Vector2Int start = new Vector2Int(Random.Range(0, _mapWidth), Random.Range(0, _mapHeight));
        _path = new List<Vector2Int>();
        _path.Add(start);
        result[start.x, start.y] = TileType.creepPath;

        for (int i = 0; i < length; i++) {
            // 0 <= r <= 1
            var r = Random.value;

            // random direction
            var dir =
                r < .25f ? Vector2Int.up :
                r < .5f ? Vector2Int.right :
                r < .75f ? Vector2Int.down :
                Vector2Int.left;

            var next = start + dir;

            // check next tile is in bounds
            if (next.x >= 0 && next.x < _mapWidth && next.y >= 0 && next.y < _mapHeight) {
                // valid tile for path
                result[next.x, next.y] = TileType.creepPath;
                start = next;
                _path.Add(next);
            }
        }

        //reset state to preserve randomness elsewhere
        Random.state = rState;
        return result;
    }

    protected override IReadOnlyList<Vector2Int> GetPathForCreep() {
        return _path.AsReadOnly();
    }
}
