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
        AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
        SceneManager.LoadScene("ChoosePlayer");
    }

    public void GoToOptions()
    {
        AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
        SceneManager.LoadScene("Options");
    }

    public void GoToCredits()
    {
        AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
        SceneManager.LoadScene("MainMenu");
    }
}

