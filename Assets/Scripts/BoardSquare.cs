using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare : MonoBehaviour
{
    public int value;
    public bool hasPiece;
    private int MIN;
    private int MAX = 100;
    public Checker piece;

    private Grid<BoardSquare> grid;
    public int x;
    public int y;

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

    public override string ToString()
    {
        return this.transform.position.ToString();
    }
}
