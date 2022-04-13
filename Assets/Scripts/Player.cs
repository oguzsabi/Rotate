using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _groundDetectionHeight;
    private Rigidbody2D _rigidbody;
    private bool _isInAir = false;

    public void Start() {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void Update() {
        this.CheckGroundStatus();
        this.Jump();
        this.HandleMovement();
    }

    private void HandleMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, Vector3.zero.y, Vector3.zero.z);
        transform.position += movement * Time.deltaTime * _speed;

        this._animator.SetBool("isMoving", Mathf.Abs(horizontal) > 0);
        this.FlipSpriteToMovementDirection(horizontal);
    }

    private void FlipSpriteToMovementDirection(float horizontal) {
        if (horizontal > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (horizontal < 0) {
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
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            this._groundDetectionHeight,
            1 << LayerMask.NameToLayer("Ground")
        );

        if (hit.collider) {
            this.SetIsInAir(false);

            return;
        }

        this.SetIsInAir(true);
    }

    private void SetIsInAir(bool isInAir) {
        this._isInAir = isInAir;
        this._animator.SetBool("isInAir", isInAir);
    }
}
