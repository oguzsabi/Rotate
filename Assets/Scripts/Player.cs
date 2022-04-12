using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpForce = 5f;
    private Rigidbody2D _rigidbody;
    private bool _isInAir = false;

    public void Start() {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void Update() {
        this.Jump();
        this.HandleMovement();
        this.HandleGravity();
    }

    private void HandleMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, Vector3.zero.y, Vector3.zero.z);
        transform.position += movement * Time.deltaTime * _speed;

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

            this._isInAir = true;
        }
    }

    private void HandleGravity() {
        if (this._isInAir) {
            this._rigidbody.gravityScale = 3;

            return;
        }

        this._rigidbody.gravityScale = 1;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            this._isInAir = false;
        }
    }
}
