using UnityEngine;

public class WaveSpawnerTest : MonoBehaviour {
    public WaveSpawner ws;

    delegate GameState GameState();

    GameState gameState;

    int currentWave = 0;

    private void Awake() {
        gameState = SceneStartState;
    }

    private void Update() {
        gameState = gameState() ?? gameState;
    }

    GameState SceneStartState() {
        return WaveStartState;
    }

    GameState WaveStartState() {
        ws.SetWave(currentWave);
        return WaveSpawnState;
    }
    GameState WaveSpawnState() {
        ws.GameplayUpdate();

        if (ws.spawningDone) {
            return WaitState(10, WaveEndState);
        }
        return null;
    }
    GameState WaveEndState() {
        currentWave++;
        return WaveStartState;
    }

    GameState WaitState (float time, GameState next) {
        return () => {
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