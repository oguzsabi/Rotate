using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip deathSound;

    private float _stepSoundCurrentTime;
    private static float _defaultVolume;
    private static AudioSource _audioSource;
    
    public static bool IsPlaying => _audioSource.isPlaying;

    public void Start() {
        _audioSource = GetComponent<AudioSource>();
        _defaultVolume = _audioSource.volume;
    }

    public void PlayJumpSound() {
        _audioSource.clip = jumpSound;
        _audioSource.PlayOneShot(jumpSound);
    }

    public void PlayLandingSound() {
        _audioSource.clip = landingSound;
        _audioSource.PlayOneShot(landingSound);
    }

    public void PlayStepSound() {
        _audioSource.clip = stepSound;
        _audioSource.time = _stepSoundCurrentTime;
        _audioSource.Play();
    }

    public void StopStepSound() {
        _stepSoundCurrentTime = _audioSource.time;
        
        if (_audioSource.clip != stepSound) {
            return;
        }

        _audioSource.Stop();
    }

    public void PlayDeathSound() {
        _audioSource.clip = deathSound;
        _audioSource.PlayOneShot(deathSound);
    }

    public static void MuteAudio(bool muted) {
        _audioSource.volume = muted ? 0 : _defaultVolume;
    }
}
