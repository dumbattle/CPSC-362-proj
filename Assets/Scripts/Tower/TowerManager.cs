using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class ActionFailedException : Exception
{
    public ActionFailedException(string msg) : base(msg) { }
}

public class TowerManager : MonoBehaviour
{
    ITower[,] _towers;
    private int numOfTowers = 0;

    void Awake()
    {
        _towers = new ITower[16, 16];
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                _towers[i, j] = null;
            }
        }
    }
    public bool TileInRange(int x, int y) {
        return x >= 0 && y >= 0 && x < _towers.GetLength(0) && y < _towers.GetLength(1);
    }
    public bool TileOccupied(int x, int y)
    {
        return _towers[x, y] != null;
    }

    public ITower CreateTower(TowerBehaviour src, int x, int y)
    {
        if (!TileOccupied(x, y))
        {
            Vector2Int index;
            TowerBehaviour clone = null;                                                                        // Create null GameObject for reference to cloned object
            clone = Instantiate(src, new Vector3Int(x, y, 0), Quaternion.identity);                             // Create tower object
            clone.transform.name = transform.name.Replace("TowerManager", "Tower1." + ++numOfTowers).Trim();    // Rename tower to Tower1.numOfTower
            clone.gameObject.SetActive(true);
            index = new Vector2Int(x, y);                                                                       // Set index for ITower.MapIndex get
            _towers[x, y] = clone;                                                                              
            clone.Init(index, src);
            return clone;
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
        }
    }

    public void GameplayUpdate()
    {
        foreach (var t in AllTowers())
        {
            t.GameplayUpdate();
        }
    }

     public void WaitUpdate()
     {
          foreach (var t in AllTowers())
          {
               t.WaitUpdate();
          }
     }

    public void RemoveTower(int x, int y)
    {
        if (TileOccupied(x, y))
        {
            _towers[x, y] = null;
        }
        else
        {
            throw new ActionFailedException("TowerManager::RemoveTower - Tile unoccupied at (" + x + ", " + y + ")"); // Nothing was on the tile
        }
    }


}
