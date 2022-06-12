using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _groundDetectionHeight;
    [SerializeField] private float _fallGravityMultiplier;
    [SerializeField] private Camera _camera;
    private const float DEFAULT_GRAVITY_SCALE = 3f;
    private const float RUN_SPEED_MULTIPLIER = 10f;
    private Rigidbody2D _rigidbody;
    private float _horizontalMovement = 0;
    private bool _isInAir = false;
    private bool _isDead = false;
    private bool _canMove = true;
    private bool _isRunning = false;
    private PlayerAudioManager _playerAudioManager;


    public void Start() {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
        this._playerAudioManager = this.GetComponentInChildren<PlayerAudioManager>();
    }

    public void FixedUpdate() {
        if (this._isDead || !this._canMove) {
            return;
        }

        this.HandleMovement();
    }

    public void Update() {
        if (this._isDead || !this._canMove) {
            return;
        }

        this.CheckGroundStatus();
        this.Jump();
        this.GetMovement();
        this.SetFallingGravity();
        this.PlaySFX();
    }

    private void GetMovement() {
        this._horizontalMovement = Input.GetAxis("Horizontal") * this._speed * RUN_SPEED_MULTIPLIER;
        this._isRunning = Mathf.Abs(this._horizontalMovement) > 0;
    }

    private void HandleMovement() {
        this._rigidbody.velocity = new Vector2(this._horizontalMovement * Time.fixedDeltaTime, this._rigidbody.velocity.y);

        this._animator.SetBool("isMoving", Mathf.Abs(_horizontalMovement) > 0);
        this.FlipSpriteToMovementDirection(_horizontalMovement);
    }

    private void FlipSpriteToMovementDirection(float horizontalMovement) {
        if (horizontalMovement > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (horizontalMovement < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Jump() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !this._isInAir) {
            this._rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            this._playerAudioManager.PlayJumpSound();
        }
    }

    private void CheckGroundStatus() {
        float direction = transform.localScale.x;
        RaycastHit2D leftJumpRay = Physics2D.Raycast(
            transform.position + new Vector3(direction * -0.1f, 0, 0),
            Vector2.down,
            this._groundDetectionHeight,
            1 << LayerMask.NameToLayer("Ground")
        );
        RaycastHit2D rightJumpRay = Physics2D.Raycast(
            transform.position + new Vector3(direction * 0.25f, 0, 0),
            Vector2.down,
            this._groundDetectionHeight,
            1 << LayerMask.NameToLayer("Ground")
        );

        if (leftJumpRay.collider || rightJumpRay.collider) {
            if (this._isInAir) {
                this._playerAudioManager.PlayLandingSound();
            }

            this.SetIsInAir(false);

            return;
        }

        this.SetIsInAir(true);
    }

    private void SetFallingGravity() {
        if (this._rigidbody.velocity.y < 0) {
            this._rigidbody.gravityScale = DEFAULT_GRAVITY_SCALE * this._fallGravityMultiplier;

            return;
        }

        this._rigidbody.gravityScale = DEFAULT_GRAVITY_SCALE;
    }

    private void PlaySFX() {
        if (this._isRunning && !this._playerAudioManager.IsPlaying() && !this._isInAir) {
            this._playerAudioManager.PlayStepSound();
        } else if ((!this._isRunning || this._isInAir) && this._playerAudioManager.IsPlaying()) {
            this._playerAudioManager.StopStepSound();
        }
    }

    public void Die() {
        this._isDead = true;

        this.SetHasGravity(false);
        this._playerAudioManager.PlayDeathSound();
        this._animator.SetBool("isDead", true);
        this.ResetVelocity();
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    public void SetIsInAir(bool isInAir) {
        this._isInAir = isInAir;
        this._animator.SetBool("isInAir", isInAir);
    }

    public void SetHasGravity(bool hasGravity, float gravityMultiplier = DEFAULT_GRAVITY_SCALE) {
        _rigidbody.gravityScale = hasGravity ? gravityMultiplier : 0f;
    }

    public void ResetVelocity() {
        this._rigidbody.velocity = Vector2.zero;
    }

    public void SetCanMove(bool canMove) {
        this._canMove = canMove;
    }

    public bool GetIsInAir() {
        return this._isInAir;
    }
}