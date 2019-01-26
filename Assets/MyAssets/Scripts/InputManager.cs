using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Type of commands for this player")]
    public PlayerCommands Commands;
    [Header("Speed of the Player")]
    public float Speed;
    [Header("Speed of the Player when slow")]
    public float SpeedSlow;

    private bool CheckSlow;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity.Set(0, 0, 0);
        CheckSlow = false;
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
        if (Commands.Equals(PlayerCommands.IJKL))
        {
            if (Input.GetKey(KeyCode.L))
                XSpeed++;
            if (Input.GetKey(KeyCode.J))
                XSpeed--;
            if (Input.GetKey(KeyCode.I))
                YSpeed++;
            if (Input.GetKey(KeyCode.K))
                YSpeed--;

        }
        if (Commands.Equals(PlayerCommands.Pad1))
        {
            if (Input.GetKey(KeyCode.Joystick1Button1))
                XSpeed++;
            if (Input.GetKey(KeyCode.Joystick1Button3))
                XSpeed--;
            if (Input.GetKey(KeyCode.Joystick1Button0))
                YSpeed++;
            if (Input.GetKey(KeyCode.Joystick1Button2))
                YSpeed--;
        }
        if (Commands.Equals(PlayerCommands.Pad2))
        {
            if (Input.GetKey(KeyCode.Joystick3Button1))
                XSpeed++;
            if (Input.GetKey(KeyCode.Joystick3Button3))
                XSpeed--;
            if (Input.GetKey(KeyCode.Joystick3Button0))
                YSpeed++;
            if (Input.GetKey(KeyCode.Joystick3Button2))
                YSpeed--;
        }
        if (Commands.Equals(PlayerCommands.Pad3))
        {
            float h = Input.GetAxis("HorizontalJoy1");
            float v = Input.GetAxis("VerticalJoy1");

            if (h == -1) XSpeed--;
            else if (h == 1) XSpeed++;
            if (v == -1) YSpeed++;
            else if (v == 1) YSpeed--;
        }
        if (Commands.Equals(PlayerCommands.Pad4))
        {
            float h = Input.GetAxis("HorizontalJoy3");
            float v = Input.GetAxis("VerticalJoy3");

            if (h == -1) XSpeed--;
            else if (h == 1) XSpeed++;
            if (v == -1) YSpeed++;
            else if (v == 1) YSpeed--;
        }
        SetVelocity(XSpeed, YSpeed);
        if (XSpeed != 0 || YSpeed != 0)
            SetRotation(XSpeed, YSpeed);
    }
    

    private void SetVelocity(float X,float Y)
    {
        float s = Speed;
        if (CheckSlow)
            s = SpeedSlow;
        float x = X / 100 * s;
        float y = Y / 100 * s;
        transform.GetComponent<Rigidbody2D>().velocity=new Vector2(x,y);
        
    }

    private void SetRotation(float X, float Y)
    {
        Debug.Log(X + "-" + Y);
        float rotation = 0;
        if (X < 0 && Y==0)
            rotation = 180;
        if (X < 0 && Y>0)
            rotation = 135;
        if (X < 0 && Y<0)
            rotation = 225;
        if (X > 0 && Y == 0)
            rotation = 0;
        if (X > 0 && Y > 0)
            rotation = 45;
        if (X > 0 && Y < 0)
            rotation = 305;
        if (X == 0 && Y > 0)
            rotation = 90;
        if (X == 0 && Y < 0)
            rotation = 270;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("SlowObject"))
        CheckSlow = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("SlowObject"))
            CheckSlow = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("SlowObject"))
            CheckSlow = false;
    }

}

public enum PlayerCommands
{
    WASD,
    IJKL,
    Arrows,
    Pad1,
    Pad2,
    Pad3,
    Pad4
}