using UnityEngine;

public class AudioManager : MonoBehaviour {
    public void Awake() {
        SetupSingleton();
    }

    private void SetupSingleton() {
        var audioManagerCount = FindObjectsOfType(GetType()).Length;

        if (audioManagerCount < 2) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
