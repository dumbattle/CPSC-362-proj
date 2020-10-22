using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.UI;

public class UIInput : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile normalTile;                             // tile where towers can be placed
    public Tile highlightTile;                          // tile when player hovers over a valid tile for tower placement
    public Tile pathTile;                               // tile where enemies travel

    private Vector3Int newMousePos;                     // most updated mouse position relative to Tilemap grid
    private Vector3Int oldMousePos;                     // previous mouse position relative to Tilemap grid

    bool[] validTiles;                                  // 1D represenation of valid placement tiles in Tilemap

    public TowerBehaviour tower;
    private TowerBehaviour towerScript;
    public TowerManager towerManagerScript;

    private bool ToggleState = true;                    // indicator for building toggle
    public UnityEngine.UI.Button toggleButton;


    // Method for when mouse hovers over Tilemap GameObject
    private void OnMouseOver()
    {
        if (!ToggleState)
        {
            if (oldMousePos != newMousePos)
            {
                // When the mouse is over a new tile and if the previous tile was highlighted, 
                // revert it to a normal tile
                if (tilemap.GetTile<Tile>(oldMousePos) == highlightTile)
                {
                    tilemap.SetTile(oldMousePos, normalTile);
                }
                // Update previous mouse location to current location
                oldMousePos = newMousePos;
            }
            // When the mouse hovers over a new valid tile for tower placement, highlight it
            if (tilemap.GetTile<Tile>(newMousePos) == normalTile && tilemap.HasTile(newMousePos) && validTiles[getTileIndex()])
            {
                tilemap.SetTile(newMousePos, highlightTile);
            }
        }
    }

    // Method for when mouse is not hovering over Tilemap Gameobject
    private void OnMouseExit()
    {
        if (!ToggleState)
        {
            // If the previous tile was highlighted, revert it to a normal tile
            if (tilemap.GetTile<Tile>(oldMousePos) == highlightTile)
            {
                tilemap.SetTile(oldMousePos, normalTile);
            }
        }
    }

    // Method for when left mouse button is pressed
    private void OnMouseDown()
    {
        if (!ToggleState)
        {
            (int, int) relativeXY = getRelativeXY();
            int tileIndex = getTileIndex();

            // If the current location is a valid tile, create a tower at that location and set
            // the location to no longer be valid for additional tower placement
            if (validTiles[tileIndex])
            {
                towerManagerScript.GetComponent<TowerManager>().CreateTower(tower, relativeXY.Item1, relativeXY.Item2);
                validTiles[tileIndex] = false;
            }
        }
    }

    // return the x,y coordinates (as a tuple) of the grid as if the bottom left cell is (0,0) 
    private (int, int) getRelativeXY()
    {
        if (!ToggleState)
        {
            int relativeX = newMousePos[0] - tilemap.cellBounds.xMin;
            int relativeY = newMousePos[1] - tilemap.cellBounds.yMin;

            return (relativeX, relativeY);
        }
        return (-1, -1);
    }

    // return the index of a tile as if it were a 1D array based on the relative x, y coordinates
    private int getTileIndex()
    {
        if (!ToggleState)
        {
            // get relative coordinates of x, y
            (int, int) relativeXY = getRelativeXY();

            return relativeXY.Item1 + (tilemap.cellBounds.size[0] * relativeXY.Item2);
        }
        return -1;
    }

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleBuild);
        tilemap = gameObject.GetComponent<Tilemap>();

        // Set oldMousePos to a default position
        oldMousePos = new Vector3Int(0, 0, 0);

        // Get the bounds of the Tilemap grid
        BoundsInt bounds = tilemap.cellBounds;
        // Create an 1D array of all the tiles in the Tilemap
        TileBase[] tileArray = tilemap.GetTilesBlock(bounds);

        // Establish valid tower placement tiles 
        validTiles = new bool[tileArray.Length];
        for (int i = 0; i < tileArray.Length; i++)
        {
            if (tileArray[i] == normalTile)
            {
                validTiles[i] = true;
            }
        }

        towerScript = tower.GetComponent<TowerBehaviour>();
    }


    private void Update()
    {
        // When the game advances one frame, update location of mouse
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newMousePos = tilemap.WorldToCell(worldPoint);
    }

    public void ToggleBuild()
    {
        UnityEngine.Debug.Log("Toggle Clicked");
        ToggleState = !ToggleState;
    }

    void OnDestroy()
    { // opposite of start
        toggleButton.onClick.RemoveListener(ToggleBuild); // undo AddListener
    }
}