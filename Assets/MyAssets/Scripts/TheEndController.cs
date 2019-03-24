using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class TheEndController : MonoBehaviour
{
    //button index
    private int index;

    private Button RestartButton;
    private Button MainMenu;

    void Awake()
    {
        index = 0;//start game index
        AirConsole.instance.onMessage += OnMessage;
    }

    private void Start()
    {
        RestartButton = GameManager.instance.Panels.transform.Find("Restart").GetComponent<Button>();
        MainMenu = GameManager.instance.Panels.transform.Find("Quit").GetComponent<Button>();
        UpdateUI();
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
                if (key.Equals("right"))
                {
                    index = 1;
                    UpdateUI();
                }
                if (key.Equals("left"))
                {
                    index = 0;
                    UpdateUI();
                }
 
                if (key.Equals("confirm"))
                {
                    switch(index)
                    {
                        case 0:
                            AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
                            GameManager.instance.Restart();
                            break;
                        case 1:
                            Debug.Log("Call GAMENAGER");
                            AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
                            GameManager.instance.Quit();
                            break;
                    }
                }
            }
        }
    }

    private void UpdateUI()
    {
        switch(index)
        {
            case 0:
                RestartButton.Select();
                break;
            case 1:
                MainMenu.Select();
                break;
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
