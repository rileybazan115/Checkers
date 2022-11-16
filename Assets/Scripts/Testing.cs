using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<BoardSquare> grid;
	[SerializeField] private Board board;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<BoardSquare>(8, 8, 1f, new Vector3(-4, -4, 0), (Grid<BoardSquare> g, int x, int y) => new BoardSquare(g, x, y));
        board.SetGrid(grid);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Utils.GetMouseWorldPosition();
            BoardSquare boardSquare = grid.GetGridObject(mouseWorldPosition);
            Debug.Log(boardSquare.IsNull());
            if (boardSquare != null)
            {
                Debug.Log("click");
                boardSquare.AddValue(5);
            }
        }
    }
}
