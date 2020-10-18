<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
=======
﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
>>>>>>> Stashed changes
using UnityEngine;
using System;





public class ActionFailedException : Exception
{
    public ActionFailedException(string msg) : base(msg) { }
}



public class TowerManager : MonoBehaviour, ITower
{
<<<<<<< Updated upstream
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
=======
    ITower[,] _towers;
    private Vector2Int index;
    public Vector2Int mapIndex { get { return index; } }
    private int numOfTowers = 0;


    void Awake()
    {
        _towers = new ITower[15, 15];                                           // Setup trackerArray

        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                _towers[i, j] = null;                                           // Initialize towerTracker
            }
        }
    }

    public bool TileOccupied(int x, int y)
>>>>>>> Stashed changes
    {
        return _towers[x, y] != null;
    }

<<<<<<< Updated upstream
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
=======
    public ITower CreateTower(GameObject src, int x, int y)
    {
        ITower tt = src;
        GameObject clone = null;
        if (!TileOccupied(x, y))
        {
            index = new Vector2Int(x, y);
            clone = Instantiate(src, new Vector3Int(x, y, 0), Quaternion.identity);                           // Create tower object
            clone.transform.name = transform.name.Replace("TowerManager", "Tower1." + ++numOfTowers).Trim();    // Rename tower to Tower1.numOfTower
            _towers[x, y] = tt;                                                                                 // Add src to _towers
            return tt;
        }
        else
        {
            throw new ActionFailedException("TowerManager::PlaceTower - Tile occupied at (" + x + ", " + y + ")");   // Tile is already occupied
        }
    }

    public ITower GetTower(int x, int y)
    {
        if (TileOccupied(x, y))
        {
            return _towers[x, y];
        }
        else
        {
            throw new ActionFailedException("TowerManager::GetTower - Tile unoccupied at (" + x + ", " + y + ")");
        }
    }

     public IEnumerable<ITower> AllTowers()
     {
        for (int x = 0; x < _towers.GetLength(0); x++)
        {
            for (int y = 0; y < _towers.GetLength(1); y++)
            {
                var t = _towers[x, y];
                if (t != null)
                {
                    yield return t;
                }
            }
>>>>>>> Stashed changes
        }
    }

    public void GameplayUpdate()
    {
        foreach (var t in AllTowers())
        {
<<<<<<< Updated upstream
            UnityEngine.Debug.Log("Nothing to destroy");    // Nothing was on the tile
        }
    }

    public class Tower
    {
        public GameObject type = null;                 // The type of tower, if any
        public bool available = true;                  // Is the tile available?
=======
            t.GameplayUpdate();
        }
    }

    public void RemoveTower(int x, int y)
    {
        GameObject towerToDestroy;
        if (TileOccupied(x, y))
        {
            towerToDestroy = GetTower(x, y);                                        // Get tower reference
            Destroy(towerToDestroy);                                                // Destroy tower
            _towers[x, y] = null;
        }
        else
        {
            throw new ActionFailedException("TowerManager::RemoveTower - Tile unoccupied at (" + x + ", " + y + ")"); // Nothing was on the tile
        }
>>>>>>> Stashed changes
    }
}
