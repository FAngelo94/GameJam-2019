using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class MainMenuCtrl : MonoBehaviour
{
    //button index
    private int index;

    public Button start;
    public Button credits;
    public Button quit;

    void Awake()
    {
        index = 0;//start game index
        
        AirConsole.instance.onMessage += OnMessage;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        
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
                    if (index < 0) index++;
                    else index = -1;
                    UpdateUI();

                }
                if (key.Equals("left"))
                {
                    if (index > -1) index--;
                    else index = 0;

                    UpdateUI();
                }

                

                if (key.Equals("confirm"))
                {
                    switch (index)
                    {
                        case -1:
                            AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
                            SceneManager.LoadScene("Credits");
                            break;
                        case 0:
                            AudioManager.instance.PlaySoundOnce(SoundEvent.ButtonPressed, transform.position);
                            AirConsole.instance.SetActivePlayers();
                            SceneManager.LoadScene("ChoosePlayer");
                            ChooseCharacterManager.instance.GraphicsIsVisible();
                            ChooseCharacterManager.instance.ResetCountConfirm();
                            break;
                        /*case 1:
                            Application.Quit();
                            break;*/
                    }
                }
            }
        }
    }

    private void UpdateUI()
    {
        switch(index)
        {
            case -1:
                credits.Select();
                break;
            case 0:
                start.Select();
                break;
            /*case 1:
                quit.Select();
                break;*/
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
