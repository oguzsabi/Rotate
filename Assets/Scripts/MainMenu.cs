using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [Header("Music")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Sprite musicOnImage;
    [SerializeField] private Sprite musicOffImage;
    [Header("Sound")]
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Sprite soundOnImage;
    [SerializeField] private Sprite soundOffImage;

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
