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

    public static void MuteSound(bool muted) {
        ConsistentDataManager.SoundMuted = muted;
        PlayerAudioManager.MuteAudio(muted);
        RotateAudioManager.MuteAudio(muted);
    }

    public static void MuteMusic(bool muted) {
        ConsistentDataManager.MusicMuted = muted;
        BackgroundAudioManager.MuteAudio(muted);
    }
}
