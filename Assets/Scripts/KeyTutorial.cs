using UnityEngine;

public class KeyTutorial : MonoBehaviour {
    [SerializeField] private SpriteRenderer upArrow;
    [SerializeField] private SpriteRenderer leftArrow;
    [SerializeField] private SpriteRenderer rightArrow;
    [SerializeField] private SpriteRenderer qKey;
    [SerializeField] private SpriteRenderer eKey;
    private Color _red;

    public void Start() {
        _red = new Color32(187, 59, 74, 255);
    }

    public void Update() {
        HandleKeyColors();
    }

    private void HandleKeyColors() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            upArrow.color = _red;
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            upArrow.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            leftArrow.color = _red;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            leftArrow.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rightArrow.color = _red;
        } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            rightArrow.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            eKey.color = _red;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            eKey.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            qKey.color = _red;
        } else if (Input.GetKeyUp(KeyCode.Q)) {
            qKey.color = Color.white;
        }
    }
}
