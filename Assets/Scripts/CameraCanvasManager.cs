using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraCanvasManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI deathCountText;
    [SerializeField] private TextMeshProUGUI timeText;
    [Header("Music")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Sprite musicOnImage;
    [SerializeField] private Sprite musicOffImage;
    [Header("Sound")]
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Sprite soundOnImage;
    [SerializeField] private Sprite soundOffImage;

    public void Start() {
        UpdateUIText();

        if (ConsistentDataManager.MusicMuted) musicToggle.isOn = false;
        if (ConsistentDataManager.SoundMuted) soundToggle.isOn = false;
    }

    public void Update() {
        UpdateUIText();
    }

    private void UpdateUIText() {
        deathCountText.text = ConsistentDataManager.DeathCount.ToString();
        timeText.text = ConsistentDataManager.PassedTime;
    }

    public void MusicToggleChanged() {
        var muted = !musicToggle.isOn;
        AudioManager.MuteMusic(muted);

        if (muted) {
            musicToggle.GetComponent<Image>().sprite = musicOffImage;
            
            return;
        }
        
        musicToggle.GetComponent<Image>().sprite = musicOnImage;
    }

    public void SoundToggleChanged() {
        var muted = !soundToggle.isOn;
        AudioManager.MuteSound(muted);

        if (muted) {
            soundToggle.GetComponent<Image>().sprite = soundOffImage;
            
            return;
        }
        
        soundToggle.GetComponent<Image>().sprite = soundOnImage;
    }
}
