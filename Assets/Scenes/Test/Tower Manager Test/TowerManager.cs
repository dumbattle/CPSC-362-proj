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
        // Currently, each element is null
        // you need to initialize each element in a loop
        // alternatively you could make Tower a struct
        // -Austin
    }

    Tower[] AllTowers()
    {
        throw new System.NotImplementedException(); // fix compilier error -Austin
    }

    Tower GetTower(int x, int y)
    {
        throw new System.NotImplementedException(); // fix compilier error -Austin
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

    public class Tower // Should be struct
    {
        public string type;                         // The type of tower, if any
        public bool active;                         // Is a tower active on this tile?
        // should contain reference to actual tower
    }
}

// How I would do this. 
// Not tested. If you want to test:
//  - move these behaviours to their own files with filenames matching their class name
//  - create a test script to test the manager functions
//  - you will need to create a dummy tower
//    - create sprite and add the TowerBehaviourDummy script to it
//    - if you want, create 2 dummy towers and try to get that to work
// -Austin
namespace AustinsExamples {
    public class TowerManagerExample : MonoBehaviour {
        TowerBehaviourDummy[,] _towers; // track towers. null => no tower

        // don't put in start because map size might not be known
        // a GameController will call this function and pass in the correct size
        public void Initialize(int x, int y) {
            _towers = new TowerBehaviourDummy[x, y]; // each element is null
        }


        // Returning an array would be inefficient, since the number of towers is variable
        // IEnumerable<T> are similar to c++ iterators
        // To use this, you would do:
        // foreach (TowerBehaviour t in AllTowers()) {
        //    print(t.transform.position);
        // }
        public IEnumerable<TowerBehaviourDummy> AllTowers() {
            for (int x = 0; x < _towers.GetLength(0); x++) {
                for (int y = 0; y < _towers.GetLength(1); y++) {
                    var t = _towers[x, y];

                    if (t != null) {
                        yield return t; // returns 1 element, but continue
                    }
                }
            }
        }


        public TowerBehaviourDummy GetTower(int x, int y) {
            return _towers[x, y];
        }

        public bool TileOccupied(int x, int y) {
            return _towers[x, y] == null;
        }

        // src is the tower that will be created
        // (x, y) is the map index the tower will be in
        // worldPosition is where to place in the world. 
        //  - Alternatively, we could use a MapManager to convert (x, y) to a world position
        public TowerBehaviourDummy CreateTower(TowerBehaviourDummy src, int x, int y, Vector2 worldPosition) {
            if (TileOccupied(x,y)) {
                throw new System.InvalidOperationException($"Cannot place tower at ({x}, {y}) because it is already occupied");
            }

            var go = Instantiate(src.gameObject, worldPosition, Quaternion.identity);

            var tb = go.GetComponent<TowerBehaviourDummy>();

            _towers[x, y] = tb;
            return tb;
        }


        public void RemoveTower(int x, int y) {
            _towers[x, y] = null;
            // tower destruction should not occur here
        }

    }


    // Main tower component
    // No functionality
    // testing only
    public class TowerBehaviourDummy : MonoBehaviour {

    }
}