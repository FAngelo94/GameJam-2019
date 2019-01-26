using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Dictionary<string, string> Points = new Dictionary<string, string>();
    public GameObject Panels;
    //public GameObject PlayerPrefab;
    //public GameObject[] spawns;
    public
    // Start is called before the first frame update
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
            RectTransform r = Panels.transform.Find("Player " + (i + 1)).GetComponent<RectTransform>();
            r.localScale = new Vector2(r.localScale.x, r.localScale.y / 100 * float.Parse(Points["Player " + (i + 1)]));
            Panels.transform.Find("Point " + (i + 1)).GetComponent<Text>().text = Points["Player " + (i + 1)] + "&";
        }

    }

    public void UpdatePoints(string player, float points)
    {
        Points[player] = points.ToString();
    }
}
