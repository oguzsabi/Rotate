using UnityEngine;

public class KeyTutorial : MonoBehaviour {
    [SerializeField] private SpriteRenderer _upArrow;
    [SerializeField] private SpriteRenderer _leftArrow;
    [SerializeField] private SpriteRenderer _rightArrow;
    [SerializeField] private SpriteRenderer _eKey;
    [SerializeField] private SpriteRenderer _qKey;
    private Color RED;

    public void Start() {
        this.RED = new Color32(187, 59, 74, 255);
    }

    public void Update() {
        this.HandleKeyColors();
    }

    private void HandleKeyColors() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            this._upArrow.color = this.RED;
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            this._upArrow.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            this._leftArrow.color = this.RED;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            this._leftArrow.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            this._rightArrow.color = this.RED;
        } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            this._rightArrow.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            this._eKey.color = this.RED;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            this._eKey.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            this._qKey.color = this.RED;
        } else if (Input.GetKeyUp(KeyCode.Q)) {
            this._qKey.color = Color.white;
        }
    }
}
