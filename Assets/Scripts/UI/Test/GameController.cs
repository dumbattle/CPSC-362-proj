using UnityEngine;

public class GameController : MonoBehaviour
{
    delegate GameState GameState();

    GameState gameState;

    public CreepBehaviour cb;

    void Start()
    {
        cb.Init();
    }

    private void Awake()
    {
        gameState = SceneStartState;
    }

    private void Update()
    {
        gameState = gameState() ?? gameState;
        TestUIManager._main.CustomUpdate(); // check TestUIManager Line 78
    }


    GameState SceneStartState()
    {
        TestUIManager.SetPlayState();
        return PlayState; // next state
    }

    GameState PlayState()
    {
        cb.GameplayUpdate();
        if (TestUIManager.pausedReceived)
        {
            //Debug.Log("error");
            TestUIManager.SetPauseState();
            return PausedState;
        }
        return null;
    }

    GameState PausedState()
    {
        if (TestUIManager.playReceived)
        {
            TestUIManager.SetPlayState();
            return PlayState;
        }
        return null;
    }
}
