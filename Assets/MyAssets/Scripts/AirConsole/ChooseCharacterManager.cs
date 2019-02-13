using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ChooseCharacterManager : MonoBehaviour
{
    public static ChooseCharacterManager instance;

    private int CountConfirm;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        CountConfirm = 0;
    }

    public void ConfirmPlayer()
    {
        CountConfirm++;
        int totalPlayer = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        if (CountConfirm == totalPlayer)
        {//Start the game

        }
    }
}
