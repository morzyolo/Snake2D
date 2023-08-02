using UnityEngine;

public class MeshFactory
{
	public Mesh CreateMesh(Vector2Int gridSize)
	{
		var mesh = new Mesh { name = "GridMap" };

		Vector3[] vetices = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
		Vector2[] uv = new Vector2[vetices.Length];
		for (int y = 0, i = 0; y < gridSize.y + 1; y++)
		{
			for (int x = 0; x < gridSize.x + 1; x++, i++)
			{
				vetices[i] = new Vector3(x, y);
				uv[i] = new Vector2(XYtoUV(x), XYtoUV(y));
			}
		}
		mesh.vertices = vetices;
		mesh.uv = uv;

		int[] triangles = new int[gridSize.x * gridSize.y * 6];
		for (int y = 0, t = 0, v = 0; y < gridSize.y; y++, v++)
		{
			for (int x = 0; x < gridSize.x; x++, t += 6, v++)
			{
				triangles[t] = v;
				triangles[t + 1] = triangles[t + 4] = v + gridSize.x + 1;
				triangles[t + 2] = triangles[t + 3] = v + 1;
				triangles[t + 5] = v + gridSize.x + 2;
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();

		return mesh;
	}

	private float XYtoUV(int t)
	{
		t %= 4;

		if (t == 0 || t == 2) return 0.5f;
		if (t == 1) return 1f;
		return 0f;
	}
}
