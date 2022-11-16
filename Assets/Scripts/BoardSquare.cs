using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare : MonoBehaviour
{
    public int value;
    private int MIN;
    private int MAX = 100;

    private Grid<BoardSquare> grid;
    private int x;
    private int y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BoardSquare(Grid<BoardSquare> grid, int x, int y)
	{
        this.grid = grid;
        this.x = x;
        this.y = y;
	}

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
    }

    public bool IsNull()
    {
        if (this == null) return true;
        else return false;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
