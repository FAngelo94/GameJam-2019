using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//airconsole
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Dictionary<string, string> Points = new Dictionary<string, string>();
    public GameObject Panels;

    void Awake()
    {
//        AirConsole.instance.onConnect += OnConnect;
//        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    void Start()
    {
        
        instance = this;
        for (int i = 0; i < 7; i++)
            Points.Add("Player " + (i + 1), "0");
        Panels.SetActive(false);
        DontDestroyOnLoad(this);
    }

    public void TheEnd()
    {
        SceneManager.LoadScene("TheEnd");
        Panels.SetActive(true);
        for(int i = 0; i < 7; i++)
        {
            //Set the dimension of pizza
            RectTransform r = Panels.transform.Find("Player " + (i + 1)).GetComponent<RectTransform>();
            r.localScale = new Vector2(r.localScale.x, 1 - r.localScale.y / 100 * float.Parse(Points["Player " + (i + 1)]));
            Panels.transform.Find("Point " + (i + 1)).GetComponent<Text>().text = float.Parse(Points["Player " + (i + 1)]).ToString("F1") + "%";
            //Set sprite
            GameObject character = Panels.transform.Find("Character " + (i + 1)).gameObject;
            int spriteIndex = ChooseCharacterManager.instance.GetChoosen(i);
            //disable all sprite
            for(int j = 0; j < 7; j++)
            {
                GameObject s = character.transform.Find("Sprite " + (j + 1)).gameObject;
                s.SetActive(false);
            }
            //active only the selected sprite
            GameObject sprite = character.transform.Find("Sprite " + (spriteIndex+1)).gameObject;
            sprite.SetActive(true);
        }

    }

    public void UpdatePoints(string player, float points)
    {
        Points[player] = points.ToString();
    }

    public void Restart()
    {
        Panels.SetActive(false);
        SceneManager.LoadScene("Level1_AirConsoleTest");
    }

    public void Quit()
    {
        ChooseCharacterManager.instance.ResetCountConfirm();
        Panels.SetActive(false);
        SceneManager.LoadScene("MainMenu");
        //Application.Quit();
    }

    //AirConsole
    /*
    void OnConnect(int device_id)
    {
        int devNum = AirConsole.instance.GetControllerDeviceIds().Count;
        if (devNum < 7) AirConsole.instance.SetActivePlayers(devNum);
    }

    void OnDisconnect(int device_id)
    {
        int devNum = AirConsole.instance.GetControllerDeviceIds().Count;
        if (devNum < 7) AirConsole.instance.SetActivePlayers(devNum);
    }*/
}
