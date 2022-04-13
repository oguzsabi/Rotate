using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject _player;
    [SerializeField] private float _followSpeed;

    public void LateUpdate() {
        Vector3 newPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * _followSpeed);
    }
}
