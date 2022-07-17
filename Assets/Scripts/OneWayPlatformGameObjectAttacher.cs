using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayPlatformGameObjectAttacher : MonoBehaviour {
    [SerializeField] private GameObject tileGameObject;

    private const float TileGameObjectOffset = 0.5f;
    private Tilemap _tilemap;

    private void Start() {
        _tilemap = GetComponent<Tilemap>();

        foreach (var position in _tilemap.cellBounds.allPositionsWithin) {
            var tile = _tilemap.GetTile<Tile>(position);
            var tileEulerAngles = _tilemap.GetTransformMatrix(position).rotation.eulerAngles;

            if (!tile) continue;

            var tileGameObjectInstance = Instantiate(
                tileGameObject,
                new Vector3(position.x + TileGameObjectOffset, position.y + TileGameObjectOffset, position.z),
                Quaternion.Euler(tileEulerAngles.x, tileEulerAngles.y, tileEulerAngles.z)
            );
            tileGameObjectInstance.transform.parent = _tilemap.transform;
            tile.gameObject = tileGameObjectInstance;
        }
    }
}
