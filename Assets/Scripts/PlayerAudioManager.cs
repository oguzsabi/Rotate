using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _landingSound;
    [SerializeField] private AudioClip _deathSound;
    private float _stepSoundCurrentTime = 0f;
    private AudioSource _audioSource;

    public void Start() {
        this._audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayJumpSound() {
        this._audioSource.clip = this._jumpSound;
        this._audioSource.PlayOneShot(this._jumpSound);
    }

    public void PlayLandingSound() {
        this._audioSource.clip = this._landingSound;
        this._audioSource.PlayOneShot(this._landingSound);
    }

    public void PlayStepSound() {
        this._audioSource.clip = this._stepSound;
        this._audioSource.time = this._stepSoundCurrentTime;
        this._audioSource.Play();
    }

    public void StopStepSound() {
        this._stepSoundCurrentTime = this._audioSource.time;
        
        if (this._audioSource.clip != this._stepSound) {
            return;
        }

        this._audioSource.Stop();
    }

    public void PlayDeathSound() {
        this._audioSource.clip = this._deathSound;
        this._audioSource.PlayOneShot(this._deathSound);
    }

    public bool IsPlaying() {
        return this._audioSource.isPlaying;
    }
}
