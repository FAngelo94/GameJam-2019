using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    public RectTransform PizzaPanel;
    public float PercentOfSecond=1;

    private bool PizzaTaken;

    private float Width;
    private float PizzaRemain;
    // Start is called before the first frame update
    void Start()
    {
        PizzaTaken = false;
        Width = PizzaPanel.rect.width;
        PizzaRemain = 100;
    }

    public void DecrementPizza()
    {
        PizzaRemain--;
        PizzaPanel.sizeDelta = new Vector2(Width / 100 * PizzaRemain, 0);
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
