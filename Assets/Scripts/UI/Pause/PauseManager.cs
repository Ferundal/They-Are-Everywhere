using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public bool IsPaused { get; private set; }

    public delegate void StateChangeHandler(bool isPaused);
    public event StateChangeHandler OnStateChanged;


    public void SetPause(bool isPaused)
    {
        if (isPaused == IsPaused)
            return;

        IsPaused = isPaused;
        OnStateChanged?.Invoke(isPaused);
    }
}
