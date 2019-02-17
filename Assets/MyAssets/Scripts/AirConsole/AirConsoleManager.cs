using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AirConsoleManager : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
