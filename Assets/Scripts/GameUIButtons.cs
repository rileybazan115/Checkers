using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIButtons : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] string mainMenuScene;
    
    [SerializeField] GameObject howToPlayScreen;
    [SerializeField] GameObject mainScreen;


    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().build;
    }

    public void PlayAgain()
    {
        
    }

    public void HowToPlay()
    {
        mainScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void MainScreen()
    {
        mainScreen.SetActive(true);
        howToPlayScreen.SetActive(false);
    }

    public void MainMenu()
    {
        // Go to Main menu
        SceneManager.LoadScene(0);
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
