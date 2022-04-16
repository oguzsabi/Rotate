using UnityEngine;
using Cinemachine;

public class PlayerFinder : MonoBehaviour {
    // Start is called before the first frame update
    public void Start() {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
