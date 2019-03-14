using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour
{
    public int playerCount;

   public bool loaded;
   public bool ready;

    void Awake()
    {
        playerCount = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        loaded = false;
        ready = false;
        AirConsole.instance.onReady += OnReady;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(!loaded &&
            ready &&
            AudioManager.instance.IsReady())
        {
            AudioManager.instance.StartMenuMusic();
            SceneManager.LoadScene("MainMenu");
            loaded = true;
        }
    }

    void OnReady(string code)
    {
        Debug.Log("AirConsole is ready. Code: " + code);
        ready = true;
    }

    void OnConnect(int device_id)
    {
        UpdateControllers();
    }

    void OnDisconnect(int device_id)
    {
        UpdateControllers();
    }

    void UpdateControllers()
    {
        AirConsole.instance.SetActivePlayers();

        playerCount = 0;

        foreach (int devID in AirConsole.instance.GetControllerDeviceIds())
        {
            int playerID = AirConsole.instance.ConvertDeviceIdToPlayerNumber(devID);

            //Debug.Log(playerID);

            playerCount++;
        }
    }

    private void OnDestroy()
    {
        AirConsole.instance.onConnect -= OnConnect;
        AirConsole.instance.onDisconnect -= OnDisconnect;
    }
}
