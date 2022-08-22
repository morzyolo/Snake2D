using UnityEngine;

public class CellGrid : MonoBehaviour
{
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2Int _gridSize;
    private CellItem[,] _cells;

    private void Awake()
    {
        _cells = new CellItem[_gridSize.x, _gridSize.y];
    }

    public bool TrySetInCellGrid(CellItem cellItem, Vector2Int position)
    {
        try
        {
            if (_cells[position.x, position.y] == null)
            {
                _cells[position.x, position.y] = cellItem;
                return true;
            }
                return false;
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }
}