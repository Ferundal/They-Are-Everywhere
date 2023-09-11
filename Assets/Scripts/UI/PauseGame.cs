using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static PauseGame instance;
    [HideInInspector] public bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    private float _previousTimeScale = 1f;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
            pauseMenu.gameObject.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = _previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
            pauseMenu.gameObject.SetActive(false);
        }
    }
}
