using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    TowerBehaviour tower;
    public TowerManager tm;
    public MapManager mm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData data) {
        tower = Instantiate(UIManager.towerPurchased, UIManager.mousePosition, Quaternion.identity);
        tower.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData data) {
        
        //transform.Translate(0, 0, Time.deltaTime);
        tower.transform.position = UIManager.mousePosition;
    }

    public void OnEndDrag(PointerEventData data) {
        var (x, y) = mm.GetTilePosition(UIManager.mousePosition);
        tm.CreateTower(UIManager.towerPurchased, x, y);
        Destroy(tower.gameObject);
    }
}
