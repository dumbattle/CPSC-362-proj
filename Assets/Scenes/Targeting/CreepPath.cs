using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreepPath : MonoBehaviour {
    public Color color= new Color(1,1,1,1); // for editor usae

    //[ArrayLabel("Tile")]
    [HideInInspector]
    public List<Vector2Int> path = new List<Vector2Int>() { new Vector2Int(0, 0), new Vector2Int(0, 1) };

    public Grid grid;


}
