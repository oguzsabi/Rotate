using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {
    [SerializeField] public float _restartTime = 1f;

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().Die();
            StartCoroutine(RestartLevel());
        }
    }

    public IEnumerator RestartLevel() {
        yield return new WaitForSeconds(this._restartTime);

        LevelLoader.RestartLevel();
    }
}
