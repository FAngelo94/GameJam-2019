using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Characters : MonoBehaviour { 

    public float SpeedSlider = 5;

    private bool check;
    private GameObject Panels;
    // Start is called before the first frame update
    void Start()
    {
        Panels = transform.Find("Panels").gameObject;
        check = true;
    }

    private void Update()
    {
        if(Input.anyKey)
            SceneManager.LoadScene("Level1");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 p=Panels.transform.position;
        if (p.x > -7000)
        {
            p.x -= SpeedSlider;
            Panels.transform.position = p;
        }
        if (p.x <= -7000 && check)
        {
            check = false;
            StartCoroutine(StartGame());
        }

    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Level1");
    }
}
