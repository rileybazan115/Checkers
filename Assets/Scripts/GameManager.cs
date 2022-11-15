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
    public bool IsGameOver = false;
    public GameObject GameOverWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver) GameOver();
        CheckWinLoss();
    }

    private void GameOver()
    {
        // Show game over screen
        GameOverWindow.SetActive(true);
    }

    private void CheckWinLoss()
    {
        // if player1 or player2 checkers are all gone
        IsGameOver = true;
    }


}
