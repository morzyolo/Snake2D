using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CellGrid : MonoBehaviour
{
    public static Action CherryEaten; 

    [SerializeField] private Vector2Int _gridSize;
    private ICellItem[,] _cells;

    private Mesh _mesh;

    private void Awake()
    {
        _cells = new ICellItem[_gridSize.x, _gridSize.y];
        Camera.main.transform.position = new Vector3((float)_gridSize.x / 2, (float)_gridSize.y / 2, -10f);
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "GridMap";

        Vector3[] vetices = new Vector3[(_gridSize.x + 1) * (_gridSize.y + 1)];
        Vector2[] uv = new Vector2[vetices.Length];
        for (int y = 0, i = 0; y < _gridSize.y + 1; y++)
        {
            for (int x = 0; x < _gridSize.x + 1; x++, i++)
            {
                vetices[i] = new Vector3(x, y);
                uv[i] = new Vector2(XYtoUV(x), XYtoUV(y));
            }
        }
        _mesh.vertices = vetices;
        _mesh.uv = uv;

        int[] triangles = new int[_gridSize.x * _gridSize.y * 6];
        for (int y = 0, t = 0, v = 0; y < _gridSize.y; y++, v++)
        {
            for (int x = 0; x < _gridSize.x; x++, t+=6, v++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + _gridSize.x + 1;
                triangles[t + 2] = triangles[t + 3] = v + 1;
                triangles[t + 5] = v + _gridSize.x + 2;
            }
        } 
        _mesh.triangles = triangles;

        _mesh.RecalculateNormals();
    }

    private float XYtoUV(int t)
    {
        t %= 4;

        if (t == 0 || t == 2) return 0.5f;
        if (t == 1) return 1f;
        return 0f;
    }

    public Vector2Int GetGridSize() => _gridSize;

    public void RemoveRefFromCellGrid(Vector2Int position) => _cells[position.x, position.y] = null;

    public bool TrySetInCellGrid(ICellItem cellItem, Vector2Int position)
    {
        try
        {
            if (_cells[position.x, position.y] is SnakeTail) return false;
            if (_cells[position.x, position.y] is Cherry)
            {
                if (cellItem is Cherry) return false;
                if (cellItem is SnakeTail)
                    CherryEaten?.Invoke();
            }
            _cells[position.x, position.y] = cellItem;
            return true;
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }
}