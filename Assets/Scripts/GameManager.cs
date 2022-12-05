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

    public Texture2D white;
    public Texture2D red;

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
					var checker = Instantiate(whiteChecker);
					checker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
					checker.GetComponent<Checker>().grid = grid;
					square.hasPiece = true;
					square.piece = checker.GetComponent<Checker>();
				}

				if (square.y % 2 == 1 && square.x % 2 == 0)
				{
                    var checker = Instantiate(whiteChecker);
                    checker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    checker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = checker.GetComponent<Checker>();
                }
			}

            if (square.y < 3)
			{
                if (square.y % 2 == 1 && square.x % 2 == 0)
				{
                    var checker = Instantiate(redChecker);
                    checker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    checker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = checker.GetComponent<Checker>();
                }

                if (square.y % 2 == 0 && square.x % 2 == 1)
				{
                    var checker = Instantiate(redChecker);
                    checker.transform.position = grid.GetWorldPosition(square.x, square.y) + cellSize;
                    checker.GetComponent<Checker>().grid = grid;
                    square.hasPiece = true;
                    square.piece = checker.GetComponent<Checker>();
                }
			}
		}
	}

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
        isGameOver = true;
    }


}
