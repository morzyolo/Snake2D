using System.Collections.Generic;
using UnityEngine;

public class SnakeFactory
{
	private readonly CellGrid _cellGrid;
	private readonly SnakeTail _tailPrefab;

	public SnakeFactory(CellGrid cellGrid, SnakeTail tailPrefab)
	{
		_cellGrid = cellGrid;
		_tailPrefab = tailPrefab;
	}

	public LinkedList<SnakeTail> CreateSnake(Transform snakeTransform, int maxTailCount)
	{
		var tails = new LinkedList<SnakeTail>();
		Vector2Int gridSize = _cellGrid.GetGridSize();
		var spawnPosition = new Vector2Int(Mathf.Clamp(gridSize.x / 5, 0, 4), gridSize.y / 2);
		int tailsCount = Mathf.Clamp(gridSize.x / 4, 1, maxTailCount);

		for (int i = 0; i < tailsCount; i++)
		{
			SnakeTail tail = Object.Instantiate(_tailPrefab, snakeTransform);
			tail.transform.localPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0);
			_cellGrid.SetCellItemInGrid(tail, spawnPosition);
			tails.AddFirst(tail);
			spawnPosition.x++;
		}

		return tails;
	}
}
