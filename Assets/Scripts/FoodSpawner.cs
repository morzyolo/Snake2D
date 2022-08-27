using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private CellGrid _cellGrid;
    [SerializeField] private Cherry _cherryPrefab;

    private Cherry _currentCherry;

    private void Start()
    {
        CellGrid.CherryEatenAction += SpawnCherry;

        _currentCherry = Instantiate(_cherryPrefab, this.transform);
        SpawnCherry();
    }

    private void SpawnCherry()
    {
        Vector2Int spawnPosition;
        Vector2Int gridSize = _cellGrid.GetGridSize();

        do
        {
            spawnPosition = new Vector2Int(Random.Range(0 , gridSize.x), Random.Range(0, gridSize.y));
        } while (!_cellGrid.TrySetInCellGrid(_currentCherry, spawnPosition));

        _currentCherry.transform.localPosition = new Vector3(spawnPosition.x, spawnPosition.y);
    }

    private void OnDisable()
    {
        CellGrid.CherryEatenAction -= SpawnCherry;
    }
}