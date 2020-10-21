using UnityEngine;
using UnityEngine.UI;

namespace AustinsExamples.GameState {
    public class ExampleGSUIManager : GameStateUIManager {
        public Button playButton;
        public Button pauseButton;
        public Button playPauseButton; // alternate method. use a single button for both purposes
        public Text playPauseText;     // text will be updated according to game state

        [Space] // inspector organization
        public Button colorMenuButton; // open/close color menu
        public GameObject colorMenuPanel; // the thing that will be opened /closed

        public Button[] colorButtons; // can have as many buttons as we like

        // true the frame a button is pressed
        // this will be used privately to prevent clicks from being registered when a button is clicked
        // we don't want 2 input signals to be sent for a single action
        bool buttonClicked = false;

        void Start() {
            playButton.onClick.AddListener(() => Register.Play());
            pauseButton.onClick.AddListener(() => Register.Pause());

            foreach (var b in colorButtons) {
                // we are using the base UIManager.Register
                b.onClick.AddListener(() => UIManager.Register.Color(b.GetComponent<Image>().color));
                b.onClick.AddListener(() => buttonClicked = true);
            }

            // colorMenuButton does not register input. It is use within this class for UI organization
            colorMenuButton.onClick.AddListener(() => colorMenuPanel.gameObject.SetActive(!colorMenuPanel.gameObject.activeInHierarchy));

            playButton.onClick.AddListener(() => buttonClicked = true);
            pauseButton.onClick.AddListener(() => buttonClicked = true);
            colorMenuButton.onClick.AddListener(() => buttonClicked = true);
        }

        void Update() {
            Clear();

            // tile input will be the mouse position
            // same as in UI example
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition); // world position

            var x = Mathf.RoundToInt(p.x);
            var y = Mathf.RoundToInt(p.y);

            UIManager.Register.Tile(x, y);


            // space as alternate way to press Play/Pause button
            if (Input.GetKeyDown(KeyCode.Space)) {
                // bugged?
                // To reproduce
                // 1. press play or pause button, state switches as expected
                // 2. press space
                //      - expected: state switches
                //      - actual: state switches TWICE, ending up in original state. Why???
                //          - I used print() to figure that part out
                // 3. repeat step 2
                //      - will be fixed if you do:
                //          - click anywhere????? how does this even work
                //          - wtf im so confused
                //          - that makes no sense
                //      
                // possible solutions: 
                // 1. Don't use this method  
                //
                // I found the problem:
                //      - The space bar by default CLICKS THE LAST BUTTON YOU CLICKED
                //          - So space bar was Registering play/pause, then the button is called and registers it again
                //      - Why is this even a thing
                //      - How do I disable this?
                //      - Where is documentation for this Unity?
                //      - I got SO SO SO SO lucky that I found this
                //          - I was spamming spacebar trying to figure out what was happening
                //          - My cursor happened to be hovering over the LAST BUTTON THAT I PRESSED
                //          - Whenever I hit spacebar, the button LIT UP AS IF IT WERE BEING CLICKED
                //      - Solution: Don't use spacebar  >:(
                // I could fix this, but I'll leave this here so you can see my 'debugging' process
                playPauseButton.onClick.Invoke();

            }

            // click! but only if a button was not clicked this frame
            if (Input.GetMouseButtonUp(0) && !buttonClicked) {
                Register.Click();
            }

            // reset after this has been used
            buttonClicked = false;
        }



        // set the correct UI elements
        protected override void ShowPlayUI() {
            playPauseText.text = "Pause\n(Space)";

            playButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);

            colorMenuPanel.gameObject.SetActive(false); // close color menu
            PlayPause_Play(); 
        }

        // set the correct UI elements
        protected override void ShowPausedUI() {
            playPauseText.text = "Play\n(Space)";

            playButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);

            colorMenuPanel.gameObject.SetActive(false);
            PlayPause_Pause();
        }

        // pause state
        // probably should use a better name
        void PlayPause_Pause() {
            playPauseButton.onClick.RemoveAllListeners(); // don't wan't to accumulate listeners
            playPauseButton.onClick.AddListener(() => Register.Play()); // subscribe correct function
            playPauseButton.onClick.AddListener(() => buttonClicked = true); // re-add since it was cleared
        }

        // play state
        void PlayPause_Play() {
            playPauseButton.onClick.RemoveAllListeners();
            playPauseButton.onClick.AddListener(() => Register.Pause());
            playPauseButton.onClick.AddListener(() => buttonClicked = true);
        }
    }
}