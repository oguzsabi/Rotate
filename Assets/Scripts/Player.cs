using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] public bool isInvincible;

    [SerializeField] private float speed = 35f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private Animator animator;
    [SerializeField] private float fallGravityMultiplier = 1.5f;
    [SerializeField] private PhysicsMaterial2D zeroFrictionMat;
    [SerializeField] private PhysicsMaterial2D fullFrictionMat;
    [SerializeField] private ParticleSystem deathParticleSystem;
    [SerializeField] private ParticleSystem runParticleSystem;
    [SerializeField] private ParticleSystem jumpParticleSystem;
    [SerializeField] private ParticleSystem landParticleSystem;

    public bool IsInAir { get; private set; }

    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");
    private static readonly int IsDeadHash = Animator.StringToHash("isDead");
    private static readonly int IsInAirHash = Animator.StringToHash("isInAir");
    private const float DefaultGravityScale = 3f;
    private const float RunSpeedMultiplier = 10f;
    private PlayerAudioManager _playerAudioManager;
    private Rigidbody2D _rigidbody;
    private float _horizontalMovement;
    private bool _hasJumped;
    private bool _isDead;
    private bool _isRunning;
    private bool _canMove = true;
    private BoxCollider2D _groundCheckCollider;

    public void Start() {
        ConsistentDataManager.TimerStopped = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAudioManager = GetComponentInChildren<PlayerAudioManager>();
        _groundCheckCollider = GetComponent<BoxCollider2D>();
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
        animator.SetBool(IsMovingHash, Mathf.Abs(_horizontalMovement) > 0);
        FlipSpriteToMovementDirection(_horizontalMovement);

        if (_isRunning && !IsInAir && !_hasJumped) {
            _rigidbody.velocity = Vector2.ClampMagnitude(new Vector2(_horizontalMovement * Time.fixedDeltaTime, _rigidbody.velocity.y), 7f);
            runParticleSystem.Play();
        } else {
            _rigidbody.velocity = new Vector2(_horizontalMovement * Time.fixedDeltaTime, _rigidbody.velocity.y);
        }

        if (IsInAir || !_isRunning) runParticleSystem.Stop();
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
        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.W) || IsInAir) return;

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _hasJumped = true;

        _rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumpParticleSystem.Play();
        _playerAudioManager.PlayJumpSound();
    }

    private void CheckGroundStatus() {
        if (_groundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if (IsInAir) BeforeLandingOperations();

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
        if (_isRunning && !PlayerAudioManager.IsPlaying && !IsInAir) {
            _playerAudioManager.PlayStepSound();
        } else if ((IsInAir || !_isRunning) && PlayerAudioManager.IsPlaying) {
            _playerAudioManager.StopStepSound();
        }
    }

    public void Die() {
        _isDead = true;

        SetHasGravity(false);
        _playerAudioManager.PlayDeathSound();
        animator.SetBool(IsDeadHash, true);
        ResetVelocity();
        deathParticleSystem.Play();
    }

    private void BeforeLandingOperations() {
        landParticleSystem.Play();
        _playerAudioManager.PlayLandingSound();
        _hasJumped = false;
    }

    private void SetIsInAir(bool isInAir) {
        IsInAir = isInAir;
        animator.SetBool(IsInAirHash, isInAir);
    }

    private void SetHasGravity(bool hasGravity) {
        _rigidbody.gravityScale = hasGravity ? DefaultGravityScale : 0f;
    }

    private void ResetVelocity() {
        _rigidbody.velocity = Vector2.zero;
    }

    public void SetIsRotating(bool isRotating) {
        SetIsInAir(isRotating);
        SetHasGravity(!isRotating);
        _canMove = !isRotating;

        if (!isRotating) return;

        ResetVelocity();
        ResetAndClearParticles();
    }

    private void ResetAndClearParticles() {
        runParticleSystem.Stop();
        runParticleSystem.Clear();
        jumpParticleSystem.Stop();
        jumpParticleSystem.Clear();
        landParticleSystem.Stop();
        landParticleSystem.Clear();
    }
}
