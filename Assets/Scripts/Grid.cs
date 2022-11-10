using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
	public const int HEAT_MAP_MAX_VALUE = 100;
	public const int HEAT_MAP_MIN_VALUE = 0;

	public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
	public class OnGridObjectChangedEventArgs : EventArgs
	{
		public int x;
		public int y;
	}

	private int width;
	private int height;
	private float cellSize;
	private Vector3 originPosition;
	private TGridObject[,] gridArray;
	private TextMesh[,] debugTextArray;

	public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
	{
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;
		this.originPosition = originPosition;

		gridArray = new TGridObject[width, height];
		debugTextArray = new TextMesh[width, height];

		for (int x = 0; x < gridArray.GetLength(0); x++)
		{
			for (int y = 0; y < gridArray.GetLength(1); y++)
			{
				gridArray[x, y] = createGridObject(this, x, y);
			}
		}

		//bool debug
		for (int x = 0; x < gridArray.GetLength(0); x++)
		{
			for (int y = 0; y < gridArray.GetLength(1); y++)
			{
				debugTextArray[x, y] = Utils.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y), Color.white, 100f);
			}
		}

		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, 0), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, height), GetWorldPosition(width, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(0, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, height), GetWorldPosition(0, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(0, 0), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(0, 0), GetWorldPosition(0, height), Color.white, 100f);

		OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
		{
			debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
		};
	}

	public Vector3 GetWorldPosition(int x, int y)
	{
		return new Vector3(x, y) * cellSize + originPosition;
	}

	public void GetXY(Vector3 worldPosition, out int x, out int y)
	{
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
	}

	public void SetGridObject(int x, int y, TGridObject value)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
			//gridArray[x, y, z] = Mathf.Clamp(value, HEAT_MAP_MIN_VALUE, HEAT_MAP_MAX_VALUE);
			gridArray[x, y] = value;
			if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
			debugTextArray[x, y].text = gridArray[x, y].ToString();
		}
	}

	public void TriggerGridObjectChanged(int x, int y)
	{
		if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
	}

	public void SetGridObject(Vector3 worldPosition, TGridObject value)
	{
		int x, y;
		GetXY(worldPosition, out x, out y);
		SetGridObject(x, y, value);
	}

	public TGridObject GetGridObject(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
			return gridArray[x, y];
		}
		else
		{
			return default(TGridObject);
		}
	}

	public TGridObject GetGridObject(Vector3 worldPostion)
	{
		int x, y;
		GetXY(worldPostion, out x, out y);
		return GetGridObject(x, y);
	}

	/*public void GetLowestValue(out int lowestValue, out Vector3 lowestValuePosition)
	{
		lowestValue = int.MaxValue;
		lowestValuePosition = Vector3.zero;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				for (int z = 0; z < depth; z++)
				{
					TGridObject go = GetGridObject(x, y, z);
					
					if (GetGridObject(x, y, z) < lowestValue)
					{
						GetGridObject(x, y, z);

						lowestValue = GetGridObject(x, y, z);
						lowestValuePosition = GetWorldPosition(x, y, z);
					}
				}
			}
		}
	}

	public void GetNearestLowestValue(Vector3 position, out int lowestValue, out Vector3 lowestValuePosition)
	{
		int x, y, z;
		lowestValue = int.MaxValue;
		lowestValuePosition = Vector3.zero;

		GetXYZ(position, out x, out y, out z);

		if (GetGridObject(x, y, z) < lowestValue)
		{
			lowestValue = GetGridObject(x, y, z);
		}

		CenterSearch(x, y, z, lowestValue, lowestValuePosition);
	}

	//might need to do something with positive and negatives, not sure
	public void CenterSearch(int x, int y, int z, int lowestValue, Vector3 lowestValuePosition)
	{
		if (GetGridObject(x, y, z) < lowestValue)
		{
			lowestValue = GetGridObject(x, y, z);
			lowestValuePosition = GetWorldPosition(x, y, z);
		}
		if (x + 1 <= width)
		{
			CenterSearch(x + 1, y, z, lowestValue, lowestValuePosition);
		}
		else if (y + 1 <= height)
		{
			CenterSearch(x, y + 1, z, lowestValue, lowestValuePosition);
		}
		*//*else if (z + 1 <= width)
		{
			CenterSearch(x, y, z + 1, lowestValue, lowestValuePosition);
		}*//*
	}*/

	public int GetWidth()
	{
		return gridArray.GetLength(0);
	}

	public int GetHeight()
	{
		return gridArray.GetLength(1);
	}

	public float GetCellSize()
	{
		return cellSize;
	}
}
