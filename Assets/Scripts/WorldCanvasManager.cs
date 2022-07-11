using System;
using TMPro;
using UnityEngine;

public class WorldCanvasManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _levelText;

    public void Start() {
        _levelText.text = $"Level {this.ExtractLevelNumber()}";
    }

    private string ExtractLevelNumber() {
        int.TryParse(LevelLoader.GetSceneName().Split('_')[1] ?? "0", out int levelNumber);

        return levelNumber < 10 ? $"0{levelNumber}" : levelNumber.ToString();
    }
}
