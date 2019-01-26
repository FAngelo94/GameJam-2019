using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Type of commands for this player")]
    public PlayerCommands Commands;
    [Header("Speed of the Player")]
    public float Speed;

    private bool NoCollision;

    // Start is called before the first frame update
    void Start()
    {
        NoCollision = true;
    }

    public void SetPlayer(GameObject p)
    {
       
    }

    public void AllowMovement()
    {
        NoCollision = true;
    }

    private void FixedUpdate()
    {
        int XSpeed = 0;
        int YSpeed = 0;
        if (Commands.Equals(PlayerCommands.WASD))
        {
            if (Input.GetKey(KeyCode.D))
                XSpeed++;
            if (Input.GetKey(KeyCode.A))
                XSpeed--;
            if (Input.GetKey(KeyCode.W))
                YSpeed++;
            if (Input.GetKey(KeyCode.S))
                YSpeed--;
        }
        if (Commands.Equals(PlayerCommands.Arrows))
        {
            if (Input.GetKey(KeyCode.RightArrow))
                XSpeed++;
            if (Input.GetKey(KeyCode.LeftArrow))
                XSpeed--;
            if (Input.GetKey(KeyCode.UpArrow))
                YSpeed++;
            if (Input.GetKey(KeyCode.DownArrow))
                YSpeed--;

        }
        if (Commands.Equals(PlayerCommands.Pad1))
        {

        }
        if (Commands.Equals(PlayerCommands.Pad2))
        {

        }
        if(NoCollision)
            SetVelocity(XSpeed, YSpeed);
    }
    

    private void SetVelocity(float X,float Y)
    {
        Debug.Log(X+"-"+Y);
        float x = X / 100 * Speed;
        float y = Y / 100 * Speed;
        Debug.Log(x + " - " + y);
        transform.Translate(x, y, 0, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        //NoCollision = false;
    }

}

public enum PlayerCommands
{
    WASD,
    Arrows,
    Pad1,
    Pad2
}