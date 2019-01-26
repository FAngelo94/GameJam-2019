using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float PushForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if(collision.tag.Equals("Player"))
        {
            Transform p = collision.transform;
            Vector2 playerPos = p.position;
            Vector2 wallPos = transform.position;
            float x=0,y=0;
            if (playerPos.x > wallPos.x)
                x++;
            if (playerPos.x < wallPos.x)
                x--;
            if (playerPos.y > wallPos.y)
                y++;
            if (playerPos.x < wallPos.x)
                y--;
            p.Translate(x/10, y/10, 0, Space.World);
        }
    }
}
