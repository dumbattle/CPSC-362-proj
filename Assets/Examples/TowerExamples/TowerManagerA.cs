using System.Collections.Generic;
using UnityEngine;

namespace AustinsExamples.Tower {
    public class TowerManagerA : MonoBehaviour {
        ITower[,] _towers;


        public bool TileOccupied(int x, int y) {
            // bounds check

            return _towers[x, y] != null;
        }

        public ITower PlaceTower(ITower src, int x, int y) {
            var tt = src; // create new tower

            _towers[x, y] = tt;
            return tt;
        }

        public IEnumerable<ITower> AllTowers() {

            for (int x = 0; x < _towers.GetLength(0); x++) {
                for (int y = 0; y < _towers.GetLength(1); y++) {

                    var t = _towers[x, y];
                    if (t != null) {
                        yield return t;
                    }
                }
            }
        }

        public void GameplayUpdate() {
            foreach (var t in AllTowers()) {
                t.GameplayUpdate();
            }
        }

        public void RemoveTower(int x, int y) {
            _towers[x, y] = null;
        }
    }
}
