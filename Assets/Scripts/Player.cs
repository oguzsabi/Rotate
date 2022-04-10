using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _jumpForce = 100f;
    private Rigidbody2D _rigidbody;
    private bool _isInAir = false;

    public void Start() {
        this._rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void Update() {
        this.Jump();
        this.HandleMovement();
    }

    private void HandleMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontal, _rigidbody.velocity.y);

        this._rigidbody.velocity = movement * _speed;
    }

    private void Jump() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !this._isInAir) {
            this._rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            this._isInAir = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision");

        if (collision.gameObject.layer == 6) {
            Debug.Log("Ground");
            this._isInAir = false;
        }
    }
}
