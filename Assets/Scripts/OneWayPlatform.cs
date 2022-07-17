using UnityEngine;

public class OneWayPlatform : MonoBehaviour {
    [SerializeField] private Transform player;

    private const float ColliderHeightOffset = 0.155f;
    private GameObject _activator;
    private BoxCollider2D _collider;

    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
        _activator = GameObject.Find("OneWayPlatformActivator");
    }

    private void Update() {
        var platformPosition = transform.position;

        if (
            _activator.transform.position.y > platformPosition.y + Mathf.Sign(platformPosition.y) * ColliderHeightOffset &&
            Mathf.Approximately(player.rotation.eulerAngles.z, transform.rotation.eulerAngles.z)
        ) {
            _collider.isTrigger = false;

            return;
        } 
        
        _collider.isTrigger = true;
    }
}
