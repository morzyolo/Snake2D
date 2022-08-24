using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CellGrid _cellGrid;
    [SerializeField] private SnakeTail _snakeTailPrefab;

    [SerializeField] private float _moveDelay;
    private List<SnakeTail> _tails;

    private Vector2Int _moveDirection;

    private void Start()
    {
        _inputManager.ChangeDirectionAction += ChangeDirection;
        _cellGrid.FoodEatenAction += EatFood;

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
        Vector2Int currentDirection = _moveDirection;

        while (canMove)
        {
            Vector2Int movePosition = new Vector2Int((int)_tails[0].transform.position.x + currentDirection.x,
                (int)_tails[0].transform.position.y + currentDirection.y);

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

            await Task.Delay((int)(_moveDelay * 1000));
            if (Vector2Int.Scale(currentDirection, _moveDirection) == Vector2Int.zero)
                currentDirection = _moveDirection;
        }
    }

    private void EatFood(CellItem food)
    { }  

    private void ChangeDirection(Vector2Int direction) => _moveDirection = direction;

    private void OnDisable()
    {
        _inputManager.ChangeDirectionAction -= ChangeDirection;
        _cellGrid.FoodEatenAction -= EatFood;
    }
}