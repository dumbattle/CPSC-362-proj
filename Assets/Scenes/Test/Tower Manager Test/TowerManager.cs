using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class ActionFailedException : Exception
{
    public ActionFailedException(string msg) : base(msg) { }
}

public class TowerManager : MonoBehaviour, ITower
{
    ITower[,] _towers;
    private Vector2Int index;
    public Vector2Int mapIndex { get { return index; } }
    private GameObject obj;
    public GameObject reference {  get { return obj; } }
    private int numOfTowers = 0;

    void Awake()
    {
        _towers = new ITower[15, 15];
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                _towers[i, j] = null;
            }
        }
    }

    public bool TileOccupied(int x, int y)
    {
        return _towers[x, y] != null;
    }

    public ITower CreateTower(GameObject src, int x, int y)
    {
        if (!TileOccupied(x, y))
        {
            GameObject clone = null;                                                                            // Create null GameObject for reference to cloned object
            clone = Instantiate(src, new Vector3Int(x, y, 0), Quaternion.identity);                             // Create tower object
            clone.transform.name = transform.name.Replace("TowerManager", "Tower1." + ++numOfTowers).Trim();    // Rename tower to Tower1.numOfTower
            obj = clone;                                                                                        // Set obj for ITower.GamObject reference get
            index = new Vector2Int(x, y);                                                                       // Set index for ITower.MapIndex get
            ITower tt;                                                                                          // Create ITower -- Not sure if this is working how I think it is
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
            index = new Vector2Int(x, y);                                                                       // Set index for ITower.MapIndex get
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
        }
    }

    public void GameplayUpdate()
    {
        foreach (var t in AllTowers())
        {
            t.GameplayUpdate();
        }
    }

    public void RemoveTower(int x, int y)
    {
        ITower towerToDestroy;
        if (TileOccupied(x, y))
        {
            towerToDestroy = GetTower(x, y);                                                                    // Get tower reference
            Destroy(towerToDestroy.reference);                                                                  // Destroy tower
            _towers[x, y] = null;
        }
        else
        {
            throw new ActionFailedException("TowerManager::RemoveTower - Tile unoccupied at (" + x + ", " + y + ")"); // Nothing was on the tile
        }
    }
}
