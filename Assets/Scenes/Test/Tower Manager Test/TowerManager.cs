using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Data members
    Tower[,] towerTracker;                          // 2D array of Towers


    void Awake()
    {
        towerTracker = new Tower[15, 15];           // Setup trackerArray
    }

    Tower[] AllTowers()
    {

    }

    Tower GetTower(int x, int y)
    {

    }

    bool TileOccupied(int x, int y)
    {
        return towerTracker[x, y].active;           // Is the tower at (x, y) active?
    }

    void CreateTower(int x, int y)
    {

    }

    void RemoveTower(int x, int y)
    {

    }

    public class Tower
    {
        public string type;                         // The type of tower, if any
        public bool active;                         // Is a tower active on this tile?
    }
}
