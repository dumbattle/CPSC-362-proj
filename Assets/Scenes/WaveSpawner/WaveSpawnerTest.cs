using UnityEngine;

public class WaveSpawnerTest : MonoBehaviour {
    public WaveSpawner ws;
    public CreepManager cm;

    delegate GameState GameState();

    GameState gameState;

    int currentWave = 0;

    private void Awake() {
        gameState = SceneStartState;
    }
    private void Start() {
        ws.Init(cm);
    }
    private void Update() {
        gameState = gameState() ?? gameState;
    }

    void GameplayUpdate() {
        cm.GameplayUpdate();
    }
    GameState SceneStartState() {
        return WaveStartState;
    }

    GameState WaveStartState() {
        ws.SetWave(currentWave);
        return WaveSpawnState;
    }
    GameState WaveSpawnState() {
        ws.SpawnUpdate();
        GameplayUpdate();
        if (ws.spawningDone) {
            return WaitForCreepsToEndState();
        }
        return null;
    }
    GameState WaveEndState() {
        currentWave++;
        return WaveStartState;
    }
    GameState WaitForCreepsToEndState() {
        GameplayUpdate();
        if (cm.creepCount == 0) {
            return WaitState(10, WaveEndState);
        }
        return null;
    }
    GameState WaitState (float time, GameState next) {
        return () => {
        GameplayUpdate();
            if (time <= 0) {
                return next;
            }
            return WaitState(time - .01f, next);
        };
    }

    GameState EndState() {
        return null;
    }
}