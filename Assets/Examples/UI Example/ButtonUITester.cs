using UnityEngine;


namespace AustinsExamples.UI {
    public class ButtonUITester : MonoBehaviour {
        // for visualization
        public SpriteRenderer tile;

        // don't read raw user input
        // instead use UIManager for input
        void Update() {
            if (UIManager.colorRecieved) {
                tile.color = UIManager.color;
            }

            if (UIManager.tileRecieved) {
                tile.transform.position = new Vector3(UIManager.tile.x, UIManager.tile.y);
            }
        }
    }
}