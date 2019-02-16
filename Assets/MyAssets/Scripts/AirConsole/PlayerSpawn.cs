using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NDream.AirConsole;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    [Header("Player prefab")]
    public GameObject playerPrefab;
    [Header("this SpawnPoint number")]
    public GameObject[] spawn_locations;
    [Header("Sprites for all players [7][3]")]
    public Sprite[] spritesList; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        int playerNum = AirConsole.instance.GetActivePlayerDeviceIds.Count;
        Debug.Log(playerNum + " player in game");
        if (playerNum < 1 || playerNum > 7) return;

        //number of spawn locations
        int n = spawn_locations.Length;
        //flag to identify if a location has been already used
        bool[] used_spawn = new bool[n];
        for (int i=0; i < n; i++) used_spawn[i] = false;

        for (int i=0; i < playerNum; i++)
        {
            //randomize spawn location
            int spawn_point = Random.Range(0, n);
            if (!used_spawn[spawn_point])
            {
               used_spawn[spawn_point] = true;
            }
            else
            {
                while (used_spawn[spawn_point])
                {
                    spawn_point = Random.Range(0, n);
                }
                used_spawn[spawn_point] = true;
            }

            //Instantiate player
            GameObject player = Instantiate(playerPrefab, spawn_locations[spawn_point].transform);

            //Assign player ID
            player.GetComponent<Player_AirConsole>().PlayerID = i;

            //Sprite da cambiare in base alla selezione nella scena precedente!
            //un qualcosa tipo sostituire la "i" con GameManager.instance.Player[i].spriteSelection(), integer da 0 a 6 che identifica il personaggio.
            int index = ChooseCharacterManager.instance.GetChoosen(i);
            player.GetComponent<SpriteRenderer>().sprite = spritesList[0 + 3 * index];
            player.transform.Find("Fat").gameObject.GetComponent<SpriteRenderer>().sprite = spritesList[1 + 3 * index];
            player.transform.Find("Hands").gameObject.GetComponent<SpriteRenderer>().sprite = spritesList[2 + 3 * index];
            player.name = "Player " + (i + 1);
        }
    }
}
