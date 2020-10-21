using UnityEngine;


namespace AustinsExamples.GameState {
    public class ExampleGameController : MonoBehaviour {
        // the game state will be tracked as a function
        // when called, it will carry out its duties, then return the next gamestate (could be the same gamestate)
        delegate GameState GameState(); 

        public DummyManager dm;
        public SpriteRenderer tile; // for visualization of tile and color
        public Dummy dummy; // what to spawn

        // current gameState
        GameState gameState;

        private void Awake() {
            // initial gamestate
            gameState = SceneStartState;
        }

        private void Update() {
            // call gameState
            // it returns the next gameState
            // if it is null, keep old gameState            
            gameState = gameState() ?? gameState; // does this look weird? yes it does
            /*
             * the following:
             * => A = B ?? C; 
             * 
             * is equal to:
             * => if (B != null) {
             * =>   A = B;
             * => }
             * => else {
             * =>   A = C;
             * => }
             */
        }


        //----------Helper Functions---------
        void SpawnDummy() {
            if (GameStateUIManager.click) {
                Vector2 t = GameStateUIManager.tile;

                var d = dm.SpawnDummy(dummy);
                d.transform.position = t;
                d.sr.color = GameStateUIManager.color;
            }
        }

        void UpdateTile() {
            if (UIManager.colorRecieved) {
                tile.color = UIManager.color;
            }

            if (UIManager.tileRecieved) {
                tile.transform.position = new Vector3(UIManager.tile.x, UIManager.tile.y);
            }
        }
        //----------Game State Functions---------

        // default state
        GameState SceneStartState() {
            dm.SpawnDummy(dummy); // spawn 1 for testing

            // we are moving to play state
            // Tell the UI manager to set the corret UI
            GameStateUIManager.SetPlayState();
            return PlayState; // next state
        }

        GameState PlayState() {
            // Dummy and DummyManager can't update themselves
            dm.GameplayUpdate();

            // SpawnDummy performs input check, so no need to check for input here
            SpawnDummy();

            // Same reasoning
            UpdateTile();

            // move to pause state
            if (GameStateUIManager.pausedRecieved) {
                // set correct UI
                GameStateUIManager.SetPauseState();
                return PausedState;
            }

            // null means same state
            return null;
        }

        GameState PausedState() {
            // No Gameplay Update
            // No SpawnDummy
            // This is how to achieve pausing
            // If we want, we could have a PauseUpdate() and call it here

            // we still want this functionallity
            UpdateTile();


            if (GameStateUIManager.playRecieved) {
                GameStateUIManager.SetPlayState();
                return PlayState;
            }
            return null;
        }
    }
}