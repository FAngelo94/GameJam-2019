using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class Player_AirConsole : MonoBehaviour
{
    [Header("Player ID")]
    public int PlayerID;
    [Header("Speed of the Player")]
    public float Speed = 150;
    [Header("Speed of the Player when very slow")]
    public float SpeedVerySlow = 75;
    [Header("Speed of the Player when slow")]
    public float SpeedSlow = 100;
    [Header("Speed of the Player when fast")]
    public float SpeedFast = 200;
    [Header("Time of stun in seconds")]
    public float StunTime = 1;
    [Header("Text with points")]
//    public Text Points;
    private float PointsFloat;
    [FMODUnity.EventRef]
    public string FootStepEvent;
    [FMODUnity.EventRef]
    public string WaterStepEvent;
    [FMODUnity.EventRef]
    public string ClothStepEvent;
    private int stepSoundTimer;

    [FMODUnity.EventRef]
    public string BumpCollisionEvent;
    [FMODUnity.EventRef]
    public string StealCollisionEvent;
    [FMODUnity.EventRef]
    public string CarpetEvent;

    private bool CheckVerySlow;
    private bool CheckSlow;
    private bool CheckFast;
    private GameObject Pizza;
    private bool Stunned;

    private GameObject Fat;
    private Vector2 FatScale;

    private bool moveUp, moveDown, moveLeft, moveRight;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity.Set(0, 0, 0);
        PointsFloat = 0;
//        Points.text = PointsFloat + "%";
        CheckSlow = false;
        CheckFast = false;
        Stunned = false;
        Fat = transform.Find("Fat").gameObject;
        FatScale = Fat.transform.localScale;
        StartCoroutine(EatPizza());

        moveUp = moveDown = moveLeft = moveRight = false;
    }

    public void AddPizza(GameObject pizza)
    {
        pizza.transform.SetParent(transform);
        Vector2 p = new Vector2(1f, 0);
        pizza.transform.localPosition = p;
        pizza.transform.rotation = Quaternion.identity;
        Pizza = pizza;
    }

    public bool IsStunned()
    {
        return Stunned;
    }

    public void CheckBump(GameObject opponent)
    {
        if (!Stunned && !opponent.GetComponent<Player_AirConsole>().IsStunned() && Pizza != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(StealCollisionEvent, transform.position);

            opponent.GetComponent<Player_AirConsole>().AddPizza(Pizza);
            Pizza = null;
            Stunned = true;
            StartCoroutine(RemoveStun());
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(BumpCollisionEvent, transform.position);
        }
    }

    private void FixedUpdate()
    {
        int XSpeed = 0;
        int YSpeed = 0;

        stepSoundTimer++;
        bool playSound = false;

        if (moveUp) YSpeed++;
        if (moveDown) YSpeed--;
        if (moveRight) XSpeed++;
        if (moveLeft) XSpeed--;

        if (moveUp || moveDown || moveRight || moveLeft) playSound = true;

        if (!Stunned)
            SetVelocity(XSpeed, YSpeed);
        if (XSpeed != 0 || YSpeed != 0)
            SetRotation(XSpeed, YSpeed);

        if (playSound && stepSoundTimer > 12)
        {
            if (CheckVerySlow)
            {
                FMODUnity.RuntimeManager.PlayOneShot(WaterStepEvent, transform.position);
            }
            else if (CheckSlow)
            {
                FMODUnity.RuntimeManager.PlayOneShot(ClothStepEvent, transform.position);
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(FootStepEvent, transform.position);
            }
            stepSoundTimer = 0;
        }
    }

    private void SetVelocity(float X, float Y)
    {
        float s = Speed;
        if (CheckVerySlow)
            s = SpeedVerySlow;
        if (CheckSlow)
            s = SpeedSlow;
        if (CheckFast)
            s = SpeedFast;
        s = s / 100 * (100 - PointsFloat);
        s = s > 0 ? s : 0;
        float x = X / 10 * s;
        float y = Y / 10 * s;
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);

    }

    private void SetRotation(float X, float Y)
    {
        float rotation = 0;
        if (X < 0 && Y == 0)
            rotation = 180;
        if (X < 0 && Y > 0)
            rotation = 135;
        if (X < 0 && Y < 0)
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
        if (collision.tag.Equals("WaterObject"))
        {
            CheckVerySlow = true;
        }
        if (collision.tag.Equals("SlowObject"))
        {
            CheckSlow = true;
        }
        if (collision.tag.Equals("FastObject"))
        {
            CheckFast = true;
            FMODUnity.RuntimeManager.PlayOneShot(CarpetEvent, transform.position);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("WaterObject"))
            CheckVerySlow = true;
        if (collision.tag.Equals("SlowObject"))
            CheckSlow = true;
        if (collision.tag.Equals("FastObject"))
            CheckFast = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("WaterObject"))
            CheckVerySlow = false;
        if (collision.tag.Equals("SlowObject"))
            CheckSlow = false;
        if (collision.tag.Equals("FastObject"))
            CheckFast = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject opponent = collision.gameObject;

        if (opponent.tag.Equals("Player"))
        {
            CheckBump(opponent);
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
            if (Pizza != null)
            {
                Pizza.GetComponent<Pizza>().DecrementPizza();
                PointsFloat += 0.1f;
//                Points.text = PointsFloat.ToString("F1") + " % ";
                float scale = FatScale.x;
                scale = scale + PointsFloat / 30;
                Fat.transform.localScale = new Vector2(scale, scale);
                IncrementMass();
                GameManager.instance.UpdatePoints(gameObject.name, PointsFloat);
            }
        }
    }
    private void IncrementMass()
    {
        transform.GetComponent<Rigidbody2D>().mass = 1 + PointsFloat;
    }

    //AirConsole Functions
    void OnMessage(int device_id, JToken data)
    {
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);

        if (active_player == PlayerID)
        {
            if (data != null &&
                data["key"] != null &&
                data["pressed"] != null)
            {
                string key = key = (string)data["key"];
                if ((bool)data["pressed"])
                {
                    Debug.Log("IF");
                    if (key.Equals("up")) moveUp = true;
                    if (key.Equals("down")) moveDown = true;
                    if (key.Equals("right")) moveRight = true;
                    if (key.Equals("left")) moveLeft = true;
                }
                else
                {
                    Debug.Log("ELSE");
                    if (key.Equals("up")) moveUp = false;
                    if (key.Equals("down")) moveDown = false;
                    if (key.Equals("right")) moveRight = false;
                    if (key.Equals("left")) moveLeft = false;
                }
            }
        }
    }

    void OnDestroy()
    {
        // unregister airconsole events on scene change
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}