using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ChooseCharacter : MonoBehaviour
{
    [Header("Player ID")]
    public int PlayerID;

    private int MaxCharacter = 7;//numero dei personaggi giocabili
    private int IndexCharacter;
    private int OldIndex;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    void OnDestroy()
    {
        // unregister airconsole events on scene change
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IndexCharacter = 1;
        OldIndex = IndexCharacter;
    }

    //AirConsole Functions
    void OnMessage(int device_id, JToken data)
    {
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        Debug.Log("active_player=" + active_player + " confirmed="+ !ChooseCharacterManager.instance.HasPlayerConfirmed(active_player));
        if (active_player == PlayerID && !ChooseCharacterManager.instance.HasPlayerConfirmed(active_player))
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
                        IndexCharacter++;
                        if (IndexCharacter > MaxCharacter)
                            IndexCharacter = 1;
                        ChangeCharacter();
                    }
                    if (key.Equals("left"))
                    {
                        IndexCharacter--;
                        if (IndexCharacter < 1)
                            IndexCharacter = MaxCharacter;
                        ChangeCharacter();
                    }
                    if (key.Equals("confirm"))
                    {
                        ChoosePlayer(active_player);
                    }
                }
            }
        }
    }

    private void ChangeCharacter()
    {
        string nameSprite = "Character " + OldIndex;
        transform.Find(nameSprite).gameObject.SetActive(false);
        nameSprite = "Character " + IndexCharacter;
        transform.Find(nameSprite).gameObject.SetActive(true);
        OldIndex = IndexCharacter;
    }

    private void ChoosePlayer(int active_player)
    {
        ChooseCharacterManager.instance.ConfirmPlayer(active_player, IndexCharacter - 1);
    }
}
