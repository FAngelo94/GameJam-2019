using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    private bool PizzaTaken;
    // Start is called before the first frame update
    void Start()
    {
        PizzaTaken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PizzaTaken && collision.tag.Equals("Player"))
        {
            collision.GetComponent<Player>().AddPizza(gameObject);
            PizzaTaken = true;
        }
    }
}
