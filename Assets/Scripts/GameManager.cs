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
    private Checker currentPiece;

    private int redCheckerCount = 0;
    private int whiteCheckerCount = 0;

    private int modIndex = 0;

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
                    whiteChecker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = whiteChecker.GetComponent<Checker>();

                    // Louis code
                    whiteCheckerCount++;
				}

				if (square.y % 2 == 1 && square.x % 2 == 0)
				{
					Instantiate(whiteChecker);
					whiteChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    whiteChecker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = whiteChecker.GetComponent<Checker>();


                    // Louis code
                    whiteCheckerCount++;
                }
			}

            if (square.y < 3)
			{
                if (square.y % 2 == 1 && square.x % 2 == 0)
				{
                    Instantiate(redChecker);
                    redChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    redChecker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = redChecker.GetComponent<Checker>();


                    // Louis code
                    redCheckerCount++;
                }

                if (square.y % 2 == 0 && square.x % 2 == 1)
				{
                    Instantiate(redChecker);
                    redChecker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    redChecker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = redChecker.GetComponent<Checker>();

                    // Louis code
                    redCheckerCount++;
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
            grid.GetXY(mouseWorldPosition, out int x, out int y);
            BoardSquare boardSquare = grid.GetGridObject(x, y);
             
            if (boardSquare.hasPiece == true)
			{
                currentPiece = boardSquare.piece;
                currentPiece.transform.position = new Vector3(-5, -5, 0);
                Debug.Log(currentPiece);
            }

            if (boardSquare.hasPiece == false)
			{
                currentPiece.Move(currentPiece.transform.position, mouseWorldPosition);
			}
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
        if(redCheckerCount <= 0 || whiteCheckerCount <= 0)
        {
          isGameOver = true;
        }
    }


}
