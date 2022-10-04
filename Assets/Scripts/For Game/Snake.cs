using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeTail _snakeTailPrefab;
    [SerializeField] private float _moveDelay;

    private LinkedList<SnakeTail> _tails;
    private CellGrid _cellGrid;
    private AudioSource _audioSource;
    private Vector2Int _moveDirection;
    private bool _snakeGrow;

    private void Start()
    {
        _tails = new LinkedList<SnakeTail>();
        _moveDirection = Vector2Int.right;
        _cellGrid = FindObjectOfType<CellGrid>();
        _audioSource = GetComponent<AudioSource>();
        GenerateSnake();
    }

    private void StartGame()
    {
        Move();
    }

    private void GenerateSnake()
    {
        Vector2Int gridsize = _cellGrid.GetGridSize();
        Vector2Int spawnPosition = new Vector2Int(2, gridsize.y / 2);
        for (int i = 0; i < 3; i++)
        {
            if (!TryInsertNewSnakeTail(spawnPosition)) break;
            spawnPosition.x++;
        }
    }

    public async void Move()
    {
        bool canMove = true;
        _snakeGrow = false;
        Vector2Int currentDirection = _moveDirection; 

        do
        {
            await Task.Delay((int)(_moveDelay * 1000));
            if (Vector2Int.Scale(currentDirection, _moveDirection) == Vector2Int.zero)
                currentDirection = _moveDirection;

            Transform firstTailTransform = _tails.First.Value.transform;
            Vector2Int movePosition = new Vector2Int((int)firstTailTransform.position.x + currentDirection.x,
                (int)firstTailTransform.position.y + currentDirection.y);

            if (_snakeGrow)
            {
                _snakeGrow = false;
                canMove = TryInsertNewSnakeTail(movePosition);
            }
            else
                canMove = TryReplaceSnakeTail(movePosition);
        } while (canMove);

        Game.GameOver?.Invoke();
    }

    private bool TryInsertNewSnakeTail(Vector2Int spawnPosition)
    {
        SnakeTail snakeTail = Instantiate(_snakeTailPrefab, this.transform);

        if (!_cellGrid.TrySetInCellGrid(snakeTail, spawnPosition)) return false;
        
        snakeTail.transform.localPosition = new Vector3(spawnPosition.x, spawnPosition.y);
        _tails.AddFirst(snakeTail);
        return true;
    }

    private bool TryReplaceSnakeTail(Vector2Int placePosition)
    {
        SnakeTail lastTail = _tails.Last.Value;
        _cellGrid.RemoveRefFromCellGrid(new Vector2Int((int)lastTail.transform.position.x, (int)lastTail.transform.position.y));
        if (!_cellGrid.TrySetInCellGrid(lastTail, placePosition)) return false;

        lastTail.transform.localPosition = new Vector3(placePosition.x, placePosition.y);
        _tails.RemoveLast();
        _tails.AddFirst(lastTail);
        return true;
    }

    private void EatCherry()
    {
        _snakeGrow = true;
        _audioSource.Play();
    }

    private void ChangeDirection(Vector2Int direction) => _moveDirection = direction;

    private void OnEnable()
    {
        MovementInput.DirectionChanged += ChangeDirection;
        CellGrid.CherryEaten += EatCherry;
        Game.GameStarted += StartGame;
    }

    private void OnDisable()
    {
        MovementInput.DirectionChanged -= ChangeDirection;
        CellGrid.CherryEaten -= EatCherry;
        Game.GameStarted -= StartGame;
    }
}