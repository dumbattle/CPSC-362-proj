using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.UI;

public class FailedTestException : Exception
{
    public FailedTestException(string msg) : base(msg) { }
}

// Test to see if TowerManager is working
public class TowerManagerTest : MonoBehaviour
{ 
    // place TowerManager here
    public TowerManager tm;

    // dummy towers for testing
    public TowerBehaviour src;

    void Start()
    {
        print("Test Starting");

        if (tm == null)
        {
            throw new FailedTestException("TowerManager is not assigned to TowerManagerTest");
        }
        if (src == null)
        {
            throw new FailedTestException("tower1 is not assigned to TowerManagerTest");
        }

        print("Creating 3 copies of tower1 at (0, 0), (3, 3), and (5, 5)");

        ITower t1_1 = tm.CreateTower(src, 0, 0);
        ITower t1_2 = tm.CreateTower(src, 3, 3);
        ITower t1_3 = tm.CreateTower(src, 5, 5);

        print("Checking those tiles are now occupied");
        if (!tm.TileOccupied(0, 0))
        {
            throw new FailedTestException("Tile (0, 0) should be occupied, but is not");
        }
        if (!tm.TileOccupied(3, 3))
        {
            throw new FailedTestException("Tile (3, 3) should be occupied, but is not");
        }
        if (!tm.TileOccupied(5, 5))
        {
            throw new FailedTestException("Tile (5, 5) should be occupied, but is not");
        }

        print("Retrieving recently created towers");
        // modify GetTower to return the tower as an ITower
        if (tm.GetTower(0, 0) != t1_1)
        {
            throw new FailedTestException("The retrieved tower at (0, 0) does not match the tower that was created");
        }
        if (tm.GetTower(3, 3) != t1_2)
        {
            throw new FailedTestException("The retrieved tower at (3, 3) does not match the tower that was created");
        }
        if (tm.GetTower(5, 5) != t1_3)
        {
            throw new FailedTestException("The retrieved tower at (5, 5) does not match the tower that was created");
        }
        List<Vector2Int> tl = new List<Vector2Int>();


        print("Clearing Towers");
        // change AllTowers to return some sort of ITower collection
        foreach (ITower t in tm.AllTowers())
        {
            tl.Add(t.mapIndex);
        }

        foreach (var ind in tl)
        {
            tm.RemoveTower(ind.x, ind.y);
        }

        print("Checking the tiles are now clear");
        if (tm.TileOccupied(0, 0))
        {
            throw new FailedTestException("Tile (0, 0) should be unoccupied, but is not");
        }
        if (tm.TileOccupied(3, 3))
        {
            throw new FailedTestException("Tile (3, 3) should be unoccupied, but is not");
        }
        if (tm.TileOccupied(5, 5))
        {
            throw new FailedTestException("Tile (5, 5) should be unoccupied, but is not");
        }

    }
}