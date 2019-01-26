using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Dictionary<string, string> Points = new Dictionary<string, string>();
    public GameObject PlayerPrefab;
    public GameObject[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            instance = this;
        for (int i = 0; i < 7; i++)
            Points.Add("Player " + (i + 1), "0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TheEnd()
    {
        SceneManager.LoadScene("TheEnd");

    }

    public void UpdatePoints(string player, int points)
    {
        Points[player] = points.ToString();
    }
}
