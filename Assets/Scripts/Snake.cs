using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    [SerializeField] private CellGrid _cellGrid;
    [SerializeField] private SnakeTail _snakeTailPrefab;

    private List<SnakeTail> _tails;

    private void Start()
    {
        _cellGrid.FoodEaten += EatFood;

        _tails = new List<SnakeTail>();
        Vector2Int gridsize = _cellGrid.GetGridSize();
        Vector3Int spawnPosition = new Vector3Int(4, 0);

        for (int i = 0; i < 3; i++)
        {
            SnakeTail snakeTail = Instantiate(_snakeTailPrefab, this.transform);
            _cellGrid.TrySetInCellGrid(snakeTail, new Vector2Int(spawnPosition.x, spawnPosition.y));
            snakeTail.transform.localPosition = spawnPosition;
            _tails.Add(snakeTail);
            spawnPosition.x--;
        }
    }

    public void MoveSnake(Vector2Int direction)
    {
        Vector2Int movePosition = new Vector2Int((int)_tails[0].transform.position.x + direction.x, (int)_tails[0].transform.position.y + direction.y);
        Vector2Int lastTailPosition = new Vector2Int((int)_tails[^1].transform.position.x, (int)_tails[^1].transform.position.y);

        if (_cellGrid.TrySetInCellGrid(_tails[^1], movePosition))
        {
            _tails[^1].transform.localPosition = new Vector3(movePosition.x, movePosition.y);
            _cellGrid.RemoveRefFromCellGrid(lastTailPosition);
            SnakeTail tail = _tails[^1];
            _tails.RemoveAt(_tails.Count - 1);
            _tails.Insert(0, tail);
        }
    }

    private void EatFood(CellItem food)
    { }

    private void OnDisable()
    {
        _cellGrid.FoodEaten -= EatFood;
    }
}