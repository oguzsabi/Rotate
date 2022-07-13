using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField] private float rotateSpeed = 300f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private AudioSource rotateAudioManager;

    private bool _isRotating;
    private const float RotateAngle = 90f;
    private float _currentRotateAngle;
    private float _targetAngle;
    private VirtualCameraController _virtualCameraController;
    private Player _player;

    public void Start() {
        _virtualCameraController = mainCamera.GetComponentInChildren<VirtualCameraController>();
        _player = playerTransform.GetComponent<Player>();
    }

    public void Update() {
        if (!_player) _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        HandleRotation();
        RotateMap();
    }

    private void HandleRotation() {
        if (!Input.GetKeyDown(KeyCode.Q) && !Input.GetKeyDown(KeyCode.E) || _isRotating || _player.IsInAir) return;

        _isRotating = true;
        _currentRotateAngle = Input.GetKeyDown(KeyCode.Q) ? RotateAngle : -RotateAngle;
        _targetAngle = CalculateTargetAngle();

        rotateAudioManager.Play();
        _virtualCameraController.SetDamping(0f);
        _player.SetIsRotating(_isRotating);
    }

    private void RotateMap() {
        if (!_isRotating) return;


        if (Mathf.Abs(_targetAngle - transform.rotation.eulerAngles.z) < 0.0001f) {
            _isRotating = false;

            _virtualCameraController.SetDamping(0.5f);
            _player.SetIsRotating(_isRotating);

            return;
        }

        playerTransform.localRotation = Quaternion.RotateTowards(playerTransform.localRotation, Quaternion.Euler(0, 0, -_targetAngle), rotateSpeed * Time.deltaTime);
        mainCamera.localRotation = Quaternion.RotateTowards(mainCamera.localRotation, Quaternion.Euler(0, 0, -_targetAngle), rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, _targetAngle), rotateSpeed * Time.deltaTime);
    }

    private float CalculateTargetAngle() {
        var targetAngle = Mathf.Round(transform.rotation.eulerAngles.z + _currentRotateAngle) % 360f;

        return targetAngle < 0 ? 360 + targetAngle : targetAngle;
    }
}
