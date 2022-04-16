using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    private bool _isRotating = false;
    private const float ROTATE_ANGLE = 90f;
    private float _currentRotateAngle = 0f;
    private float _targetAngle = 0f;

    public void Update() {
        this.HandleRotation();
        this.RotateMap();
    }

    private void HandleRotation() {
        if (_isRotating) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) {
            this._isRotating = true;
            this._currentRotateAngle = Input.GetKeyDown(KeyCode.Q) ? ROTATE_ANGLE : -ROTATE_ANGLE;
            this._targetAngle = (transform.rotation.eulerAngles.z + _currentRotateAngle) % 360;
            this._player.GetComponent<Player>().SetHasGravity(false);
        }
    }

    private void RotateMap() {
        if (!this._isRotating) {
            return;
        }

        if (Mathf.Abs(this._targetAngle - transform.rotation.eulerAngles.z) < 0.00001f) {
            this._isRotating = false;
            this._player.GetComponent<Player>().SetHasGravity(true);

            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, this._targetAngle), this._rotateSpeed * Time.deltaTime);
        this._player.localRotation = Quaternion.Lerp(this._player.localRotation, Quaternion.Euler(0, 0, -this._targetAngle), this._rotateSpeed * Time.deltaTime);
        this._camera.localRotation = Quaternion.Lerp(this._camera.localRotation, Quaternion.Euler(0, 0, -this._targetAngle), this._rotateSpeed * Time.deltaTime);
    }
}
