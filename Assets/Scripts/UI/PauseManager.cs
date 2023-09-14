using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Pause()
    {
        //if (!IsPaused) IsPaused = true;
        GameState currentGameState = StateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay ? GameState.Paused : GameState.Gameplay;
        StateManager.Instance.SetState(newGameState);
        Debug.Log(newGameState.ToString());
    }
}
