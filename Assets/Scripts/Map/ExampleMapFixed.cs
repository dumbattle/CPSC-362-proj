/*
 * Example of a fixed map
 * This creates the same map every time
 * If you use this as a template, be sure that the path is valid
 *      Each step is either vertical or horizontal (no  diagonal)
 *      Each step distance is 1
 */


 
 
 using UnityEngine;
using System.Collections.Generic;


[ExecuteInEditMode]
<<<<<<< HEAD
[AddComponentMenu("Map1")]
public class ExampleMapFixed : MapBehaviour {
    // fixed path
    List<Vector2Int> _path = new List<Vector2Int>() {
        new Vector2Int(0, 3),
        new Vector2Int(1, 3),
        new Vector2Int(2, 3),
        new Vector2Int(3, 3),
        new Vector2Int(3, 4),
        new Vector2Int(4, 4),
        new Vector2Int(5, 4),
        new Vector2Int(6, 4),
        new Vector2Int(6, 5),
        new Vector2Int(7, 5),
        new Vector2Int(8, 5),
        new Vector2Int(9, 5),
        new Vector2Int(10, 5),
        new Vector2Int(11, 5),
        new Vector2Int(12, 5),
        new Vector2Int(13, 5),
        new Vector2Int(14, 5),
        new Vector2Int(15, 5),
=======
[AddComponentMenu("Map/Example/Map Example Fixed")]
public class ExampleMapFixed : MapBehaviour {
    // fixed path
    List<Vector2Int> _path = new List<Vector2Int>() {
        new Vector2Int(3, 3),
        new Vector2Int(3, 4),
        new Vector2Int(3, 5),
        new Vector2Int(3, 6),
        new Vector2Int(4, 6),
        new Vector2Int(5, 6),
        new Vector2Int(5, 5),
        new Vector2Int(5, 4),
        new Vector2Int(6, 4),
        new Vector2Int(7, 4),
        new Vector2Int(8, 4)
>>>>>>> 12ae5fc49d469d14db2c4e5a748d86c2c7fb17c0
    };

    protected override TileType[,] BuildMap() {
        // fixed size
        var result = new TileType[16, 16];

        // initialize
        for (int x = 0; x < 16; x++) {
            for (int y = 0; y < 16; y++) {
                result[x, y] = TileType.towerSpot;
            }
        }

        // set path
        foreach (var p in _path) {
            result[p.x, p.y] = TileType.creepPath;
        }

        return result;
    }

    protected override IReadOnlyList<Vector2Int> GetPathForCreep() {
        // read only - don't want path to be modified
        return _path.AsReadOnly();
    }
}