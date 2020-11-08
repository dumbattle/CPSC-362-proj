using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlowTowerGameController : MonoBehaviour{
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
        TestUI._main.CustomUpdate();
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

    GameState SceneStartState() {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        TestUI.SetPauseState();
        return WavePrepState; 
    }

    GameState WavePrepState() {
        WaitUpdate();
        if (TestUI.towerReceived) {
            return TowerPurchaseSubState;
        }
        if (TestUI.playReceived) {
            TestUI.SetPlayState();
            return WaveStartState;
        }

        return null;

        GameState TowerPurchaseSubState() {
              WaitUpdate();
              // replaced `var (x, y) = TestUIManager.tilePosition`
            var (x, y) = mm.GetTilePosition(TestUI.mousePosition);
           
            // added condition `mm.GetTile(TestUI.mousePosition) != mm.GetPathTile()`
            if (tm.TileInRange(x, y) && !tm.TileOccupied(x, y) && mm.GetTile(TestUI.mousePosition) != mm.GetPathTile()) {
                tileHighlight.SetActive(true);
                tileHighlight.transform.position = new Vector3(x, y, 0);
                
                if (TestUI.clickReceived && em.TrySpend(TestUI.towerSelected.cost)) {
                    var tower = tm.CreateTower(TestUI.towerSelected, x, y);
                    tileHighlight.SetActive(false);
                    return WavePrepState;
                }
            }
            else {
                tileHighlight.SetActive(false);
                if (TestUI.clickReceived) {
                    return WavePrepState;
                }
            }

            if (TestUI.playReceived) {
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
        if (TestUI.pausedReceived) {
            TestUI.SetPauseState();
            return WaveSpawnPauseState;
        }

        if (ws.spawningDone) {
            return PlayState;
        }
        return null;
    }


    GameState WaveSpawnPauseState() {
        if (TestUI.playReceived) {
            TestUI.SetPlayState();
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

        if (TestUI.pausedReceived) {
            TestUI.SetPauseState();
            return PausedState;
        }

        if (cm.creepCount == 0) {
            TestUI.SetPlayState();
            return WaveEndState;
        }
        return null;
    }

    GameState PausedState() {
        if (TestUI.playReceived) {
            TestUI.SetPlayState();
            return PlayState;
        }
        return null;
    }

    GameState WaveEndState() {
        currentWave++;
        TestUI.SetPauseState();

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
