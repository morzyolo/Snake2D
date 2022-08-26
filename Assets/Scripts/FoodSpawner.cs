using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private CellGrid _cellGrid;
    [SerializeField] private Cherry _cherryPrefab;

    private Cherry _currentFood;

    private void Start()
    {
        CellGrid.FoodEatenAction += SpawnNewFood;

        _currentFood = Instantiate(_cherryPrefab, this.transform);

        SpawnNewFood(null);
    }

    private void SpawnNewFood(CellItem cellItem)
    {
        Vector2Int spawnPosition;
        Vector2Int gridSize = _cellGrid.GetGridSize();

        do
        {
            spawnPosition = new Vector2Int(Random.Range(gridSize.x / 4, gridSize.x * 3/4), Random.Range(gridSize.y / 4, gridSize.y * 3/4));
        } while (!_cellGrid.TrySetInCellGrid(_currentFood, spawnPosition));


        _currentFood.transform.localPosition = new Vector3(spawnPosition.x, spawnPosition.y);
    }

    private void OnDisable()
    {
        CellGrid.FoodEatenAction -= SpawnNewFood;
    }
}