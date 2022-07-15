using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip landingSound;
    [SerializeField] private AudioClip deathSound;

    private float _stepSoundCurrentTime;
    private static AudioSource _audioSource;
    
    public static bool IsPlaying => _audioSource.isPlaying;

    public void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.mute = ConsistentDataManager.SoundMuted;
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
        if (!_audioSource) return;
        
        _audioSource.mute = muted;
    }
}
