using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<Board> grid;
	[SerializeField] private Board board;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<Board>(8, 8, 1f, new Vector3(-4.5f, -4.5f, 0), (Grid<Board> g, int x, int y) => new Board(g, x, y));
        board.SetGrid(grid);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Utils.GetMouseWorldPosition();
            Board boardSquare = grid.GetGridObject(position);
            if (boardSquare != null)
            {
                Debug.Log("click");
                boardSquare.AddValue(5);
            }
        }
    }
}
