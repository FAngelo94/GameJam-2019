using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;

public class ButtonControl : MonoBehaviour
{
    public void StartGame()
    {
        AirConsole.instance.SetActivePlayers();
        SceneManager.LoadScene("ChoosePlayer");
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

