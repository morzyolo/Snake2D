using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CellGrid : MonoBehaviour
{
	[SerializeField] private Vector2Int _gridSize;
	private ICellItem[,] _cells;

	private void Awake()
	{
		_cells = new ICellItem[_gridSize.x, _gridSize.y];
		GetComponent<MeshFilter>().mesh = new MeshFactory().CreateMesh(_gridSize);
	}

	public Vector2Int GetGridSize() => _gridSize;

	public void RemoveRefFromGrid(Vector3 position)
		=> _cells[(int)position.x, (int)position.y] = null;

	public void SetCellItemInGrid(ICellItem item, Vector2Int potion)
		=> _cells[potion.x, potion.y] = item;

	public ICellItem GetCellItemOrNullFromGrid(Vector2Int position)
		=> _cells[position.x, position.y];

	public bool IsPositionInGrid(Vector2Int position)
		=> position.x < _gridSize.x && position.x >= 0
		&& position.y < _gridSize.y && position.y >= 0;
}