using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {
    public void Awake() {
        SetupSingleton();
    }

    private void SetupSingleton() {
        var musicPlayerCount = FindObjectsOfType(GetType()).Length;

        if (musicPlayerCount < 2) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
