using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    //red at bottom, white at top
    public bool isRed;
    bool isKing;
    int kingRow;
    public Grid<BoardSquare> grid;
    Vector2 xyPosition;

    Color[] colors;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Checker(bool isRed, Vector2 xyPosition, Texture2D texture)
	{
		this.isRed = isRed;
		this.xyPosition = xyPosition;

		if (isRed == true)
		{
			this.kingRow = 7;
			this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1);
        }
		else
		{
			this.kingRow = 0;
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1);
        }
	}

	public void Move(Vector2 checkerPosition, Vector2 nextMovePosition)
	{
        grid.GetXY(checkerPosition, out int cX, out int cY);
        grid.GetXY(nextMovePosition, out int nX, out int nY);
        //red
        if (isRed)
		{
            //move 1
            if (nY - cY == 1 && Mathf.Abs(nX - cX) == 1)
			{
                Debug.Log("Move");
                if (!CheckForPiece(nextMovePosition))
				{
                    grid.GetGridObject(cX, cY).hasPiece = false;
                    grid.GetGridObject(nX, nY).hasPiece = true;
                    grid.GetGridObject(cX, cY).piece = null;
                    grid.GetGridObject(nX, nY).piece = this;
                    if (nY == kingRow) isKing = true;
                    MovePiece(nextMovePosition);
				}
			}
            //move 2 and capture
            if (nY - cY == 2 && Mathf.Abs(nX - cX) == 2)
            {
                Vector2 inbetween = (nextMovePosition + checkerPosition) / 2;
                grid.GetXY(inbetween, out int iX, out int iY);
                if (CheckForPiece(inbetween) && CheckForPiece(nextMovePosition) == false && grid.GetGridObject(iX, iY).piece.isRed == false)
				{
                    grid.GetGridObject(cX, cY).hasPiece = false;
                    grid.GetGridObject(nX, nY).hasPiece = true;
                    grid.GetGridObject(cX, cY).piece = null;
                    grid.GetGridObject(nX, nY).piece = this;
                    if (nY == kingRow) isKing = true;
                    MovePiece(nextMovePosition);
                    GameManager.instance.whiteCheckerCount--;
                    CapturePiece(inbetween);
				}
            }
        }

		//white
		else
		{
			if (nY - cY == -1 && Mathf.Abs(nX - cX) == 1)
			{
                if (!CheckForPiece(nextMovePosition))
                {
                    grid.GetGridObject(cX, cY).hasPiece = false;
                    grid.GetGridObject(nX, nY).hasPiece = true;
                    grid.GetGridObject(cX, cY).piece = null;
                    grid.GetGridObject(nX, nY).piece = this;
                    if (nY == kingRow) isKing = true;
                    MovePiece(nextMovePosition);
                }
            }
			if (nY - cY == -2 && Mathf.Abs(nX - cX) == 2)
			{
                Vector2 inbetween = (nextMovePosition + checkerPosition) / 2;
                grid.GetXY(inbetween, out int iX, out int iY);
                if (CheckForPiece(inbetween) && CheckForPiece(nextMovePosition) == false && grid.GetGridObject(iX, iY).piece.isRed == true)
                {
                    grid.GetGridObject(cX, cY).hasPiece = false;
                    grid.GetGridObject(nX, nY).hasPiece = true;
                    grid.GetGridObject(cX, cY).piece = null;
                    grid.GetGridObject(nX, nY).piece = this;
                    if (nY == kingRow) isKing = true;
                    MovePiece(nextMovePosition);
                    GameManager.instance.redCheckerCount--;
                    CapturePiece(inbetween);
                }
            }
		}
		//king
		if (isKing)
		{
            if (Mathf.Abs(nY - cY) == 1 && Mathf.Abs(nX - cX) == 1)
            {
                if (!CheckForPiece(nextMovePosition))
                {
                    grid.GetGridObject(cX, cY).hasPiece = false;
                    grid.GetGridObject(nX, nY).hasPiece = true;
                    grid.GetGridObject(cX, cY).piece = null;
                    grid.GetGridObject(nX, nY).piece = this;
                    MovePiece(nextMovePosition);
                }
            }

            if (Mathf.Abs(nY - cY) == 2 && Mathf.Abs(cX - cX) == 2)
			{
                Vector2 inbetween = (nextMovePosition + checkerPosition) / 2;
                grid.GetXY(inbetween, out int iX, out int iY);
                if (CheckForPiece(inbetween) && CheckForPiece(nextMovePosition) == false && grid.GetGridObject(iX, iY).piece.isRed != this.isRed)
                {
                    grid.GetGridObject(cX, cY).hasPiece = false;
                    grid.GetGridObject(nX, nY).hasPiece = true;
                    grid.GetGridObject(cX, cY).piece = null;
                    grid.GetGridObject(nX, nY).piece = this;
                    MovePiece(nextMovePosition);
                    if (grid.GetGridObject(iX, iY).piece.isRed) GameManager.instance.redCheckerCount--;
                    if (!grid.GetGridObject(iX, iY).piece.isRed) GameManager.instance.whiteCheckerCount--;
                    CapturePiece(inbetween);
                }
            }
        }

        //return false;
	}

    public bool CheckForPiece(Vector2 nextMovePosition)
	{
        if (FindSquare(nextMovePosition).hasPiece == true) return true;

        return false;
	}

    public void CapturePiece(Vector2 piece)
	{
        BoardSquare square = FindSquare(piece);
        Destroy(square.piece.gameObject);
        square.piece = null;
        square.hasPiece = false;
	}

    public BoardSquare FindSquare(Vector2 location)
	{
        int x, y;
        grid.GetXY(location, out x, out y);
        return grid.GetGridObject(x, y);
    }        

    public void MovePiece(Vector2 movePosition)
	{
        grid.GetXY(movePosition, out int x, out int y);
        this.transform.position = grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f;
	}

    public void King()
	{
        if (this.xyPosition.y == kingRow)
		{
            isKing = true;
		}
	}

    public override string ToString()
    {
        return this.transform.position.ToString();
    }
}



/*if (isRed)
{
    //move 1
    if (nextMovePosition.y - checkerPosition.y == 1 && Mathf.Abs(nextMovePosition.x - checkerPosition.x) == 1)
    {
        Debug.Log("Move");
        if (!CheckForPiece(nextMovePosition))
        {

            MovePiece(nextMovePosition);
        }
    }
    //move 2 and capture
    if (nextMovePosition.y - checkerPosition.y == 2 && Mathf.Abs(nextMovePosition.x - checkerPosition.x) == 2)
    {
        Vector2 inbetween = (nextMovePosition + checkerPosition) / 2;
        if (CheckForPiece(nextMovePosition) && CheckForPiece(nextMovePosition) == false)
        {
            MovePiece(nextMovePosition);
            CapturePiece(inbetween);
        }
    }
}

//white
else
{
    if (nextMovePosition.y - checkerPosition.y == -1 && Mathf.Abs(nextMovePosition.x - checkerPosition.x) == 1)
    {
        if (!CheckForPiece(nextMovePosition))
        {
            MovePiece(nextMovePosition);
        }
    }
    if (nextMovePosition.y - checkerPosition.y == -2 && Mathf.Abs(nextMovePosition.x - checkerPosition.x) == 2)
    {
        Vector2 inbetween = (nextMovePosition + checkerPosition) / 2;
        if (CheckForPiece(nextMovePosition) && CheckForPiece(nextMovePosition) == false)
        {
            MovePiece(nextMovePosition);
            CapturePiece(inbetween);
        }
    }
}
//king
if (isKing)
{
    if (Mathf.Abs(nextMovePosition.y - checkerPosition.y) == 1 && Mathf.Abs(nextMovePosition.x - checkerPosition.x) == 1)
    {
        if (!CheckForPiece(nextMovePosition))
        {
            MovePiece(nextMovePosition);
        }
    }

    if (Mathf.Abs(nextMovePosition.y - checkerPosition.y) == 2 && Mathf.Abs(nextMovePosition.x - checkerPosition.x) == 2)
    {
        Vector2 inbetween = (nextMovePosition + checkerPosition) / 2;
        if (CheckForPiece(nextMovePosition) && CheckForPiece(nextMovePosition) == false)
        {
            MovePiece(nextMovePosition);
            CapturePiece(inbetween);
        }
    }
}*/
