using UnityEngine;

public class ArrowKeyTutorial : MonoBehaviour {
    [SerializeField] private SpriteRenderer _upArrow;
    [SerializeField] private SpriteRenderer _leftArrow;
    [SerializeField] private SpriteRenderer _rightArrow;
    public Color RED;

    public void Start() {
        this.RED = new Color32(187, 59, 74, 255);
    }

    public void Update() {
        this.HandleArrowKeyColors();
    }

    private void HandleArrowKeyColors() {
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
    }
}
