using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [HideInInspector]
    public bool isGameOver = false;
    public GameObject gameOverWindow;
    public GameObject redChecker;
    public GameObject whiteChecker;
    public GameObject currentPiece;

    private int modIndex;

    private Grid<BoardSquare> grid;
    [SerializeField] private Board board;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<BoardSquare>(8, 8, 1f, new Vector3(-4, -4, 0), (Grid<BoardSquare> g, int x, int y) => new BoardSquare(g, x, y));
        Vector3 cellSize = new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f;
        board.SetGrid(grid);

		foreach (BoardSquare square in grid.GetArray())
		{
			if (square.y > 4)
			{
				if (square.y % 2 == 0 && square.x % 2 == 1)
				{
					Instantiate(whiteChecker);
					whiteChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    square.hasPiece = true;
                    square.piece = whiteChecker;
				}

				if (square.y % 2 == 1 && square.x % 2 == 0)
				{
					Instantiate(whiteChecker);
					whiteChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    square.hasPiece = true;
                    square.piece = whiteChecker;
				}
			}

            if (square.y < 3)
			{
                if (square.y % 2 == 1 && square.x % 2 == 0)
				{
                    Instantiate(redChecker);
                    redChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    square.hasPiece = true;
                    square.piece = redChecker;
				}

                if (square.y % 2 == 0 && square.x % 2 == 1)
				{
                    Instantiate(redChecker);
                    redChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    square.hasPiece = true;
                    square.piece = redChecker;
                }
			}
		}
	}

    //need to know whose turn it is
    //know whether a piece is already selected, and who pieces belong too
    //

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Utils.GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);
            BoardSquare boardSquare = grid.GetGridObject(mouseWorldPosition);

            if (boardSquare.hasPiece == true)
			{
                currentPiece = boardSquare.piece;
			}
            /*if (boardSquare != null)
            {
                Debug.Log("click");
                boardSquare.AddValue(5);
            }*/
        }

        if (isGameOver) GameOver();
        CheckWinLoss();
    }

    private void GameOver()
    {
        // Show game over screen
        gameOverWindow.SetActive(true);
    }

    private void CheckWinLoss()
    {
        // if player1 or player2 checkers are all gone
        isGameOver = true;
    }


}
