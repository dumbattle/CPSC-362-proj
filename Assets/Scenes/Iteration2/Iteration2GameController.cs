using UnityEngine;

public class Iteration2GameController : MonoBehaviour{
    delegate GameState GameState();

    public WaveSpawner ws;
    public CreepManager cm;
    public TowerManager tm;
    public SimpleEconomyManager em;
    public GameObject tileHighlight;

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
        TestUIManager._main.CustomUpdate(); // check TestUIManager Line 78
    }


    void GameplayUpdate() {
        cm.GameplayUpdate();
    }

    void PurchaseTower() {
        var (x, y) = TestUIManager.tilePosition;

        if (TestUIManager.clickReceived && TestUIManager.towerReceived && tm.TileInRange(x,y) && !tm.TileOccupied(x, y)) {
            var tower = tm.CreateTower(TestUIManager.towerSelected, x, y);
        }
    }



    GameState SceneStartState() {
        TestUIManager.SetPauseState();
        return WavePrepState; 
    }

    GameState WavePrepState() {
        if (TestUIManager.towerReceived) {
            return TowerPurchaseSubState;
        }
        if (TestUIManager.playReceived) {
            TestUIManager.SetPlayState();
            return WaveStartState;
        }

        return null;

        GameState TowerPurchaseSubState() {
            var (x, y) = TestUIManager.tilePosition;

            if (tm.TileInRange(x, y) && !tm.TileOccupied(x, y)) {
                tileHighlight.SetActive(true);
                tileHighlight.transform.position = new Vector3(x, y, 0);
                if (TestUIManager.clickReceived && em.TrySpend(TestUIManager.towerSelected.cost)) {
                    var tower = tm.CreateTower(TestUIManager.towerSelected, x, y);
                    tileHighlight.SetActive(false);
                    return WavePrepState;
                }
            }
            else {
                tileHighlight.SetActive(false);
                if (TestUIManager.clickReceived) {
                    return WavePrepState;
                }
            }

            if (TestUIManager.playReceived) {
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

        if (TestUIManager.pausedReceived) {
            TestUIManager.SetPauseState();
            return WaveSpawnPauseState;
        }

        if (ws.spawningDone) {
            return PlayState;
        }
        return null;
    }


    GameState WaveSpawnPauseState() {
        if (TestUIManager.playReceived) {
            TestUIManager.SetPlayState();
            return WaveSpawnState;
        }

        return null;
    }





    GameState PlayState() {
        GameplayUpdate();

        if (TestUIManager.pausedReceived) {
            TestUIManager.SetPauseState();
            return PausedState;
        }

        if (cm.creepCount == 0) {
            TestUIManager.SetPlayState();
            return WaveEndState;
        }
        return null;
    }

    GameState PausedState() {
        if (TestUIManager.playReceived) {
            TestUIManager.SetPlayState();
            return PlayState;
        }
        return null;
    }

    GameState WaveEndState() {
        currentWave++;
        TestUIManager.SetPauseState();

        return WavePrepState;
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
