using System;
using UnityEngine;

public class ParticleSystemRotation : MonoBehaviour {
    private Transform _playerTransform;

    private void Start() {
        _playerTransform = transform.parent;
    }

    public void FixedUpdate() {
        var particleSystemTransform = transform;
        var localScale = particleSystemTransform.localScale;
        particleSystemTransform.localScale = new Vector3(_playerTransform.localScale.x, localScale.y, localScale.z);
    }
}
