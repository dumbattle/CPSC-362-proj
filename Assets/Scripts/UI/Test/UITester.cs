using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UITester : MonoBehaviour
{
    public Tile fieldTile;
    public Tile pathTile;
    public Tile highlightTile;
    private bool[] validTiles;

    void Start() {
        validTiles = new bool[TestUIManager.tiles.Length];
        for (int i = 0; i < validTiles.Length; i++) {
            if (TestUIManager.tiles[i] == fieldTile) {
                validTiles[i] = true;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (TestUIManager.towerReceived) {
            // test code; ignore for now
            //highlight.transform.position = new Vector3(TestUIManager.tilePosition.Item1, TestUIManager.tilePosition.Item2);

            if (TestUIManager.clickReceived && validTiles[TestUIManager.tileIndex]){
                GameObject tower = Instantiate(TestUIManager.towerSelected,
                                               new Vector3Int(TestUIManager.tilePosition.Item1, TestUIManager.tilePosition.Item2, 0),
                                               Quaternion.identity
                                               );
                tower.SetActive(true);
                validTiles[TestUIManager.tileIndex] = false;
            }
        }
    }
}
