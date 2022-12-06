using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIButtons : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string mainMenuScene;

    [SerializeField] GameObject board;

    [SerializeField] GameObject howToPlayScreen;
    [SerializeField] GameObject mainScreen;


    public void Play()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PlayAgain()
    {
        
    }

    public void HowToPlay()
    {
        if (board != null) board.SetActive(false);
        mainScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void MainScreen()
    {
        if (board != null) board.SetActive(true);
        mainScreen.SetActive(true);
        howToPlayScreen.SetActive(false);
    }

    public void MainMenu()
    {
        // Go to Main menu
        SceneManager.LoadScene(0);
    }

    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
