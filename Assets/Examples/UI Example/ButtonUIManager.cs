using UnityEngine;
using UnityEngine.UI;


namespace AustinsExamples.UI {
    public class ButtonUIManager : UIManager {
        // 2 buttons for user to input a color
        public Button redButton;
        public Button greenButton;

        void Start() {
            // we assign callbacks here instead of assigning it to the buttons inspector
            // this way, we will get a NullReferenceException if we forget to assign buttons to this script
            // if we try to assign via the inspector, we would get no error if we forgot to assign
            redButton.onClick.AddListener(() => Register.Color(Color.red));
            greenButton.onClick.AddListener(() => Register.Color(Color.green));
            // I used lamba functions for this. If you are unfamiliar with them, you can use google or ask me in discord
        }

        void Update() {
            // clear last frames input
            // do this before we register input for this frame
            Clear(); 

            // tile input will be the mouse position
            var p =  Camera.main.ScreenToWorldPoint(Input.mousePosition); // world position

            var x = Mathf.RoundToInt(p.x);
            var y = Mathf.RoundToInt(p.y);   

            Register.Tile(x, y);


            // alternate way to input a color
            // right click to choose random color
            if (Input.GetMouseButtonDown(1)) {
                Register.Color(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f)));
            }
        }
    }
}