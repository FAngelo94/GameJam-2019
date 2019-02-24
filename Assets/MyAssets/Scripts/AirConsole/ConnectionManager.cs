using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour
{
    public int playerCount;

    void Awake()
    {
        playerCount = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
        DontDestroyOnLoad(this);
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
