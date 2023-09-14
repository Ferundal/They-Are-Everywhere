using UnityEngine;

public class StateManager
{
    private static StateManager _instance;

    public static StateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StateManager();
            }
            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void StateChangeHandler(GameState newGameState);
    public event StateChangeHandler OnStateChanged;

    private StateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;

        Debug.Log("happened");
        CurrentGameState = newGameState;
        OnStateChanged?.Invoke(newGameState);
    }
}
