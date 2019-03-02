using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class TheEndCtrl : MonoBehaviour
{
    //button index
    private int index;

    private Button restart;
    private Button mainmenu;

    void Awake()
    {
        index = 0;//start game index

        restart = GameManager.instance.Panels.transform.Find("Restart").GetComponent<Button>();
        mainmenu = GameManager.instance.Panels.transform.Find("Quit").GetComponent<Button>();

        AirConsole.instance.onMessage += OnMessage;
    }

    private void Update()
    {
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
                    if (index < 1) index++;
                    else index = 0;
                }
                if (key.Equals("left"))
                {
                    if (index > 0) index--;
                    else index = 1;
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
                restart.Select();
                break;
            case 1:
                mainmenu.Select();
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
