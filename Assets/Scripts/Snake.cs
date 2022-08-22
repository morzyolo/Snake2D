using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    [SerializeField] private CellGrid cellGrid;

    private List<SnakeTail> tails;

    private void Start()
    {
        tails = FindObjectsOfType<SnakeTail>().ToList();
        for (int i = 0; i < tails.Count; i++)
        {
            Vector2Int position = new Vector2Int((int)tails[i].Position.position.x, (int)tails[i].Position.position.y);
            cellGrid.TrySetInCellGrid(tails[i], position);
        }
    }
}