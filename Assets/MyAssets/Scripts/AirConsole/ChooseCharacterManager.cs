using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class ChooseCharacterManager : MonoBehaviour
{
    public static ChooseCharacterManager instance;
    public GameObject Graphics;

    private int CountConfirm;
    private int[] PlayerChoosen;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
        CountConfirm = 0;
        PlayerChoosen = new int[8];
    }

    public void ConfirmPlayer(int active_player, int index_character)
    {
        CountConfirm++;
        int totalPlayer = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        PlayerChoosen[active_player] = index_character;
        if (CountConfirm == totalPlayer)
        {//Start the game
            StartGame();
        }
    }

    public int GetChoosen(int player)
    {
        return PlayerChoosen[player];
    }

    public void StartGame()
    {
        Graphics.SetActive(false);
        SceneManager.LoadScene("Level1_AirConsoleTest");
    }
}
