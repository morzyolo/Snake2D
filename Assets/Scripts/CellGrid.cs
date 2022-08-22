using System;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    public Action<CellItem> FoodEaten; 

    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2Int _gridSize;
    private CellItem[,] _cells;

        
    private void Awake()
    {
        _cells = new CellItem[_gridSize.x, _gridSize.y];
    }

    public Vector2Int GetGridSize() => _gridSize;

    public bool TrySetInCellGrid(CellItem cellItem, Vector2Int position)
    {
        try
        {
            if (_cells[position.x, position.y] is SnakeTail) return false;

            if (_cells[position.x, position.y] == null)
            {
                _cells[position.x, position.y] = cellItem;
                return true;
            }

            if (_cells[position.x, position.y] is Cherry || cellItem is SnakeTail) return false;
            {
                _cells[position.x, position.y] = cellItem;
                FoodEaten?.Invoke(cellItem);
                return true;
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }
}