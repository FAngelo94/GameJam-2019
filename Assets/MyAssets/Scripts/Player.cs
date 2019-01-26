using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Type of commands for this player")]
    public PlayerCommands Commands;
    [Header("Speed of the Player")]
    public float Speed = 20;
    [Header("Speed of the Player when slow")]
    public float SpeedSlow = 10;
    [Header("Time of stun in seconds")]
    public float StunTime = 2;
    [Header("Text with points")]
    public Text Points;
    private float PointsFloat;

    [FMODUnity.EventRef]
    public string BumpCollisionEvent;
    [FMODUnity.EventRef]
    public string StealCollisionEvent;

    private bool CheckSlow;
    private GameObject Pizza;
    private bool Stunned;

    private GameObject Fat;
    private Vector2 FatScale;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity.Set(0, 0, 0);
        PointsFloat = 0;
        Points.text = PointsFloat + "%";
        CheckSlow = false;
        Stunned = false;
        Fat = transform.Find("Fat").gameObject;
        FatScale = Fat.transform.localScale;
        StartCoroutine(EatPizza());
    }

    public void AddPizza(GameObject pizza)
    {
        pizza.transform.SetParent(transform);
        Vector2 p = new Vector2(1f, 0);
        pizza.transform.localPosition = p;
        pizza.transform.rotation = Quaternion.identity;
        Pizza = pizza;
        float scale = FatScale.x;
        scale = scale + PointsFloat / 100;
        Fat.transform.localScale = new Vector2(scale, scale);
    }

    public bool IsStunned()
    {
        return Stunned;
    }

    private void Update()
    {
        
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
            if (Input.GetKey(KeyCode.Joystick2Button1))
                XSpeed++;
            if (Input.GetKey(KeyCode.Joystick2Button3))
                XSpeed--;
            if (Input.GetKey(KeyCode.Joystick2Button0))
                YSpeed++;
            if (Input.GetKey(KeyCode.Joystick2Button2))
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
            float h = Input.GetAxis("HorizontalJoy2");
            float v = Input.GetAxis("VerticalJoy2");

            if (h == -1) XSpeed--;
            else if (h == 1) XSpeed++;
            if (v == -1) YSpeed++;
            else if (v == 1) YSpeed--;
        }
        if (!Stunned)
            SetVelocity(XSpeed, YSpeed);
        if (XSpeed != 0 || YSpeed != 0)
            SetRotation(XSpeed, YSpeed);
    }
    

    private void SetVelocity(float X,float Y)
    {
        float s = Speed;
        if (CheckSlow)
            s = SpeedSlow;
        float x = X / 10 * s;
        float y = Y / 10 * s;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject opponent = collision.gameObject;

        if (opponent.tag.Equals("Player"))
        {
            if (!Stunned && !opponent.GetComponent<Player>().IsStunned() && Pizza != null)
            {
                FMODUnity.RuntimeManager.PlayOneShot(StealCollisionEvent, transform.position);

                collision.gameObject.GetComponent<Player>().AddPizza(Pizza);
                Pizza = null;
                Stunned = true;
                StartCoroutine(RemoveStun());
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(BumpCollisionEvent, transform.position);
            }
        }
    }
    private IEnumerator RemoveStun()
    {
        yield return new WaitForSeconds(StunTime);
        Stunned = false;
    }
    private IEnumerator EatPizza()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(Pizza!=null)
            {
                Pizza.GetComponent<Pizza>().DecrementPizza();
                PointsFloat += 0.1f;
                PointsFloat = (int)(PointsFloat * 10);
                PointsFloat /= 10;
                Debug.Log(PointsFloat);
                Points.text = PointsFloat + "%";
            }
            Debug.Log("Not Eat");
        }
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