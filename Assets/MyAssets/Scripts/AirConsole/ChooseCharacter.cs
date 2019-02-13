using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ChooseCharacter : MonoBehaviour
{
    [Header("Player ID")]
    public int PlayerID;
    [Header("Choose Player Component")]
    public GameObject Player;

    private int IndexPlayer;

    // Start is called before the first frame update
    void Start()
    {
        IndexPlayer = 0;
    }

    //AirConsole Functions
    void OnMessage(int device_id, JToken data)
    {
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);

        if (active_player == PlayerID)
        {
            if (data != null &&
                data["key"] != null &&
                data["pressed"] != null)
            {
                string key = key = (string)data["key"];
                if ((bool)data["pressed"])
                {
                    Debug.Log("IF");
                    if (key.Equals("right"))
                    {
                    }
                    if (key.Equals("left"))
                    {
                    }
                    if (key.Equals("confirm"))
                    {
                    }
                }
            }
        }
    }

    private void ChangeCharacter()
    {

    }

    private void ChoosePlayer()
    {
        ChooseCharacterManager.instance.ConfirmPlayer();
    }
}
