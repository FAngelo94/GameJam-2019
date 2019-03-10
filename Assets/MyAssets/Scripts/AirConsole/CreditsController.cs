using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public Button BackButton;

    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    private void Start()
    {
        BackButton.Select();
    }

    //AirConsole Functions
    void OnMessage(int device_id, JToken data)
    {
        //get master controller
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(AirConsole.instance.GetMasterControllerDeviceId());

        if (data != null && data["key"] != null && data["pressed"] != null)
        {
            string key = key = (string)data["key"];

            if ((bool)data["pressed"])
            { 
                if (key.Equals("confirm"))
                {
                    AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }

    void OnDestroy()
    {
        // unregister airconsole events on scene change
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}
