using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public static void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void LoadNextLevel() {
        ConsistentDataManager.TimerStopped = true;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene((currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public static string GetSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    public static void LoadMainMenu() {
        ConsistentDataManager.ResetData();
        SceneManager.LoadScene("Main_Menu");
    }

    public static void QuitGame() {
        Application.Quit();
    }
}
