using UnityEngine;

public class PlayerJumpManager : MonoBehaviour {
    [SerializeField] private float timeForNextLandingEffect = 0.04f;
    private Player _player;
    private BoxCollider2D _groundCheckCollider;
    private float _timeSinceLastLandingEffect;

    private void Start() {
        _player = transform.parent.GetComponent<Player>();
        _groundCheckCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        _timeSinceLastLandingEffect += Time.deltaTime;

        CheckGroundStatus();
    }

    private void CheckGroundStatus() {
        if (_groundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        _player.SetIsInAir(true);
    }
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.isTrigger || !_groundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (_player.IsInAir && _timeSinceLastLandingEffect > timeForNextLandingEffect) {
            _timeSinceLastLandingEffect = 0;

            _player.BeforeLandingOperations();
        }

        _player.SetIsInAir(false);
    }
}
