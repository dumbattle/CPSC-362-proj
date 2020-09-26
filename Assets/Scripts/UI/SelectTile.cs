using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
 
public class SelectTile : MonoBehaviour {
    private Tilemap tilemap;
    public Tile normalTile;
    public Tile highlightTile;
    public Tile pathTile;
 
    //private Ray ray;
    private Vector3Int newMousePos;
    private Vector3Int oldMousePos;

    bool[] validTiles;

    public GameObject tower;
    private TowerMovement towerScript;
 
 
    private void OnMouseOver() {
 
        if (oldMousePos != newMousePos) {
            if (tilemap.GetTile<Tile>(oldMousePos) == highlightTile) {
                tilemap.SetTile(oldMousePos, normalTile);
            }
            oldMousePos = newMousePos;
        }
 
        if (tilemap.GetTile<Tile>(newMousePos) == normalTile && tilemap.HasTile(newMousePos)) {
            tilemap.SetTile(newMousePos, highlightTile);
        }
    }

    private void OnMouseDown() {
        int tileIndex = (newMousePos[0] + 1) + (16 * (newMousePos[1] + 1));
        
        if (validTiles[tileIndex]) {
            Instantiate(tower, newMousePos, Quaternion.identity);
        }    
    }

    private void Start() {
        tilemap = gameObject.GetComponent<Tilemap>();
        oldMousePos = new Vector3Int(0, 0, 0);

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tileArray = tilemap.GetTilesBlock(bounds);
        validTiles = new bool[tileArray.Length];

        for (int i = 0; i < tileArray.Length; i++) {
            if (tileArray[i] == normalTile) {
                validTiles[i] = true;
            }
        }

        towerScript = tower.GetComponent<TowerMovement>();
    }
 
    private void Update() {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newMousePos = tilemap.WorldToCell(worldPoint);
    }
}