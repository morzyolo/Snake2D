using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	public event Action CherryEaten;
	public event Action Hited;

	[SerializeField] private CellGrid _cellGrid;
	[SerializeField] private Game _game;

	[Header("Configs")]
	[SerializeField] private SnakeTail _snakeTailPrefab;
	[SerializeField] private Vector3 _offScreenPosition;
	[SerializeField] private int _maxStartTailCount = 3;
	[SerializeField] private float _moveDelay;

	private LinkedList<SnakeTail> _tails;
	private List<SnakeTail> _additionalTails;
	private Vector2Int _moveDirection = Vector2Int.right;

	private void Start()
	{
		_additionalTails = new List<SnakeTail>();
		_tails = new SnakeFactory(_cellGrid, _snakeTailPrefab)
			.CreateSnake(transform, _maxStartTailCount);
	}

	private void StartMove()
	{
		_moveDirection = Vector2Int.right;
		StartCoroutine(Move());
	}

	private IEnumerator Move()
	{
		Vector2Int movePosition;
		Vector2Int previousDirection = _moveDirection;
		var delay = new WaitForSeconds(_moveDelay);

		do
		{
			yield return delay;

			if (Vector2Int.Scale(previousDirection, _moveDirection) == Vector2Int.zero)
				previousDirection = _moveDirection;

			Vector3 firstTailPosition = _tails.First.Value.transform.position;
			movePosition = new Vector2Int((int)firstTailPosition.x + previousDirection.x,
				(int)firstTailPosition.y + previousDirection.y);
		} while (CanMoveToNextPosition(movePosition));

		Hited?.Invoke();
	}

	private bool CanMoveToNextPosition(Vector2Int nextPosition)
	{
		if (!_cellGrid.IsPositionInGrid(nextPosition)) return false;

		ICellItem cellItem = _cellGrid.GetCellItemOrNullFromGrid(nextPosition);

		if (cellItem is SnakeTail) return false;

		if (cellItem is Cherry)
		{
			InsertNewTail();
			CherryEaten?.Invoke();
		}

		TransferTail(nextPosition);
		return true;
	}

	private void InsertNewTail()
	{
		SnakeTail newTail = Instantiate(_snakeTailPrefab, _offScreenPosition, Quaternion.identity, transform);
		_additionalTails.Add(newTail);
	}

	private void TransferTail(Vector2Int nextPosition)
	{
		SnakeTail tail = GetNextTail();

		_cellGrid.SetCellItemInGrid(tail, new Vector2Int(nextPosition.x, nextPosition.y));
		tail.transform.localPosition = new Vector3(nextPosition.x, nextPosition.y, 0);
		_tails.AddFirst(tail);
	}

	private SnakeTail GetNextTail()
	{
		SnakeTail tail;
		if (_additionalTails.Count > 0)
		{
			tail = _additionalTails[^1];
			_additionalTails.Remove(tail);
		}
		else
		{
			tail = _tails.Last.Value;
			_cellGrid.RemoveRefFromGrid(tail.transform.position);
			_tails.RemoveLast();
		}
		return tail;
	}

	public void ChangeDirection(Vector2Int direction) => _moveDirection = direction;

	private void OnEnable() => _game.GameStarted += StartMove;

	private void OnDisable() => _game.GameStarted -= StartMove;
}