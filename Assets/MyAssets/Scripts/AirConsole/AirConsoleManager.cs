using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AirConsoleManager : MonoBehaviour
{
    public void Ready()
    {
        SetActivePlayers();
        SceneManager.LoadScene("MainMenu");
    }

    private void SetActivePlayers()
    {
        //Set the currently connected devices as the active players (assigning them a player number)
        AirConsole.instance.SetActivePlayers();

        string activePlayerIds = "";
        foreach (int id in AirConsole.instance.GetActivePlayerDeviceIds)
        {
            activePlayerIds += id + "\n";
        }

        //Log to on-screen Console
        Debug.Log("Active Players were set to:\n" + activePlayerIds + " \n \n");
    }
}
