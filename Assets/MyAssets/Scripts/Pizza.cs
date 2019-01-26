using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public RectTransform PizzaPanel;
    public float PercentOfSecond=200;

    private bool PizzaTaken;

    private float Width;
    private float PizzaRemain;

    [FMODUnity.EventRef]
    public string GrabEvent;

    // Start is called before the first frame update
    void Start()
    {
        PizzaTaken = false;
        Width = PizzaPanel.localScale.x;
        PizzaRemain = 100;
    }

    public void DecrementPizza()
    {
        PizzaRemain -= 0.1f;
        PizzaPanel.localScale = new Vector2(Width / 100 * PizzaRemain, 1);
        if (PizzaRemain <= 0)
            GameManager.instance.TheEnd();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PizzaTaken && collision.tag.Equals("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(GrabEvent, transform.position);

            collision.GetComponent<Player>().AddPizza(gameObject);
            PizzaTaken = true;
        }
    }

    
}
