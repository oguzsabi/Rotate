using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {
    [SerializeField] private float restartTime = 1f;

    public void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;

        ConsistentDataManager.IncrementDeathCount();

        var player = other.gameObject.GetComponent<Player>();

        if (player.isInvincible) return;

        other.gameObject.GetComponent<Player>().Die();
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel() {
        yield return new WaitForSeconds(restartTime);

        LevelLoader.RestartLevel();
    }
}
