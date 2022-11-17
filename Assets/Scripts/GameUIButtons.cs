using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIButtons : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] GameObject howToPlayScreen;
    [SerializeField] GameObject mainMenu;


    public void Play()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void PlayAgain()
    {
        
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void BackToMainMenuButton()
    {
        howToPlayScreen.SetActive(false);
        mainMenu.SetActive(true);
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
