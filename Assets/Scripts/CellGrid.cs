using System;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Action<CellItem> FoodEatenAction; 

    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2Int _gridSize;
    private CellItem[,] _cells;

    private void Awake()
    {
        _cells = new CellItem[_gridSize.x, _gridSize.y];
    }

    public Vector2Int GetGridSize() => _gridSize;

    public void RemoveRefFromCellGrid(Vector2Int position) => _cells[position.x, position.y] = null;

    public bool TrySetInCellGrid(CellItem cellItem, Vector2Int position)
    {
        try
        {
            if (_cells[position.x, position.y] is SnakeTail) return false;

            _cells[position.x, position.y] = cellItem;
            if (_cells[position.x, position.y] is Cherry && cellItem is SnakeTail) FoodEatenAction?.Invoke(cellItem);
            return true;
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }
}