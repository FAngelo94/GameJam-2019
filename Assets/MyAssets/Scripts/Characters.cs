using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour { 

    public float SpeedSlider = 5;

    private GameObject Panels;
    // Start is called before the first frame update
    void Start()
    {
        Panels = transform.Find("Panels").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 p=Panels.transform.position;
        p.x -= SpeedSlider;
        Panels.transform.position = p;
    }
}
