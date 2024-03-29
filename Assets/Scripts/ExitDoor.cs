using System.Collections;
using UnityEngine;

public class ExitDoor : MonoBehaviour {
    [SerializeField] private float exitTime = 0.15f;

    public void OnTriggerStay2D(Collider2D other) {
        if (
            other.gameObject.CompareTag("Player") &&
            Mathf.Approximately(other.transform.localRotation.eulerAngles.z, transform.localRotation.eulerAngles.z)
        ) StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(exitTime);
        LevelLoader.LoadNextLevel();
    }
}
