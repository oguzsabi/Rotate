using UnityEngine;

public class OneWayPlatform : MonoBehaviour {
    private BoxCollider2D _collider;

    private void Start() {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (!col.transform.CompareTag("Player")) return;

        _collider.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D col) {
        if (
            !col.name.Equals("OneWayPlatformActivator") ||
            !Mathf.Approximately(col.transform.parent.localRotation.eulerAngles.z, transform.localRotation.eulerAngles.z)
        ) return;

        _collider.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (!col.name.Equals("OneWayPlatformActivator")) return;

        _collider.isTrigger = true;
    }
}
