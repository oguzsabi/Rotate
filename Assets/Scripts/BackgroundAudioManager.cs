using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {
    private static float _defaultVolume;
    private static AudioSource _audioSource;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _defaultVolume = _audioSource.volume;
    }

    public static void MuteAudio(bool muted) {
        _audioSource.volume = muted ? 0 : _defaultVolume;
    }
}
