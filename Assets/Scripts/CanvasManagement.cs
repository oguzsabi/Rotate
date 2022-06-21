using UnityEngine;
using TMPro;

public class CanvasManagement : MonoBehaviour {
    private static TextMeshProUGUI _deathCountText;

    public void Start() {
        _deathCountText = GameObject.FindGameObjectWithTag("DeathCount").GetComponent<TextMeshProUGUI>();
        _deathCountText.text = Death.DeathCount.ToString();
    }

    public static void UpdateDeathCount(int count) {
        _deathCountText.text = count.ToString();
    }
}
