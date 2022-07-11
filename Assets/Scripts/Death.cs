using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {
    [SerializeField] private float _restartTime;

    private static int _deathCount = 0;
    public static int DeathCount { get { return _deathCount; } }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            CameraCanvasManager.UpdateDeathCount(++_deathCount);

            Player player = other.gameObject.GetComponent<Player>();

            if (!player.isInvincible) {
                other.gameObject.GetComponent<Player>().Die();
                StartCoroutine(RestartLevel());
            }
        }
    }

    public IEnumerator RestartLevel() {
        yield return new WaitForSeconds(this._restartTime);

        LevelLoader.RestartLevel();
    }
}
