using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //public GameObject prefab;

    private Grid<BoardSquare> grid;
    private Mesh mesh;
	private int modIndex;

	public void Awake()
	{
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

	public void SetGrid(Grid<BoardSquare> grid)
    {
        this.grid = grid;
        UpdateGridMesh();
        //grid.TriggerGridObjectChanged(x, y);
        grid.OnGridObjectChanged += Grid_OnGridObjectChanged;
    }

    public void Grid_OnGridObjectChanged(object sender, Grid<BoardSquare>.OnGridObjectChangedEventArgs e)
	{
        Debug.Log("grid changed");
        UpdateGridMesh();
	}

    public void UpdateGridMesh()
	{
		MeshUtils.CreateEmptyMeshArrays2d(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

		for (int x = 0; x < grid.GetWidth(); x++)
		{
			for (int y = 0; y < grid.GetHeight(); y++)
			{
                modIndex++;
                if (modIndex % 2 == 0) grid.GetGridObject(x, y).value = 0;
                if (modIndex % 2 == 1) grid.GetGridObject(x, y).value = 10;
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                BoardSquare gridObject = grid.GetGridObject(x, y);
                float gridValueNormalized = gridObject.GetValueNormalized();
                Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);
                MeshUtils.AddToMeshArrays2d(vertices, uvs, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);
                //Debug.Log(index);
            }
            modIndex--;
        }
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }		
}

