using UnityEngine;
using Cinemachine;

public class VirtualCameraController : MonoBehaviour {
    private CinemachineFramingTransposer _virtualCameraFramingTransposer;

    public void Start() {
        this._virtualCameraFramingTransposer = this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetDamping(float damping) {
        _virtualCameraFramingTransposer.m_XDamping = damping;
        _virtualCameraFramingTransposer.m_YDamping = damping;
    }
}
