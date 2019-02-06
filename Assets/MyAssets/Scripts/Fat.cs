using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fat : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Player_AirConsole p = transform.parent.GetComponent<Player_AirConsole>();
            if (p != null)
            {
                if (collision.transform.GetComponent<Player_AirConsole>() != null)
                    transform.parent.GetComponent<Player_AirConsole>().CheckBump(collision.gameObject);
                else
                    transform.parent.GetComponent<Player_AirConsole>().CheckBump(collision.transform.parent.gameObject);
            }
        }
    }
}
