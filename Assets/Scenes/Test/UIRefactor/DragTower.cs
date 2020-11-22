using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragTower : UIManager
{
    public Transform prefab ;
     
     private Transform spawn;

     private Rect rect = new Rect(0,0,100,50);
     
     void Update() {
         
         
         if (Input.GetMouseButton(0) && spawn != null) {
             var pos = Input.mousePosition;
             pos.z = -Camera.main.transform.position.z;
             spawn.transform.position = Camera.main.ScreenToWorldPoint(pos);
         }
         
         if (Input.GetMouseButtonUp(0)) {
             spawn = null;
         }
     }
 
     void OnGUI() {
         Event e = Event.current;
         
         if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition)) {
             var pos = Input.mousePosition;
             pos = Camera.main.ScreenToWorldPoint(pos);
             spawn = Instantiate(prefab, pos, Quaternion.identity) as Transform;
         }
         
         GUI.Button (rect, "Button");
    }

}
