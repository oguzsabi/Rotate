using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {
    private static AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.mute = ConsistentDataManager.MusicMuted;
    }

    public static void MuteAudio(bool muted) {
        if (!_audioSource) return;
        
        _audioSource.mute = muted;
    }
}
