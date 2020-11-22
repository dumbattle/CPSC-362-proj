using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPooler : MonoBehaviour
{
    public static HighlightPooler _main;            // class object reference
    [System.NonSerialized]
    public int poolSize;                            // max size of object pool

    public GameObject pooledHighlight;              // single object in pool
    private List<GameObject> pooledHighlights;      // list of all pooled objects
    [System.NonSerialized]
    public bool poolCreated = false;             
    
    void Awake() {
        _main = this;
    }

    public void CreatePool() {
        pooledHighlights = new List<GameObject>();

        // instantiate as many objects up to the poolSize and add them to 
        // the pool list
        for (int i = 0; i < poolSize; i++) {
            GameObject highlight = Instantiate(pooledHighlight);
            highlight.SetActive(false);
            pooledHighlights.Add(highlight);
        }

        poolCreated = true;
    }

    // returns a deactivated object from the pool
    public GameObject GetPooledHighlight() {
        for (int i = 0; i < pooledHighlights.Count; i++) {
            if (!pooledHighlights[i].activeInHierarchy) {
                return pooledHighlights[i];
            }
        }
        return null;
    }

    // deactivate all the objects in the pool
    public void DeactivateHighlights() {
        foreach (GameObject highlight in pooledHighlights) {
            highlight.SetActive(false);
        }
    }
}
