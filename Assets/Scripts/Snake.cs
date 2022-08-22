using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    [SerializeField] private CellGrid _cellGrid;
    [SerializeField] private SnakeTail _snakeTailPrefab;

    private List<SnakeTail> tails;

    private void Start()
    {
        Vector2Int gridsize = _cellGrid.GetGridSize();
        Vector3Int spawnPosition = new Vector3Int(gridsize.x / 2, gridsize.y / 2);

        for (int i = 0; i < 2; i++)
        {
            tails.Add(Instantiate(_snakeTailPrefab, spawnPosition, Quaternion.identity));
            _cellGrid.TrySetInCellGrid(tails[i], new Vector2Int(spawnPosition.x, spawnPosition.y));
            spawnPosition.x--;
        }
        _cellGrid.FoodEaten += EatFood;
    }

    private void EatFood(CellItem food)
    {

    }

    private void OnDisable()
    {
        _cellGrid.FoodEaten -= EatFood;
    }
}