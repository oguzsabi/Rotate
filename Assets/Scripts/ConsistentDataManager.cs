using UnityEngine;

public class ConsistentDataManager : MonoBehaviour {
    public static int DeathCount { get; private set; }
    public static string PassedTime { get; private set; }
    public static bool TimerStopped { get; set; } = true;
    public static bool SoundOn { get; set; }

    private float _passedTime;

    public void Awake() {
        SetupSingleton();
    }

    private void SetupSingleton() {
        var audioManagerCount = FindObjectsOfType(GetType()).Length;

        if (audioManagerCount < 2) {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (TimerStopped) return;

        ConvertSecondsToStringTime();
    }

    private void ConvertSecondsToStringTime() {
        _passedTime += Time.deltaTime;
        var passedTime = "";

        var hours = Mathf.Floor(_passedTime / 3600);
        var minutes = Mathf.Floor(_passedTime / 60) % 60;
        var seconds = Mathf.Floor(_passedTime) % 60;
        var milliseconds = Mathf.Round(_passedTime * 100) % 100;

        passedTime += hours != 0 ? $"{hours}:".PadLeft(3, '0') : "";
        passedTime += minutes != 0 ? $"{minutes}:".PadLeft(3, '0') : "";
        passedTime += seconds != 0 ? $"{seconds}.".PadLeft(3, '0') : "";
        passedTime += milliseconds != 0 ? $"{milliseconds}" : "";
        PassedTime = passedTime;
    }

    public static void IncrementDeathCount() {
        DeathCount++;
    }
}
