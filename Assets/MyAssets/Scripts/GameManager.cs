using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        /*int n = PlayerPrefs.GetInt("Players");
        Debug.Log(n);
        if (n == 0)
            n = 1;
        for (int i = 0; i < n; i++)
        {
            GameObject player = Instantiate(PlayerPrefab,spawns[i].transform.position,Quaternion.identity);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
