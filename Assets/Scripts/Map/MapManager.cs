using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    // current map
    [SerializeField]
    private Tilemap tilemap = null;
    // different types of tiles on the map
    [SerializeField]
    private Tile fieldTile = null;
    [SerializeField]
    private Tile pathTile = null; 
    [SerializeField]
    private Tile highlightTile = null;

    // returns position on Tilemap
    private Vector3Int GetMousePosition(Vector3 worldPoint) {
        return tilemap.WorldToCell(worldPoint);
    }

    // returns tile at position
    //   - requires input from UI manager
    public Tile GetTile(Vector3 worldPoint) {
        Vector3Int mousePosition = GetMousePosition(worldPoint);
        return tilemap.GetTile<Tile>(mousePosition);
    }

    // returns relative tile position at position
    //   - requires input from UI manager
    public (int, int) GetTilePosition(Vector3 worldPoint) {
        Vector3Int mousePosition = GetMousePosition(worldPoint);
        int relativeX = mousePosition[0] - tilemap.cellBounds.xMin;
        int relativeY = mousePosition[1] - tilemap.cellBounds.yMin;
    
        return (relativeX, relativeY);
    }

    public Vector2Int MapSize() {
        return new Vector2Int(tilemap.size.x, tilemap.size.y);
    }


    public bool ValidTowerTile(int x, int y)
    {
        var t = tilemap.GetTile<Tile>(new Vector3Int(x + tilemap.cellBounds.xMin, y + tilemap.cellBounds.yMin, 0));
    
        return t != pathTile;
    }

    // returns tile asset type of fieldTile
    public Tile GetFieldTile() {
        return fieldTile;
    }

    // returns tile asset type of fieldTile
    public Tile GetPathTile() {
        return pathTile;
    }

    // returns tile asset type of highlightTile
    public Tile GetHighlightTile() {
        return highlightTile;
    }
}
