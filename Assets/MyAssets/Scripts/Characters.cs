using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Characters : MonoBehaviour { 

    public float SpeedSlider = 5;

    private GameObject Panels;
    // Start is called before the first frame update
    void Start()
    {
        Panels = transform.Find("Panels").gameObject;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            SceneManager.LoadScene("Level_1");
        //SceneManager.LoadScene("Level1");
        if (Input.GetKey(KeyCode.RightArrow))
            Slide(20);
        if (Input.GetKey(KeyCode.LeftArrow))
            Slide(-20);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void Slide(int speed)
    {
        Vector2 p = Panels.transform.position;
        p.x -= SpeedSlider + speed;
        Panels.transform.position = p;
       
    }
}
