using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Action GameOverAction;

    [SerializeField] private CellGrid _cellGrid;
    [SerializeField] private SnakeTail _snakeTailPrefab;
    [SerializeField] private float _moveDelay;

    private List<SnakeTail> _tails;
    private Vector2Int _moveDirection;
    private bool _snakeGrow;

    private void Start()
    {
        InputManager.ChangeDirectionAction += ChangeDirection;
        CellGrid.CherryEatenAction += EatCherry;

        _tails = new List<SnakeTail>();
        _moveDirection = Vector2Int.right;

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

    public async void MoveSnake()
    {
        bool canMove = true;
        _snakeGrow = false;
        Vector2Int currentDirection = _moveDirection;

        while (canMove)
        {
            await Task.Delay((int)(_moveDelay * 1000));
            if (Vector2Int.Scale(currentDirection, _moveDirection) == Vector2Int.zero)
                currentDirection = _moveDirection;

            Vector2Int movePosition = new Vector2Int((int)_tails[0].transform.position.x + currentDirection.x,
                (int)_tails[0].transform.position.y + currentDirection.y);

            if (_snakeGrow)
            {
                _snakeGrow = false;
                SnakeTail newSnakeTail = Instantiate(_snakeTailPrefab, this.transform);
                if (_cellGrid.TrySetInCellGrid(newSnakeTail, movePosition))
                {
                    newSnakeTail.transform.localPosition = new Vector3(movePosition.x, movePosition.y);
                    _tails.Insert(0, newSnakeTail);
                }
            }
            else
            {
                int lastIndex = _tails.Count - 1;
                Vector2Int lastTailPosition = new Vector2Int((int)_tails[lastIndex].transform.position.x,
                    (int)_tails[lastIndex].transform.position.y);

                _cellGrid.RemoveRefFromCellGrid(lastTailPosition);
                canMove = _cellGrid.TrySetInCellGrid(_tails[lastIndex], movePosition);
                if (canMove)
                {
                    _tails[lastIndex].transform.localPosition = new Vector3(movePosition.x, movePosition.y);
                    SnakeTail tail = _tails[lastIndex];
                    _tails.RemoveAt(lastIndex);
                    _tails.Insert(0, tail);
                }
                else
                {
                    GameOverAction?.Invoke();
                }
            }
        }
    }

    private void EatCherry()
    {
        _snakeGrow = true;
    }

    private void ChangeDirection(Vector2Int direction) => _moveDirection = direction;

    private void OnDisable()
    {
        InputManager.ChangeDirectionAction -= ChangeDirection;
        CellGrid.CherryEatenAction -= EatCherry;
    }
}