using TMPro;
using UnityEngine;

public class EndMenu : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI deathCountText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    private void Start() {
        deathCountText.text = ConsistentDataManager.DeathCount.ToString();
        timeText.text = ConsistentDataManager.PassedTime;
    }
}
