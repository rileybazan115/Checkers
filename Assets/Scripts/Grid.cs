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
	private int counter = 0;

	[SerializeField] Checker checker;

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
				//debugTextArray[x, y] = Utils.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
				Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

				/*if (y > 4) { Checker checker = new Checker(false, new Vector2(x, y)); }
				if (y < 3) { Checker checker = new Checker(true, new Vector2(x, y)); }*/
				if (y > 4) { }
			}
		}

		Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
		Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
		

		OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
		{
			//debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
		};
	}

	public Vector3 GetWorldPosition(int x, int y)
	{
		return new Vector3(x, y) * cellSize + originPosition;
	}

	public void GetXY(Vector3 worldPosition, out int x, out int y)
	{
		counter++;
		x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
		y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
	}

	public void SetGridObject(int x, int y, TGridObject value)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
		{
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
			//Debug.Log(x + " " + y);
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
		//Debug.Log(x + " " + y);
		return GetGridObject(x, y);
	}

	public Array GetArray()
	{
		return gridArray;
	}

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
