using System;
using System.Collections;
using UnityEngine;

public class ExitDoor : MonoBehaviour {
    [SerializeField] private float exitTime = 0.15f;

    public void OnTriggerStay2D(Collider2D other) {
        if (
            other.gameObject.CompareTag("Player") &&
            Math.Abs(other.transform.localRotation.eulerAngles.z - transform.localRotation.eulerAngles.z) < 0.1
        ) StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(exitTime);
        LevelLoader.LoadNextLevel();
    }
}
