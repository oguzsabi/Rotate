using UnityEngine;

public class ParticleSystemRotation : MonoBehaviour {
    private Transform _playerTransform;
    
    private void Start() {
        _playerTransform = transform.parent;
    }

    public void Update() {
        var particleSystemTransform = transform;
        var localScale = particleSystemTransform.localScale;
        localScale = new Vector3(_playerTransform.localScale.x, localScale.y, localScale.z);
        particleSystemTransform.localScale = localScale;
    }
}
