using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _groundDetectionHeight;
    private const float _runSpeedConst = 10f;
    private Rigidbody2D _rigidbody;
    private float horizontalMovement = 0;
    private bool _isInAir = false;
    private bool _isDead = false;

    public void Start() {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        if (_isDead) {
            return;
        }

        this.HandleMovement();
    }

    public void Update() {
        if (_isDead) {
            return;
        }

        this.CheckGroundStatus();
        this.Jump();
        this.GetMovement();
    }

    private void GetMovement() {
        horizontalMovement = Input.GetAxis("Horizontal") * this._speed * _runSpeedConst;
    }

    private void HandleMovement() {
        _rigidbody.velocity = new Vector2(horizontalMovement * Time.fixedDeltaTime, _rigidbody.velocity.y);

        this._animator.SetBool("isMoving", Mathf.Abs(horizontalMovement) > 0);
        this.FlipSpriteToMovementDirection(horizontalMovement);
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
            this.SetIsInAir(true);
        }
    }

    public void CheckGroundStatus() {
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
            this.SetIsInAir(false);

            return;
        }

        this.SetIsInAir(true);
    }

    public void Die() {
        this._isDead = true;
        this._animator.SetBool("isDead", true);
        this._rigidbody.velocity = Vector2.zero;
        gameObject.GetComponent<ParticleSystem>().Play();
    }

    private void SetIsInAir(bool isInAir) {
        this._isInAir = isInAir;
        this._animator.SetBool("isInAir", isInAir);
    }

    public void SetHasGravity(bool hasGravity) {
        _rigidbody.gravityScale = hasGravity ? 3f : 0f;
    }
}
