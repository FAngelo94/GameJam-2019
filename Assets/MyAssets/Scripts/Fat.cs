using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fat : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Player p = transform.parent.GetComponent<Player>();
            if (p != null)
            {
                if (collision.transform.GetComponent<Player>() != null)
                    transform.parent.GetComponent<Player>().CheckBump(collision.gameObject);
                else
                    transform.parent.GetComponent<Player>().CheckBump(collision.transform.parent.gameObject);
            }
        }
    }
}
