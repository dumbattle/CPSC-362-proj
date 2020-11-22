using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

    public partial class UIRefactorGC : MonoBehaviour{
    delegate GameState GameState();

    

    public MapManager mm;
    public WaveSpawner ws;
    public CreepManager cm;
    public TowerManager tm;
    public SimpleEconomyManager em;
    public GameObject tileHighlight;
    public TowerUIManager tui;

    [Space]
    public Text winText;
    public Text loseText;
    public Text waveDisplay;

    GameState gameState;
    int currentWave = 0;

    void Start() {
        tileHighlight.SetActive(false);
        ws.Init(cm);
        em.Init(100, 100);

        HighlightPooler._main.poolSize = mm.MapSize().x * mm.MapSize().y;
    }

    private void ShowTileHighlights() {
        // check to see if the pool of highlights exist; otherwise, create one
        if (!HighlightPooler._main.poolCreated) {
            HighlightPooler._main.CreatePool();
        }

        // this nested function is used for the outer method to activate a 
        // new highlight every time it's called
        void HighlightTile(int x, int y) {
            GameObject highlight = HighlightPooler._main.GetPooledHighlight();
            highlight.transform.position = new Vector3(x, y, 0);
            highlight.SetActive(true);
        }

        // get the map size to set the upper limits
        Vector2Int mapSize = mm.MapSize();
        
        // loop through every tile position in the tilemap
        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                // the TileInRange condition shouldn't be necessary but 
                // TowerManager still has a hardcoded size
                if (tm.TileInRange(x, y)) {
                    // check to see if the tile is valid, then activate a highlight
                    // over that position
                    if (mm.ValidTowerTile(x, y) && !tm.TileOccupied(x, y)) {
                        HighlightTile(x, y);
                    }
                }
            }
        }
    }

    private void HideTileHighlights() {
        HighlightPooler._main.DeactivateHighlights();
    }

    private void Awake() {
        gameState = SceneStartState;
    }


    private void Update() {
        gameState = gameState() ?? gameState;
        displayWave();
        TouchUIManager.CustomUpdate();
        UIManager.CustomUpdate();
    }


    void GameplayUpdate() {
        cm.GameplayUpdate();
        tm.GameplayUpdate();
        GlobalGameplayUpdate.GameplayUpdate(); // this was added
    }

    //refactored
    void WaitUpdate() {
        tm.WaitUpdate();
        GlobalGameplayUpdate.WaitUpdate(); // this was added
    }

    public void displayWave()
    {
        int waveNumber = 1 + currentWave;
        waveDisplay.text = waveNumber.ToString();
    }


    // GAME STATES ARE SPLIT INTO MULTIPLE FILES 

    GameState SceneStartState() {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        PlayPauseUIManager.SetPauseState();
        return WavePrepState; 
    }

    GameState WaveStartState() {
        ws.SetWave(currentWave);
        return WaveSpawnState;
    }

    GameState WaveSpawnState() {
        ws.SpawnUpdate();
        GameplayUpdate();

        if (em.health <= 0) {
            loseText.gameObject.SetActive(true);
            return GameEndState;
        }
        if (UIManager.pausedReceived) {
            PlayPauseUIManager.SetPauseState();
            return WaveSpawnPauseState;
        }

        if (ws.spawningDone) {
            return PlayState;
        }
        return null;
    }


    GameState WaveSpawnPauseState() {
        if (UIManager.playReceived) {
            PlayPauseUIManager.SetPlayState();
            return WaveSpawnState;
        }

        return null;
    }

    GameState PlayState() {
        GameplayUpdate();
        if (em.health <= 0) {
            loseText.gameObject.SetActive(true);
            return GameEndState;
        }

        if (UIManager.pausedReceived) {
            PlayPauseUIManager.SetPauseState();
            return PausedState;
        }

        if (cm.creepCount == 0) {
            PlayPauseUIManager.SetPlayState();
            return WaveEndState;
        }
        return null;
    }

    GameState PausedState() {
        if (UIManager.playReceived) {
            PlayPauseUIManager.SetPlayState();
            return PlayState;
        }
        return null;
    }

    GameState WaveEndState() {
        currentWave++;
        PlayPauseUIManager.SetPauseState();

        if (currentWave > ws.MaxWave) {
            winText.gameObject.SetActive(true);
            return GameEndState;
        }
        return WavePrepState;
    }

    GameState GameEndState() {
        WaitUpdate();
        // do nothing :)
        return null;
    }

    // utility state
    GameState WaitState(float time, GameState next) {
        return () => {
            GameplayUpdate();
            if (time <= 0) {
                return next;
            }
            return WaitState(time - .01f, next);
        };
    }
}
