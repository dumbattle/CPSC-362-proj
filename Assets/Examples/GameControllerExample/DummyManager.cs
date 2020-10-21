using System.Collections.Generic;
using UnityEngine;


namespace AustinsExamples.GameState {
    // example GameManager
    public class DummyManager : MonoBehaviour {

        // keep track of all dummies
        // list is probably not the ideal collection
        List<Dummy> dummies = new List<Dummy>();


        // spawn a dummy
        public Dummy SpawnDummy(Dummy src) {
            var d = Instantiate(src, Vector3.zero, Quaternion.identity);

            d.gameObject.SetActive(true); // src dummy might be inactive
            d.Init(); // dummy can't init itself
            dummies.Add(d);

            return d;
        }

        // don't use update
        public void GameplayUpdate() {
            foreach (var d in dummies) {
                d.GameplayUpdate();
            }
        }

        //********************EXAMPLE METHODS********************
        // These are not being use currently and have not been tested

        // IEnumerable<Dummy> allows us to use any type of collection for dummies, and is readonly
        public IEnumerable<Dummy> AllDummies() {
            return dummies;
        }

        // return true if a dummy was sucessfully removed
        public bool RemoveDummy(Dummy d) {
            return dummies.Remove(d);
        }
    }
}