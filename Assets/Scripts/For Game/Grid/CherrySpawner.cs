using UnityEngine;

public class CherrySpawner : MonoBehaviour
{
	[SerializeField] private CellGrid _cellGrid;
	[SerializeField] private Snake _snake;

	[SerializeField] private Cherry _cherryPrefab;

	private Cherry _cherry;

	private void Start() => SpawnCherry();

	private void SpawnCherry()
	{
		Vector3 position = GenerateRandomEmptyPosition();
		_cherry = Instantiate(_cherryPrefab, transform);
		_cherry.transform.localPosition = position;
		_cellGrid.SetCellItemInGrid(_cherry, new Vector2Int((int)position.x, (int)position.y));
	}

	private void TransferCherry()
	{
		_cellGrid.RemoveRefFromGrid(_cherry.transform.position);
		Vector3 position = GenerateRandomEmptyPosition();
		_cherry.transform.localPosition = position;
		_cellGrid.SetCellItemInGrid(_cherry, new Vector2Int((int)position.x, (int)position.y));
	}

	private Vector3 GenerateRandomEmptyPosition()
	{
		Vector2Int position;
		Vector2Int gridSize = _cellGrid.GetGridSize();

		do
		{
			position = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y));
		} while (_cellGrid.GetCellItemOrNullFromGrid(position) != null);

		return new Vector3(position.x, position.y, 0);
	}

	private void OnEnable() => _snake.CherryEaten += TransferCherry;

	private void OnDisable() => _snake.CherryEaten -= TransferCherry;
}