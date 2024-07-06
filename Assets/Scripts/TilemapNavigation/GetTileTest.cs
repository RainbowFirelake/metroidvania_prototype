using UnityEngine;
using UnityEngine.Tilemaps;

public class GetTileTest : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemap;
    [SerializeField]
    private GameObject _g;

    private void OnValidate()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(_g, vector, Quaternion.identity);
            Debug.Log(Input.mousePosition);
            Debug.Log(vector);
            var transformedVector = _tilemap.WorldToCell(vector);
            var tile = _tilemap.GetTile(transformedVector);

            if (tile != null)
            {
                Debug.Log($"� ����� {transformedVector} ������������ ����.");
            }
            else
            {
                Debug.Log($"� ����� {transformedVector} ����������� ����");
            }

            Debug.Log($"Origin: {_tilemap.origin}");
        }
    }

}
