using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] public bool isInvincible;

    [SerializeField] private float speed = 35f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private Animator animator;
    [SerializeField] private float groundDetectionHeight = 0.6f;
    [SerializeField] private float fallGravityMultiplier = 1.5f;
    [SerializeField] private PhysicsMaterial2D zeroFrictionMat;
    [SerializeField] private PhysicsMaterial2D fullFrictionMat;
    [SerializeField] private ParticleSystem deathParticleSystem;
    [SerializeField] private ParticleSystem runParticleSystem;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsInAir = Animator.StringToHash("isInAir");
    private const float DefaultGravityScale = 3f;
    private const float RunSpeedMultiplier = 10f;
    private PlayerAudioManager _playerAudioManager;
    private Rigidbody2D _rigidbody;
    private float _horizontalMovement;
    private bool _isInAir;
    private bool _hasJumped;
    private bool _isDead;
    private bool _isRunning;
    private bool _canMove = true;

    public void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAudioManager = GetComponentInChildren<PlayerAudioManager>();
    }

    public void FixedUpdate() {
        if (_isDead || !_canMove) return;

        HandleMovement();
    }

    public void Update() {
        if (_isDead || !_canMove) return;

        CheckGroundStatus();
        Jump();
        GetMovement();
        HandleFriction();
        SetFallingGravity();
        PlaySfx();
    }

    private void GetMovement() {
        _horizontalMovement = Input.GetAxis("Horizontal") * speed * RunSpeedMultiplier;
        _isRunning = Mathf.Abs(_horizontalMovement) > 0;
    }

    private void HandleMovement() {
        animator.SetBool(IsMoving, Mathf.Abs(_horizontalMovement) > 0);
        FlipSpriteToMovementDirection(_horizontalMovement);
        
        if (_isRunning && !_isInAir && !_hasJumped) {
            _rigidbody.velocity = Vector2.ClampMagnitude(new Vector2(_horizontalMovement * Time.fixedDeltaTime, _rigidbody.velocity.y), 7f);
            runParticleSystem.Play();
        }
        else _rigidbody.velocity = new Vector2(_horizontalMovement * Time.fixedDeltaTime, _rigidbody.velocity.y);

        if (_isInAir || !_isRunning) runParticleSystem.Stop();
    }

    private void HandleFriction() {
        _rigidbody.sharedMaterial = _isRunning ? zeroFrictionMat : fullFrictionMat;
    }

    private void FlipSpriteToMovementDirection(float horizontalMovement) {
        var playerTransform = transform;
        playerTransform.localScale = horizontalMovement switch
        {
            > 0 => new Vector3(1, 1, 1),
            < 0 => new Vector3(-1, 1, 1),
            _ => playerTransform.localScale
        };
    }

    private void Jump() {
        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.UpArrow) || _isInAir) return;

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _hasJumped = true;

        _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        _playerAudioManager.PlayJumpSound();
    }

    private void CheckGroundStatus() {
        var playerTransform = transform;
        var direction = playerTransform.localScale.x;
        var leftJumpRay = Physics2D.Raycast(
            playerTransform.position + new Vector3(direction * -0.1f, 0, 0),
            Vector2.down,
            groundDetectionHeight,
            1 << LayerMask.NameToLayer("Ground")
        );
        var rightJumpRay = Physics2D.Raycast(
            transform.position + new Vector3(direction * 0.25f, 0, 0),
            Vector2.down,
            groundDetectionHeight,
            1 << LayerMask.NameToLayer("Ground")
        );

        if (leftJumpRay.collider || rightJumpRay.collider) {
            if (_isInAir) BeforeLandingOperations();

            SetIsInAir(false);

            return;
        }

        SetIsInAir(true);
    }

    private void SetFallingGravity() {
        if (_rigidbody.velocity.y < 0) {
            _rigidbody.gravityScale = DefaultGravityScale * fallGravityMultiplier;

            return;
        }

        _rigidbody.gravityScale = DefaultGravityScale;
    }

    private void PlaySfx() {
        switch (_isRunning) {
            case true when !_playerAudioManager.IsPlaying() && !_isInAir:
                _playerAudioManager.PlayStepSound();
                break;
            case false or true when _playerAudioManager.IsPlaying():
                _playerAudioManager.StopStepSound();
                break;
        }
    }

    public void Die() {
        _isDead = true;

        SetHasGravity(false);
        _playerAudioManager.PlayDeathSound();
        animator.SetBool(IsDead, true);
        ResetVelocity();
        deathParticleSystem.Play();
    }

    private void BeforeLandingOperations() {
        _playerAudioManager.PlayLandingSound();
        _hasJumped = false;
    }

    public void SetIsInAir(bool isInAir) {
        _isInAir = isInAir;
        animator.SetBool(IsInAir, isInAir);
    }

    public void SetHasGravity(bool hasGravity, float gravityMultiplier = DefaultGravityScale) {
        _rigidbody.gravityScale = hasGravity ? gravityMultiplier : 0f;
    }

    public void ResetVelocity() {
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetCanMove(bool canMove) {
        _canMove = canMove;
    }

    public bool GetIsInAir() {
        return _isInAir;
    }
}
