using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterManager : MonoBehaviour
{
    public static ChooseCharacterManager instance;
    public GameObject Graphics;

    private int CountConfirm;
    public void ResetCountConfirm() { CountConfirm = 0; }
    private int[] PlayerChoosen;

    public Sprite connected;
    public Sprite disconnected;
    public Text message;
    public Button startBtn;

    public GameObject[] connectedPlayers;

    public GameObject connectionManager;

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
        PlayerChoosen = new int[7];
    }


    private void Update()
    {
        if(connectionManager) UpdateGUI(connectionManager.GetComponent<ConnectionManager>().playerCount);
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

    void UpdateGUI(int players)
    {
        Debug.Log(players);

        //player message and start button
        string mtext = "";
        bool btnOn = true; 
        if (players >= 7) mtext = "Max number of players connected";
        else if( players < 2)
        {
            mtext = "At least 2 players connected to start game";
            btnOn = false;
        }

        startBtn.gameObject.SetActive(btnOn);
        message.text = mtext;
        for (int i = 0; i < 7; i++)
        {
            if (i < players)
            {
                connectedPlayers[i].GetComponent<Image>().sprite = connected;
                connectedPlayers[i].transform.Find("Player").gameObject.SetActive(true);
            }
            else
            {
                connectedPlayers[i].GetComponent<Image>().sprite = disconnected;
                connectedPlayers[i].transform.Find("Player").gameObject.SetActive(false);
            }
        }
    }
}
