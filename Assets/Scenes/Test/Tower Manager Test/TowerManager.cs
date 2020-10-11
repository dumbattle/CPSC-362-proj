using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Data members
    Tower[,] towerTracker;                          // 2D array of Towers
    public GameObject tower;                        // Only one tower for testing purposes
    Tower[] towerList;                              // List of towers

    void Awake()
    {
        towerTracker = new Tower[15, 15];           // Setup trackerArray
        towerList = new Tower[255];
    }

    Tower[] AllTowers()
    {
        return towerList;
    }

    GameObject GetTower(int x, int y)
    {
        return towerTracker[x, y].type;
    }

    bool TileOccupied(int x, int y)
    {
        return !towerTracker[x, y].available;           // Is the tower at (x, y) active?
    }

    public void CreateTower(int x, int y)                  
    {
        if ( !TileOccupied(x, y) )
        {
            Instantiate(tower, new Vector3Int(x, y, 0), Quaternion.identity);   // Create tower object
            towerTracker[x, y].available = false;                               // Tile is now unavailable
            towerTracker[x, y].type = tower;                                    // Set tower reference

        } 
        else
        {
            UnityEngine.Debug.Log("Tile Occupied");         // Tile is already occupied
        }
    }

    void RemoveTower(int x, int y)
    {
        if ( TileOccupied(x, y) )
        {
            tower = GetTower(x, y);                          // Get tower reference
            Destroy(tower);                                 // Destroy tower
            towerTracker[x, y].available = true;            // Tile is now available
        }
        else
        {
            UnityEngine.Debug.Log("Nothing to destroy");    // Nothing was on the tile
        }
    }

    public class Tower
    {
        public GameObject type = null;                 // The type of tower, if any
        public bool available = true;                  // Is the tile available?
    }
}
