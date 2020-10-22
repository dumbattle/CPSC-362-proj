using UnityEngine;

public class Iteration2GameController : MonoBehaviour{
    delegate GameState GameState();

    public WaveSpawner ws;
    public CreepManager cm;
    public TowerManager tm;

    GameState gameState;
    int currentWave = 0;


    void Start() {
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
        ws.Init(cm);
        TestUIManager.SetPauseState();
        return WavePrepState; // next state
    }

    GameState WavePrepState() {
        PurchaseTower();
        if (TestUIManager.playReceived) {
            return WaveStartState;
        }
        return null;
    }

    GameState WaveStartState() {
        ws.SetWave(currentWave);
        return WaveSpawnState;
    }



    GameState WaveSpawnState() {
        ws.SpawnUpdate();
        GameplayUpdate();

        if (TestUIManager.pausedReceived) {
            //Debug.Log("error");
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
            //Debug.Log("error");
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
