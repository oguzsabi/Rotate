using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    private bool _gamePaused;
    private Canvas _pauseMenuCanvas;
    
    public void Awake() {
        SetupSingleton();
    }

    private void SetupSingleton() {
        var pauseMenuCount = FindObjectsOfType(GetType()).Length;

        if (pauseMenuCount < 2) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    private void Start() {
        _pauseMenuCanvas = GetComponent<Canvas>();
        _pauseMenuCanvas.enabled = _gamePaused;
    }

    private void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        HandleGamePause();
    }

    private void HandleGamePause() {
        _gamePaused = !_gamePaused;
        Time.timeScale = Convert.ToInt32(!_gamePaused);
        AudioListener.pause = _gamePaused;
        _pauseMenuCanvas.enabled = _gamePaused;
    }

    public void ContinueButtonClick() {
        HandleGamePause();
    }

    public void MainMenuButtonClick() {
        HandleGamePause();
        LevelLoader.LoadMainMenu();
    }
}
