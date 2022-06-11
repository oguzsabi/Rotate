using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _camera;
    [SerializeField] private AudioSource _rotateAudioManager;
    private bool _isRotating = false;
    private const float ROTATE_ANGLE = 90f;
    private float _currentRotateAngle = 0f;
    private float _targetAngle = 0f;
    private VirtualCameraController _virtualCameraController;
    private Player _player;

    public void Start() {
        this._virtualCameraController = this._camera.GetComponentInChildren<VirtualCameraController>();
        this._player = this._playerTransform.GetComponent<Player>();
    }

    public void Update() {
        this.HandleRotation();
        this.RotateMap();
    }

    private void HandleRotation() {
        if (_isRotating || this._player.GetIsInAir()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            this._isRotating = true;
            this._currentRotateAngle = Input.GetKeyDown(KeyCode.Q) ? ROTATE_ANGLE : -ROTATE_ANGLE;
            this._targetAngle = this.CalculateTargetAngle();

            this._rotateAudioManager.Play();
            this._player.SetIsInAir(true);
            this._player.SetHasGravity(false);
            this._player.SetCanMove(false);
            this._player.ResetVelocity();
            this._virtualCameraController.SetDamping(0f);
        }
    }

    private void RotateMap() {
        if (!this._isRotating) {
            return;
        }


        if (Mathf.Abs(this._targetAngle - transform.rotation.eulerAngles.z) < 0.0001f) {
            this._isRotating = false;

            this._player.SetIsInAir(false);
            this._player.SetHasGravity(true);
            this._player.SetCanMove(true);
            this._virtualCameraController.SetDamping(0.5f);

            return;
        }

        this._playerTransform.localRotation = Quaternion.RotateTowards(this._playerTransform.localRotation, Quaternion.Euler(0, 0, -this._targetAngle), this._rotateSpeed * Time.deltaTime);
        this._camera.localRotation = Quaternion.RotateTowards(this._camera.localRotation, Quaternion.Euler(0, 0, -this._targetAngle), this._rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, this._targetAngle), this._rotateSpeed * Time.deltaTime);
    }

    private float CalculateTargetAngle() {
        float targetAngle = Mathf.Round((transform.rotation.eulerAngles.z + _currentRotateAngle)) % 360f;

        return targetAngle < 0 ? 360 + targetAngle : targetAngle;
    }
}
