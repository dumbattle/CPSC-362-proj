using UnityEngine;
using UnityEngine.UI;

public class TestGameController : MonoBehaviour{
    delegate GameState GameState();

    public MapManager mm;
    public WaveSpawner ws;
    public CreepManager cm;
    public TowerManager tm;
    public SimpleEconomyManager em;
    public GameObject tileHighlight;

    [Space]
    public Text winText;
    public Text loseText;

    GameState gameState;
    int currentWave = 0;


    void Start() {
        tileHighlight.SetActive(false);
        ws.Init(cm);
        em.Init(100, 100);
    }

    private void Awake() {
        gameState = SceneStartState;
    }


    private void Update() {
        gameState = gameState() ?? gameState;
        UIManager.CustomUpdate(); // check UIManager Line 78
    }


    void GameplayUpdate() {
        cm.GameplayUpdate();
        tm.GameplayUpdate();
    }

    GameState SceneStartState() {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        PlayPauseUIManager.SetPauseState();
        return WavePrepState; 
    }

    GameState WavePrepState() {
        tm.WaitUpdate();
        if (UIManager.towerPurchaseReceived) {
            return TowerPurchaseSubState;
        }
        if (UIManager.playReceived) {
            PlayPauseUIManager.SetPlayState();
            return WaveStartState;
        }

        return null;

        GameState TowerPurchaseSubState() {
              tm.WaitUpdate();
            var (x, y) = mm.GetTilePosition(UIManager.mousePosition);
           
            if (tm.TileInRange(x, y) && !tm.TileOccupied(x, y) && mm.GetTile(UIManager.mousePosition) != mm.GetPathTile()) {
                tileHighlight.SetActive(true);
                tileHighlight.transform.position = new Vector3(x, y, 0);
                
                if (UIManager.clickReceived && em.TrySpend(UIManager.towerPurchased.cost)) {
                    var tower = tm.CreateTower(UIManager.towerPurchased, x, y);
                    tileHighlight.SetActive(false);
                    return WavePrepState;
                }
            }
            else {
                tileHighlight.SetActive(false);
                if (UIManager.clickReceived) {
                    return WavePrepState;
                }
            }

            if (UIManager.playReceived) {
                tileHighlight.SetActive(false);
                return WaveStartState;
            }

            return null;
        }
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
        tm.WaitUpdate();
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
